import { Component, Input } from '@angular/core';
import { Flight } from 'src/app/models/flight';

@Component({
  selector: 'app-airport-map',
  templateUrl: './airport-map.component.html',
})
export class AirportMapComponent {
  @Input() data: Flight[] = [];
}
