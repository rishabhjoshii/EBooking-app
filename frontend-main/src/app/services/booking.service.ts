import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Event } from '../models/interface/event.interface';
import { HttpClient } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private http : HttpClient) { }

  createBooking(bookingData: any) : Observable<any> {
    return this.http.post("http://localhost:5241/api/booking", bookingData);
  }
}
