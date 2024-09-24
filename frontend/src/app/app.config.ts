import { provideRouter, Route } from '@angular/router';
import { EventListComponent } from './components/event-list/event-list.component';
import { EventBookingComponent } from './components/event-booking/event-booking.component';

export const routes: Route[] = [
  { path: '', component: EventListComponent },
  { path: 'booking/:id', component: EventBookingComponent }
];

export const AppConfig = [
  provideRouter(routes)
];

