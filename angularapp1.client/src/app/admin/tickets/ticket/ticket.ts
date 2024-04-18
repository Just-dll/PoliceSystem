export interface Ticket {
  id: number;
  fine: number;
  issueTime: string;
  description: string;
  violatorName?: string | null;
  violatorSurname?: string | null;
}
