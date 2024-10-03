export interface Event {
    id: number,
    eventName: string,
    date: string
    timing: string,
    venue: string,
    description: string,
    ticketPrice: number,
    totalTickets: number,
    bookedTickets: number,
    categoryId: number,
  }

  export interface Booking{
    username: string,
    email: string,
    phoneNumber: string,
    eventId: number,
    noOfTickets: number,
    pricePaid: number
  }

