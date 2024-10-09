export interface EventBooking {
    eventName: string;
    eventLocation: string;
    eventDate: string; // ISO string format for date
    description: string;
    id: string; // Unique identifier for the booking
    name: string; // Name of the user booking the event
    email: string; // Email address of the user
    phoneNumber: number;
    eventId: string; // Unique identifier for the event
    noOfTickets: number; // Number of tickets booked
    pricePaid: number; // Total price for the tickets
}