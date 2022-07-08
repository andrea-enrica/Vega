import { KeyValuePair } from './../models/vehicle';
import { Component, OnInit } from '@angular/core';
import { Vehicle } from '../models/vehicle';
import { VehicleService } from '../services/vehicle.service';
import { any } from 'underscore';

@Component({
  selector: 'app-vehicle-list',
  templateUrl: './vehicle-list.component.html',
  styleUrls: ['./vehicle-list.component.css']
})
export class VehicleListComponent implements OnInit {
  vehicles: Vehicle[] = [];
  makes: KeyValuePair[] = [];
  query: any={};
  allVehicles: Vehicle[] = [];

  constructor(private vehicleService: VehicleService) { }

  // ngOnInit() { 
  //   this.vehicleService.getMakes()
  //     .subscribe((makes: any) => this.makes = makes);
  //   this.populateVehicles();
  // }

  ngOnInit() { 
    this.vehicleService.getMakes()
    .subscribe((makes:any )=> this.makes = makes);

    this.vehicleService.getVehicles()
      .subscribe((vehicles: any ) => this.vehicles = this.allVehicles = vehicles);
  }

  // private populateVehicles() {
  //   this.vehicleService.getVehicles(this.query)
  //     .subscribe((vehicles: any) => this.vehicles = vehicles);
  // }

  // onFilterChange() {
  //   this.populateVehicles();
  // }

  onFilterChange() {
    var vehicles = this.allVehicles;

    if (this.query.makeId)
      vehicles = vehicles.filter(v => v.make.id == this.query.makeId);

    if (this.query.modelId)
      vehicles = vehicles.filter(v => v.model.id == this.query.modelId);

    this.vehicles = vehicles;
  }

  resetFilter() {
    this.query = {};
    this.onFilterChange();
  }

  // sortBy(columnName: string) {
  //   console.log(columnName)
  //   if(this.query.sortBy == columnName) {
  //     this.query.isSortAscending = false;
  //   } else {
  //     this.query.sortBy = columnName;
  //     this.query.isSortAscending = true;
  //   }
  //   this.populateVehicles
  // }
}
