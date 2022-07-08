import * as _ from 'underscore';
import { SaveVehicle, Vehicle } from './../models/vehicle';
import { forkJoin } from 'rxjs';
import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';
import { ActivatedRoute, Router } from '@angular/router';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-vehicle-form',
  templateUrl: './vehicle-form.component.html',
  styleUrls: ['./vehicle-form.component.css']
})
export class VehicleFormComponent implements OnInit {
  makes: any = [];
  models: any = [];
  features: any = [];
  vehicle: SaveVehicle = {
    id: "",
    makeId: "",
    modelId: "",
    isRegistered: false,
    features: [],
    contact: {
      name: '',
      email: '',
      phone: '',
    }
  };

  constructor(
    private route: ActivatedRoute,
    private router: Router, 
    private VehicleService: VehicleService) {
      route.params.subscribe(p=> {
        this.vehicle.id = p['id'] //plus in front to convert into a number
      });
     }

  ngOnInit(): void {
    var sources = [
      this.VehicleService.getMakes(),
      this.VehicleService.getFeatures(),
    ];

    if(this.vehicle.id) 
      sources.push(this.VehicleService.getVehicle(this.vehicle.id));

    forkJoin(sources).subscribe((data: any) => {
      this.makes = data[0];
      this.features = data[1];

      if(this.vehicle.id)
        this.setVehicle(data[2])
        this.populateModels();
    }, (err: any) => {
        if(err.status === 404)
          this.router.navigate(['']);
    });
  }

  private setVehicle(v: Vehicle) {
    this.vehicle.id = v.id;
    this.vehicle.makeId =v.make.id;
    this.vehicle.modelId = v.model.id;
    this.vehicle.isRegistered = v.isRegistered;
    this.vehicle.contact = v.contact;
    this.vehicle.features = _.pluck(v.features, 'id');
  }

  onMakeChange() {
    this.populateModels();
    delete this.vehicle.modelId;
  }

  private populateModels() {
    var selectedMake = this.makes.find((m: any) => m.id == this.vehicle.makeId);
    this.models = selectedMake ? selectedMake.models : [];
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
    if(this.vehicle.isRegistered){
      this.vehicle.isRegistered = true
    } else {
      this.vehicle.isRegistered = false
    }

    if(this.vehicle.id) {
      this.VehicleService.update(this.vehicle)
        .subscribe(
          response => {
            alert('Success: The vehicle was successfully updated.')
            this.router.navigate(['/vehicles']);
          }
        )
    } else {
      this.VehicleService.create(this.vehicle)
        .subscribe(
          (vehicle: any) => {
            alert('Success: The vehicle was successfully created.');
            this.router.navigate(['/vehicles/', vehicle.id])
          });
    }
  }
}
