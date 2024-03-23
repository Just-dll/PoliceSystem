import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Ticket } from './ticket/ticket';
import { TicketComponent } from './ticket/ticket.component';
@Component({
  selector: 'app-tickets',
  templateUrl: './tickets.component.html',
  styleUrls: ['./tickets.component.scss']
})
export class TicketsComponent implements OnInit {
  tickets: Ticket[] = [];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.fetchTickets();
  }

  fetchTickets() {
    this.http.get<Ticket[]>('http://localhost:7265/api/tickets')
      .subscribe(
        (response) => {
          this.tickets = response;
        },
        (error) => {
          console.error('Error fetching tickets:', error);
        }
      );
  }
}
