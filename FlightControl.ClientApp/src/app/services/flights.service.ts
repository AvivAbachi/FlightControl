import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Flight } from '../models/flight';

@Injectable({
  providedIn: 'root',
})
export class FlightsService {
  private isDeparture = new BehaviorSubject<boolean>(false);
  public departureCanged = this.isDeparture.asObservable();
  constructor(private http: HttpClient) {}

  setDeparture(departure: boolean) {
    this.isDeparture.next(departure);
  }
  getDeparture() {
    return this.isDeparture.getValue();
  }

  get() {
    return this.http.get<{ flights: Flight[]; map: Flight[] }>(
      '/Api/Airport/' + (this.isDeparture.getValue() ? 'Departure' : 'Arrival')
    );
  }
}
