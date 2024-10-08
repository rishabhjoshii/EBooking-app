import { Injectable } from '@angular/core';
import { Observable, of } from 'rxjs';
import { Event } from '../models/interface/event.interface';
import { HttpClient, HttpHeaders } from '@angular/common/http';

@Injectable({
  providedIn: 'root'
})
export class BookingService {

  constructor(private http : HttpClient) { }

  createBooking(bookingData: any) : Observable<any> {
    const token = localStorage.getItem('token');

    const headers = new HttpHeaders({
      'Content-Type': 'application/json',
      'Authorization': `Bearer ${token}`  // Add the token to the Authorization header
      
    });
    
    return this.http.post("http://localhost:5241/api/booking", bookingData, { headers, withCredentials:true });
  }
}
