// export interface Event {
//   id: number,
//   eventName: string,
//   date: string
//   timing: string,
//   venue: string,
//   description: string,
//   ticketPrice: number,
//   totalTickets: number,
//   bookedTickets: number,
//   categoryId: number,
//   imagePaths: string[],
// }

export interface Event {
  id: number;
  eventName: string;
  date: string;
  timing: string;
  venue: string;
  description: string;
  categoryId: number;
  imagePaths: string[];
  ticketTypes: TicketType[];
  organiserDetails: OrganiserDetails;
}

export interface TicketType {
  ticketTypeName: string;
  ticketPrice: number;
  totalTickets: number;
  bookedTickets: number;
}

export interface OrganiserDetails {
  userName: string;
  email: string;
  firstName: string;
  lastName: string;
  phoneNumber: string;
  preferredCurrency: string | null;
  preferredLanguage: string | null;
  profileImageUrl: string | null;
}


