import { User } from "@auth0/auth0-angular";
import { UserSearchResult } from "../../usersearchresult";

export interface CaseFileModel {
  id: number;
  type: string;
  initiationDate: string;
  connectedPersons: { [key: string]: User[] };
  reports: ReportModel[];
  warrants: WarrantModel[];
}

export interface ReportModel {
  id: number;
  reportedLocation: string | undefined;
  issueTime: string | undefined;
  description: string;
}
  
export interface WarrantModel {
    id: number;
    suspectName?: string; // Optional properties
    suspectSurname?: string; // Optional properties
    description?: string; // Optional properties
    issueDate: string; // Use string type to represent DateOnly
}
