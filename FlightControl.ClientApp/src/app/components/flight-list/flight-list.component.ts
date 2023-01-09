import { Component, Input } from '@angular/core';
import { Flight } from 'src/app/models/flight';
@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
})
export class FlightListComponent {
  @Input() data: Flight[] = [];
  @Input() isDeparture: boolean = false;
  departure: string[] = [
    'flightId',
    'airline',
    'status',
    'boarding',
    'departure',
  ];

  arrival: string[] = ['flightId', 'airline', 'status', 'landing', 'arrival'];

  columns: string[] = this.isDeparture ? this.departure : this.arrival;
}
