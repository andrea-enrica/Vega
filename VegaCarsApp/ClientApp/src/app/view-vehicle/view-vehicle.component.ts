import { VehicleService } from '../services/vehicle.service';
import { Component, ElementRef, OnInit, ViewChild } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { PhotoService } from '../services/photo.service';

@Component({
  templateUrl: 'view-vehicle.component.html',
})
export class ViewVehicleComponent implements OnInit {
  @ViewChild('fileInput') fileInput!: ElementRef;
  vehicle: any;
  vehicleId: number | undefined; 
  photos: any[] = [];

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private vehicleService: VehicleService,
    private photoService: PhotoService,
    ) { 

    route.params.subscribe(p => {
      //daca am vehicle id sa ma duca la view vehicle altfel sa ma duc la vehicle list
      this.vehicleId = p['id'];
      if (this.vehicleId == undefined) {
        router.navigate(['/vehicles']);
        return; 
      }
    });
  }

  ngOnInit() { 
    this.photoService.getPhotos(this.vehicleId) 
      .subscribe((photos: any) => {
        this.photos = photos;
      });

    this.vehicleService.getVehicle(this.vehicleId)
      .subscribe(
        v => this.vehicle = v,
        err => {
          if (err.status == 404) {
            this.router.navigate(['/vehicles']);
            return; 
          }
        });
  }

  activeTab = 'basic';

  search(activeTab: string){
    this.activeTab = activeTab;
  }

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }

  uploadPhoto() {
    var nativeElement: HTMLInputElement = this.fileInput.nativeElement;

    this.photoService.upload(this.vehicleId, nativeElement.files![0])
      .subscribe(photo => {
        this.photos.push(photo);
      });
  }
} 