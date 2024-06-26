import { Component, OnInit } from '@angular/core';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Ticket } from './ticket/ticket';
import { TicketComponent } from './ticket/ticket.component';
import { environment } from '../../../environments/environment.development';
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
    
    this.http.get<Ticket[]>(`${environment.baseApiUrl}/api/Tickets/myTickets`)
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
