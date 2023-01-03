import { Flight } from './flight';

export class Station {
  constructor(
    public stationId: number,
    public name: string,
    public flight?: Flight
  ) {}
}
