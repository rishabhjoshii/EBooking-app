import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { RouterModule } from '@angular/router';
import { ActivatedRoute } from '@angular/router';
import { EventService } from '../../services/event.service';
import { Event } from '../../models/interface/event.interface';

@Component({
  selector: 'app-event-details',
  standalone: true,
  imports: [CommonModule, RouterModule],
  templateUrl: './event-details.component.html',
  styleUrl: './event-details.component.css'
})
export class EventDetailsComponent implements OnInit {
  event: Event | undefined;
  currentImageIndex = 0;
  
  constructor(
    private route: ActivatedRoute,
    private eventService: EventService
  ) {}

  ngOnInit(): void {
    const eventId = Number(this.route.snapshot.params['id']);
    this.eventService.getEvent(eventId).subscribe(event => {
      this.event = event;
    });
  }

  nextImage(): void {
    if (this.event) {
      this.currentImageIndex = (this.currentImageIndex + 1) % this.event.imagePaths.length;
    }
  }

  prevImage(): void {
    if (this.event) {
      this.currentImageIndex = this.currentImageIndex === 0 
        ? this.event.imagePaths.length - 1 
        : this.currentImageIndex - 1;
    }
  }
}