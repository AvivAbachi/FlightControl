import { Flight } from './flight';

export class Station {
  constructor(public id: number, public name: string, public flight?: Flight) {}
}
