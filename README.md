# Police Management System Project

## Overview

This project is a comprehensive system designed to manage the workflow of the judiciary system using ASP.NET and Angular. The system includes features for handling case files, tickets, users and real time updates.

## Features

1. **Case File Management**
    - Create and manage case files.
    - Request order for person arrest.

2. **Ticket issuing**
    - Issuing a ticket to the person and retreive payment from them.
    - Initiate a case file opening in case of ignoring the payment requirements.

2. **Notification Service**
    - Notifies users.
    - Sends notifications to users via various channels (website, phone, email) using RabbitMQ.

3. **Real-time Updates**
    - Uses Server-Sent Events (SSE) for real-time notifications.
    - Integrates RabbitMQ to dequeue messages and open SSE connections with the frontend.

4. **User Authentication and Authorization**
    - Uses cookies for authentication and authorization.
    - Retrieves user roles via a gRPC service connected to Identity Server.
    - Handles login through `{environment.baseApiUrl}/login`.

6. **Microservices**
    - Considers the use of .NET Aspire as orchestrator.

## Technologies

- **Backend**: ASP.NET, RabbitMQ, EF Core, gRPC
- **Frontend**: Angular
- **Authentication**: Cookies, Identity Server
- **Orchestration**: .NET Aspire

## Installation

- Ensure you have .NET installed.
- Clone the repository.
- Navigate to the AngularApp1.AppHost project
- Restore the dependencies: `dotnet restore`
- Build the project: `dotnet build`
- Run the project: `dotnet run`


## Contributing

1. Fork the repository.
2. Create a new branch: `git checkout -b feature-branch`
3. Make your changes and commit them: `git commit -m 'Add some feature'`
4. Push to the branch: `git push origin feature-branch`
5. Submit a pull request.

## License

This project is licensed under the MIT License. See the LICENSE file for details.