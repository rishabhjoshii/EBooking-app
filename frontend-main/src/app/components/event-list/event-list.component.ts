// import { Component, OnInit } from '@angular/core';
// import { EventService } from '../../services/event.service';
// import { Event } from '../../models/interface/event.interface';
// import { RouterModule } from '@angular/router';
// import { CommonModule } from '@angular/common';
// import { Observable } from 'rxjs';

// @Component({
//   selector: 'app-event-list',
//   standalone: true,
//   imports: [RouterModule,CommonModule],
//   templateUrl: './event-list.component.html',
//   styleUrl: './event-list.component.css'
// })

// export class EventListComponent implements OnInit {
//   events: Event[] = [];
//   currentImageIndex: { [key: number]: number } = {}; // Track current image index for each event

//   constructor(private eventService: EventService) {}

//   ngOnInit(): void {
//     this.eventService.getEvents().subscribe(events => {
//       this.events = events;
//       // Initialize current image index for each event
//       this.events.forEach(event => {
//         this.currentImageIndex[event.id] = 0; // Set initial index to 0
//       });
//     });
//   }

//   prevImage(event: Event): void {
//     const index = event.id;
//     this.currentImageIndex[index] = (this.currentImageIndex[index] - 1 + event.imagePaths.length) % event.imagePaths.length;
//   }

//   nextImage(event: Event): void {
//     const index = event.id;
//     this.currentImageIndex[index] = (this.currentImageIndex[index] + 1) % event.imagePaths.length;
//   }
// }


import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { Event } from '../../models/interface/event.interface';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [RouterModule, CommonModule],
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.css'
})
export class EventListComponent implements OnInit {
  events: Event[] = [];
  currentImageIndex: { [key: number]: number } = {};
  categories: string[] = ['Music', 'Comedy', 'Art'];
  selectedCategory: string | null = null;

  constructor(private eventService: EventService) {}

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(): void {
    if (this.selectedCategory) {
      this.eventService.getFilteredEvents(this.selectedCategory).subscribe(events => {
        this.setEvents(events);
      });
    } else {
      this.eventService.getEvents().subscribe(events => {
        this.setEvents(events);
      });
    }
  }

  setEvents(events: Event[]): void {
    this.events = events;
    this.events.forEach(event => {
      this.currentImageIndex[event.id] = 0;
    });
  }

  filterEvents(category: string | null): void {
    this.selectedCategory = category;
    this.loadEvents();
  }

  prevImage(event: Event): void {
    const index = event.id;
    this.currentImageIndex[index] = (this.currentImageIndex[index] - 1 + event.imagePaths.length) % event.imagePaths.length;
  }

  nextImage(event: Event): void {
    const index = event.id;
    this.currentImageIndex[index] = (this.currentImageIndex[index] + 1) % event.imagePaths.length;
  }
}
