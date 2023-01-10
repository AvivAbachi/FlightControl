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
  list: Flight[] = [];
  map: Flight[] = [];
  departure: boolean = this.flights.getDeparture();
  constructor(private flights: FlightsService) {
    this.flights.get();
  }
  ngOnInit(): void {
    this.id = setInterval(() => {
      this.flights.get().subscribe({
        next: (res) => {
          this.list = res.flights;
          this.map = res.map;
        },
        error: (err) => console.error(err),
      });
    }, 1000);
  }

  handelTabChange(e: any) {
    this.flights.setDeparture(e.index === 1);
    this.departure = this.flights.getDeparture();
  }
  ngOnDestroy() {
    if (this.id) {
      clearInterval(this.id);
    }
  }
}
