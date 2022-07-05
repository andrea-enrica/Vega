import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
  providedIn: 'root'
})
export class VehicleService {

  constructor(private httpClient: HttpClient) { }

  getMakes() {
    return this.httpClient.get('https://localhost:7151/api/makes');
  }

  getFeatures() {
    return this.httpClient.get('https://localhost:7151/api/features');
  }
}
