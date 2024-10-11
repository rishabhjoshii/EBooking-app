import { HttpClient } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap } from 'rxjs';
import { LoginRequest, SignupRequest, User } from '../models/interface/user.interface';

@Injectable({
  providedIn: 'root'
})
export class AuthService {
  public currentUserSubject: BehaviorSubject<User | null>;
  public currentUser: Observable<User | null>;
  private signupApiUrl = 'http://localhost:5241/api/account/register';
  private loginApiUrl = 'http://localhost:5241/api/account/login';

  constructor(private http: HttpClient) {
    const storedUser = localStorage.getItem('currentUser');
    this.currentUserSubject = new BehaviorSubject<User | null>(
      storedUser ? JSON.parse(storedUser) : null
    );
    this.currentUser = this.currentUserSubject.asObservable();
  }

  public get currentUserValue(): User | null {
    return this.currentUserSubject.value;
  }

  login(loginRequest: LoginRequest): Observable<User> {
    return this.http.post<any>(`${this.loginApiUrl}`, loginRequest).pipe(
      tap(user => {
        localStorage.setItem('currentUser', JSON.stringify(user));
        this.currentUserSubject.next(user);
      })
    );
  }

  signup(signupRequest: SignupRequest): Observable<User> {
    return this.http.post<any>(`${this.signupApiUrl}`, signupRequest);
  }

  logout() {
    localStorage.removeItem('currentUser');
    this.currentUserSubject.next(null);
  }

  isLoggedIn(): boolean {
    return !!this.currentUserValue;
  }
}