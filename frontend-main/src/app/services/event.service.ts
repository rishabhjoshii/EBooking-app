import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Event } from '../models/interface/event.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})

export class EventService {
  private events: Event[] = [];

  constructor(private http:HttpClient){}

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>("http://localhost:5241/api/Events");
  }

  getEvent(id: number): Observable<Event | undefined> {
    return this.http.get<Event | undefined>(`http://localhost:5241/api/Events/${id}`);
  }
}
