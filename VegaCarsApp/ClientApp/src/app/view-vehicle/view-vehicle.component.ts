import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';

@Component({
  templateUrl: 'view-vehicle.component.html'
})
export class ViewVehicleComponent implements OnInit {
  vehicle: any;
  vehicleId: number | undefined; 

  constructor(
    private route: ActivatedRoute, 
    private router: Router,
    private vehicleService: VehicleService) { 

    route.params.subscribe(p => {
      //daca am vehicle id sa ma duca la view vehicle altfel sa ma duc la vehicle list
      this.vehicleId = p['id'];
      if (this.vehicleId == undefined || this.vehicleId <= 0) {
        router.navigate(['/vehicles']);
        return; 
      }
    });
  }

  ngOnInit() { 
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

  delete() {
    if (confirm("Are you sure?")) {
      this.vehicleService.delete(this.vehicle.id)
        .subscribe(x => {
          this.router.navigate(['/vehicles']);
        });
    }
  }
} 