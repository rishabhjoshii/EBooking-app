import { Routes } from '@angular/router';
import { EventListComponent } from './components/event-list/event-list.component';
import { BookingFormComponent } from './components/booking-form/booking-form.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';


export const routes: Routes = [
    { path: '', component: EventListComponent },
    { path: 'events', component: EventListComponent },
    { path: 'booking/:id', component: BookingFormComponent },
    { path: 'login', component: LoginComponent },
    { path: 'signup', component: SignupComponent },
];
