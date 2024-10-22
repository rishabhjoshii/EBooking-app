import { Component, OnInit } from '@angular/core';
import { EventService } from '../../services/event.service';
import { Event } from '../../models/interface/event.interface';
import { RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { MatDatepickerModule } from '@angular/material/datepicker';
import { MatInputModule } from '@angular/material/input';
import { MatNativeDateModule } from '@angular/material/core';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-event-list',
  standalone: true,
  imports: [RouterModule, CommonModule, MatDatepickerModule, MatInputModule, MatNativeDateModule, FormsModule],
  templateUrl: './event-list.component.html',
  styleUrl: './event-list.component.css'
})
export class EventListComponent implements OnInit {
  events: Event[] = [];
  currentImageIndex: { [key: number]: number } = {};
  categories: string[] = ['Music', 'Comedy', 'Art'];
  selectedCategory: string | null = null;
  startDate: Date | null = null;
  endDate: Date | null = null;
  searchText: string = '';
  error: string = '';

  constructor(private eventService: EventService) {}

  ngOnInit(): void {
    this.loadEvents();
  }

  loadEvents(): void {
    this.eventService.getFilteredEvents(this.selectedCategory, this.startDate || undefined, this.endDate || undefined)
      .subscribe(events => {
        this.setEvents(events);
      });
  }

  setEvents(events: Event[]): void {
    this.events = events;
    this.events.forEach(event => {
      this.currentImageIndex[event.id] = 0;
    });
  }

  filterEvents(): void {
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

  // searchEvents(): void {
  //   if (this.searchText.trim()) {
  //     this.eventService.searchEvents(this.searchText)
  //       .subscribe(events => {
  //         this.setEvents(events);
  //       });
  //   }
  // }

  searchEvents(): void {
    if (this.searchText.trim()) {
      this.eventService.searchEvents(this.searchText).subscribe({
        next: (events) => {
          this.setEvents(events);
        },
        error: (err) => {
          console.log("search error: ", err);
          this.error = err.error;
        }
      });
    }
  }
}