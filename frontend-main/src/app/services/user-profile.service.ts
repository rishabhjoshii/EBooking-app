import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, tap } from 'rxjs';
import { User } from '../models/interface/user.interface';
import { AuthService } from './auth.service';

@Injectable({
  providedIn: 'root'
})
export class UserService {

  constructor(private http: HttpClient, private _authService : AuthService) {}

  updateProfile(profileData: any): Observable<User> {
    const user = localStorage.getItem('currentUser');
    if(user)
    {
      var usernew= JSON.parse(user);
      var token=usernew.token;
    }
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    
    return this.http.put<any>('http://localhost:5241/api/account', profileData, { headers }).pipe(
      tap(user => {
        localStorage.removeItem('currentUser');
        localStorage.setItem('currentUser', JSON.stringify(user));
        this._authService.currentUserSubject.next(user);
      })
    );
  }

  getUserProfile(token : string): Observable<any> {
    const headers = new HttpHeaders({
      'Authorization': `Bearer ${token}`
    });
    return this.http.get<any>('http://localhost:5241/api/account/userinfo', { headers });
  }
}
