// 

import { Injectable } from '@angular/core';
import { Observable } from 'rxjs';
import { Event } from '../models/interface/event.interface';
import { HttpClient, HttpParams } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class EventService {
  private baseUrl = 'http://localhost:5241/api/Events';

  constructor(private http: HttpClient) {}

  getEvents(): Observable<Event[]> {
    return this.http.get<Event[]>(this.baseUrl);
  }

  getEvent(id: number): Observable<Event | undefined> {
    return this.http.get<Event | undefined>(`${this.baseUrl}/${id}`);
  }

  getFilteredEvents(categoryName?: string): Observable<Event[]> {
    let params = new HttpParams();
    if (categoryName) {
      params = params.set('categoryName', categoryName);
    }
    return this.http.get<Event[]>(`${this.baseUrl}/filtered`, { params });
  }
}