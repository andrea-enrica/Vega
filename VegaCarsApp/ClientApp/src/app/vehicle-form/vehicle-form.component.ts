import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';
import 'rxjs/add/observable/forkJoin';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any = [];
  models: any = [];
  features: any = [];
  vehicle: any = {
    features: [],
    contact: {}
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router, 
    private VehicleService: VehicleService) {
      route.params.subscribe(p=> {
        this.vehicle.id = +p['id'] //plus in front to convert into a number
      });
     }

  ngOnInit(): void {
    var sources = [
      this.VehicleService.getMakes(),
      this.VehicleService.getFeatures(),
    ];

    if(this.vehicle.id) 
      sources.push(this.VehicleService.getVehicle(this.vehicle.id));

    Observable.forkJoin(sources).subscribe((data: any) => {
        this.makes = data[0];
        this.features = data[1];

        if(this.vehicle.id)
          this.vehicle = data[2];
    }, (err: any) => {
        if(err.status == 404)
          this.router.navigate(['/home']);
    });
  }

  onMakeChange() {
    var selectedMake = this.makes.find((m: any) => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
    delete this.vehicle.modelId;
  }

  onFeatureToggle(featureId: any, $event: any) {
    if ($event.target.checked){
      this.vehicle.features.push(featureId);
    } else {
      var index = this.vehicle.features.indexOf(featureId);
      this.vehicle.features.splice(index, 1);
    }
  }

  submit() {
    if(this.vehicle.isRegistered === 'true'){
      this.vehicle.isRegistered = true
    } else {
      this.vehicle.isRegistered = false
    }

    this.VehicleService.create(this.vehicle)
      .subscribe(x => console.log(x));
  }
}
