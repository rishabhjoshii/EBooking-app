import { Injectable } from '@angular/core';
import { Event } from '../models/event.model';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private events: Event[] = [
    { id: 1, name: 'Music Concert', date: '2024-10-10', location: 'Delhi', price: 500 },
    { id: 2, name: 'Tech Conference', date: '2024-11-12', location: 'Bangalore', price: 1000 }
  ];

  getEvents(): Event[] {
    return this.events;
  }

  getEventById(id: number): Event | undefined {
    return this.events.find(event => event.id === id);
  }
}

