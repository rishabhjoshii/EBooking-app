import { Routes } from '@angular/router';
import { EventListComponent } from './components/event-list/event-list.component';
import { BookingFormComponent } from './components/booking-form/booking-form.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { BookingHistoryComponent } from './components/booking-history/booking-history.component';
import { ProfileComponent } from './components/user-profile/user-profile.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';


export const routes: Routes = [
    { path: '', component: EventListComponent },
    { path: 'events', component: EventListComponent },
    { path: 'booking/:id', component: BookingFormComponent },
    { path: 'login', component: LoginComponent },
    { path: 'signup', component: SignupComponent },
    { path: 'booking-history', component: BookingHistoryComponent},
    { path: 'profile', component: ProfileComponent },
    { path: 'profile/edit', component: EditProfileComponent }
];
