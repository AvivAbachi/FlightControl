import { Component, Input } from '@angular/core';
import { Flight } from 'src/app/models/flight';

@Component({
  selector: 'app-airport-map',
  templateUrl: './airport-map.component.html',
})
export class AirportMapComponent {
  @Input() data: Flight[] = [];
  // constructor() {
  //   const f = new Flight();
  //   f.flightId = 2;
  //   f.location.x = 565;
  //   f.location.y = 215;
  //   f.location.r = 130;
  //   this.data = [f];
  // }
}
