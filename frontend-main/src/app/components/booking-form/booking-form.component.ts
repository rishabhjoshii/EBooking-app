import { Component, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, FormsModule, ReactiveFormsModule, Validators } from '@angular/forms';
import { Event } from '../../models/interface/event.interface';
import { ActivatedRoute, RouterModule } from '@angular/router';
import { EventService } from '../../services/event.service';
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

  constructor(
    private formBuilder: FormBuilder,
    private route: ActivatedRoute,
    private eventService: EventService
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

  onSubmit(): void {
    if (this.bookingForm.valid) {
      console.log('Booking submitted:', this.bookingForm.value);
      this.bookingSuccess = true;
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