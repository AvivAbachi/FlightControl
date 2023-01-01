import { Station } from './station';

export class Flight {
  constructor(
    public id: number,
    public target: number,
    public airline?: string,
    public comeingForm?: string,
    public departingTo?: string,
    public arrivalDate?: Date,
    public departureDate?: Date,
    public location?: { x: number; y: number },
    public station?: Station
  ) {}
}
