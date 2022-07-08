import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';

@Injectable({
    providedIn: 'root'
})
export class PhotoService {

    constructor(private httpClient: HttpClient) { }

    upload(vehicleId: any, photo: any) {
        var formData = new FormData();
        
        //key-value pair -> the same key as in api call method
        formData.append('file', photo);
        return this.httpClient.post(`https://localhost:7151/api/vehicles/${vehicleId}/photos`, formData);
    }

    
    getPhotos(vehicleId: any) {
        return this.httpClient.get(`https://localhost:7151/api/vehicles/${vehicleId}/photos`);
    }
} 