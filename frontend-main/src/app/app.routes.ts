import { Routes } from '@angular/router';
import { EventListComponent } from './components/event-list/event-list.component';
import { BookingFormComponent } from './components/booking-form/booking-form.component';
import { LoginComponent } from './components/login/login.component';
import { SignupComponent } from './components/signup/signup.component';
import { BookingHistoryComponent } from './components/booking-history/booking-history.component';
import { ProfileComponent } from './components/user-profile/user-profile.component';
import { EditProfileComponent } from './components/edit-profile/edit-profile.component';
import { authGuard } from './guards/auth-guard.guard';
import { EventDetailsComponent } from './components/event-details/event-details.component';


export const routes: Routes = [
    { path: '', component: EventListComponent },
    { path: 'events', component: EventListComponent },
    { path: 'event/:id', component: EventDetailsComponent},
    { path: 'login', component: LoginComponent },
    { path: 'signup', component: SignupComponent },
    { path: 'booking-history', component: BookingHistoryComponent, canActivate: [authGuard] },
    { path: 'profile', component: ProfileComponent, canActivate: [authGuard] },
    { path: 'profile/edit', component: EditProfileComponent, canActivate: [authGuard] }
];
