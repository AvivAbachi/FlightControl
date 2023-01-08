import { Station } from './station';

export class Flight {
  flightId: number = 0;
  target: number = 0;
  airline: string = '';
  airport: string = '';
  // location: { x: number; y: number } = { x: 0, y: 0 };
  arrivalDate?: Date;
  departureDate?: Date;
  station?: Station;
}
