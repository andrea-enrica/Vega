import { SaveVehicle } from './../models/vehicle';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { map } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {
  private readonly vehiclesEndpoint = 'https://localhost:7151/api/vehicles';
  private headers = new HttpHeaders().set('Content-Type', 'application/json; charset=utf-8');

  constructor(private httpClient: HttpClient) { }

  getMakes() {
    return this.httpClient.get('https://localhost:7151/api/makes');
  }

  getFeatures() {
    return this.httpClient.get('https://localhost:7151/api/features');
  }

  create(vehicle: SaveVehicle) {
    return this.httpClient.post(this.vehiclesEndpoint, vehicle)
  }

  getVehicle(id: any) {
    return this.httpClient.get(this.vehiclesEndpoint + '/' + id)
  }

  // getVehicles(filter: any) {
  //   return this.httpClient.get(this.vehiclesEndpoint + '?' + this.toQueryString(filter), {headers: this.headers});
  // }

  getVehicles() {
    return this.httpClient.get(this.vehiclesEndpoint);
  }

  // toQueryString(obj: any) {
  //   var parts = [];
  //   for (var property in obj) {
  //     var value = obj[property];
  //     if (value != null && value != undefined) 
  //       parts.push(encodeURIComponent(property) + '=' + encodeURIComponent(value));
  //   }

  //   return parts.join('&');
  // }

  update(vehicle: SaveVehicle) {
    return this.httpClient.put(this.vehiclesEndpoint + '/' + vehicle.id, vehicle)
  }

  delete(id: string) {
    return this.httpClient.delete(this.vehiclesEndpoint + '/' + id)
  }
}
