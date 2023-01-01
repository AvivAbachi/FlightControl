import { Component, OnInit, OnDestroy } from '@angular/core';
import { Flight } from 'src/app/models/flight';
import { Station } from 'src/app/models/station';
import { FlightsService } from 'src/app/services/flights.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  id?: NodeJS.Timer | number;
  flightsList: Flight[] = [];
  stationsList: Station[] = [];

  constructor(private flights: FlightsService) {
    this.flights.get();
  }
  ngOnInit(): void {
    this.flights.get();
    this.id = setInterval(() => {
      this.flights.get().subscribe({
        next: (res) => {
          this.flightsList = res.flights;
          this.stationsList = res.stations.filter((s) => s.flight != null);
        },
        error: (err) => console.error(err),
      });
    }, 1000);
  }
  ngOnDestroy() {
    if (this.id) {
      clearInterval(this.id);
    }
  }
}
