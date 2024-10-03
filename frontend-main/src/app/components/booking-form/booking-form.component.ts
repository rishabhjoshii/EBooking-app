import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Event } from '../../models/interface/event.interface';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { EventService } from '../../services/event.service';
import { BookingService } from '../../services/booking.service';
import { CommonModule } from '@angular/common';


@Component({
  selector: 'app-booking-form',
  standalone: true,
  imports: [CommonModule,ReactiveFormsModule, RouterModule],
  templateUrl: './booking-form.component.html',
  styleUrl: './booking-form.component.css'
})
export class BookingFormComponent implements OnInit {
  event: Event | undefined;
  bookingForm: FormGroup;
  totalPrice = 0;
  bookingSuccess = false;
  bookingFailed = false;

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private eventService: EventService,
    private bookingService: BookingService
  ) {
    this.bookingForm = this.formBuilder.group({
      name: ['', [Validators.required, Validators.minLength(2)]],
      email: ['', [Validators.required, Validators.email]],
      phone: ['', [Validators.required, Validators.pattern(/^\d{10}$/)]],
      tickets: [1, [Validators.required, Validators.min(1)]]
    });
  }

  ngOnInit(): void {
    const eventId = Number(this.route.snapshot.params['id']);
    this.eventService.getEvent(eventId).subscribe(event => {
      this.event = event;
      this.updateTotalPrice();
      this.setTicketsMaxValidator();
    });

    this.bookingForm.get('tickets')?.valueChanges.subscribe(() => {
      this.updateTotalPrice();
    });
  }

  updateTotalPrice(): void {
    if (this.event) {
      const tickets = this.bookingForm.get('tickets')?.value || 0;
      this.totalPrice = this.event.ticketPrice * tickets;
    }
  }

  setTicketsMaxValidator(): void {
    if (this.event) {
      const availableTickets = this.event.totalTickets - this.event.bookedTickets;
      const ticketsControl = this.bookingForm.get('tickets');
      ticketsControl?.setValidators([Validators.required, Validators.min(1), Validators.max(availableTickets)]);
      ticketsControl?.updateValueAndValidity();
    }
  }

  onSubmit(): void {
    if (this.bookingForm.valid && this.event) {
      const bookingData = {
        username: this.bookingForm.get('name')?.value,
        email: this.bookingForm.get('email')?.value,
        phoneNumber: Number(this.bookingForm.get('phone')?.value),
        eventId: this.event.id,  // Assuming the event object has an `id` property
        noOfTickets: this.bookingForm.get('tickets')?.value,
        pricePaid: this.totalPrice
      };

      this.bookingService.createBooking(bookingData).subscribe(response => {
        console.log('Booking submitted successfully:', response);
        this.bookingSuccess = true;
      }, error => {
        console.error('Error submitting booking:', error);
        this.bookingFailed = true;
      });
    } else {
      Object.keys(this.bookingForm.controls).forEach(key => {
        const control = this.bookingForm.get(key);
        if (control?.invalid) {
          control.markAsTouched();
        }
      });
    }
  }
}