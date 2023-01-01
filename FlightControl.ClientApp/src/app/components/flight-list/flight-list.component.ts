import { Component, Input } from '@angular/core';
import { Flight } from 'src/app/models/flight';
@Component({
  selector: 'app-flight-list',
  templateUrl: './flight-list.component.html',
})
export class FlightListComponent {
  @Input() data: Flight[] = [];
}
