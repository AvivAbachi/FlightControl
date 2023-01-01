import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Flight } from '../models/flight';
import { Station } from '../models/station';

@Injectable({
  providedIn: 'root',
})
export class FlightsService {
  constructor(private http: HttpClient) {}

  get() {
    return this.http.get<{
      flights: Flight[];
      stations: Station[];
    }>('/api/Airport');
  }
}
