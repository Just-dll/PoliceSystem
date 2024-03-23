export interface DrivingLicense {
    id: number;
    driver: number;
    issueDate: string; // Assuming issueDate and expirationDate are in ISO 8601 format (e.g., "YYYY-MM-DD")
    expirationDate: string;
  }