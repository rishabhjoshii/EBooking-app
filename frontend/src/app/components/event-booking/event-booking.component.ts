import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../../services/event.service';
import { Event } from '../../models/event.model';

@Component({
  selector: 'app-event-booking',
  templateUrl: './event-booking.component.html'
})
export class EventBookingComponent implements OnInit {
  event: Event | undefined;
  name: string = '';
  email: string = '';
  phone: string = '';
  tickets: number = 1;
  totalPrice: number = 0;

  constructor(private route: ActivatedRoute, private eventService: EventService) {}

  ngOnInit(): void {
    const id = Number(this.route.snapshot.paramMap.get('id'));
    this.event = this.eventService.getEventById(id);
    this.updateTotalPrice();
  }

  updateTotalPrice(): void {
    if (this.event) {
      this.totalPrice = this.event.price * this.tickets;
    }
  }

  onBook(): void {
    alert('Booking successful!');
  }
}

