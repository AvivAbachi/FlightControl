import { Component, OnInit, OnDestroy } from '@angular/core';
import { Flight } from 'src/app/models/flight';
import { FlightsService } from 'src/app/services/flights.service';

@Component({
  selector: 'app-home',
  templateUrl: './home.component.html',
})
export class HomeComponent implements OnInit, OnDestroy {
  id?: NodeJS.Timer | number;
  list: Flight[] = [];
  map: Flight[] = [];

  constructor(private flights: FlightsService) {
    this.flights.get();
  }
  ngOnInit(): void {
    this.id = setInterval(() => {
      this.flights.get().subscribe({
        next: (res) => {
          this.list = res.flights;
          // this.map = res.flights.filter((f) => {
          //   return !(f.station!.stationId < 1 || f.station!.stationId > 11);
          // });
        },
        error: (err) => console.error(err),
      });
    }, 100);
  }
  ngOnDestroy() {
    if (this.id) {
      clearInterval(this.id);
    }
  }
}
