import { Routes } from '@angular/router';
import { EventListComponent } from './components/event-list/event-list.component';
import { BookingFormComponent } from './components/booking-form/booking-form.component';

export const routes: Routes = [
    { path: '', component: EventListComponent },
    { path: 'booking/:id', component: BookingFormComponent },
];
