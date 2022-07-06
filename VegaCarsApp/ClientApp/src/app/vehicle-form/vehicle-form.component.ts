import { VehicleService } from '../services/vehicle.service';
import { Component, OnInit } from '@angular/core';

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
    private VehicleService: VehicleService) { }

  ngOnInit(): void {
    this.VehicleService.getMakes().subscribe((makes: any) => this.makes = makes);
    this.VehicleService.getFeatures().subscribe((features: any) => this.features = features)
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
