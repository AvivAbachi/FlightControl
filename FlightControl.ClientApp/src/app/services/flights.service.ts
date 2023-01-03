import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Flight } from '../models/flight';

@Injectable({
  providedIn: 'root',
})
export class FlightsService {
  constructor(private http: HttpClient) {}

  get() {
    return this.http.get<{ flights: Flight[] }>('/api/Airport');
  }
}
