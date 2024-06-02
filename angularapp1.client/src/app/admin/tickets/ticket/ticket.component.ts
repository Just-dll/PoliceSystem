import { Component, Input } from '@angular/core';
import { Ticket } from './ticket';
import { HttpClient, HttpHeaders } from '@angular/common/http';
import { Observable } from 'rxjs';
import { environment } from '../../../../environments/environment.development';

@Component({
  selector: 'app-ticket',
  templateUrl: './ticket.component.html',
  styleUrl: './ticket.component.scss'
})
export class TicketComponent {
  @Input() ticket!: Ticket;

  constructor(private http: HttpClient) {}

  payFine(): void {
    this.sendPaymentRequest(this.ticket.id);
  }

  sendPaymentRequest(ticketId: number): void {
    console.log(ticketId)
    this.http.delete(`${environment.baseApiUrl}/api/Tickets/payFine/${ticketId}`, { withCredentials: true })
      .subscribe(
        (error) => {
          console.error('Error fetching user data:', error);
          console.log(error.constructor)
        }
      );
    //return this.http.post(url, { ticketId: ticketId, amount: this.ticket.fine });
  }

  
}
