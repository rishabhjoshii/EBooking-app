import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { BookingFormComponent } from './components/booking-form/booking-form.component';
import { EventListComponent } from './components/event-list/event-list.component';
import { EventDetailComponent } from './components/event-detail/event-detail.component';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,BookingFormComponent,EventListComponent,EventDetailComponent],
  templateUrl: './app.component.html',
  styleUrl: './app.component.css'
})
export class AppComponent {
  title = 'frontend-main';
}
