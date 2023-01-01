import { Component, Input } from '@angular/core';
import { Station } from 'src/app/models/station';

@Component({
  selector: 'app-airport-map',
  templateUrl: './airport-map.component.html',
})
export class AirportMapComponent {
  @Input() data: Station[] = [];
}
