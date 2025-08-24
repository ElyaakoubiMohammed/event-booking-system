# Event Booking System

A full-stack event booking system built with .NET 9, React, and SQL Server. It demonstrates Clean Architecture, CQRS, and Domain-Driven Design principles in a practical, easy-to-follow way.

## Overview

This project is designed to showcase a well-structured architecture while providing a fully functional event booking application. Key layers:

* **Domain Layer** – Core business entities, domain events, and interfaces.
* **Application Layer** – CQRS handlers, validation, and services.
* **Infrastructure Layer** – Database access, repositories, and external service implementations.
* **API Layer** – REST controllers and app configuration.
* **Frontend** – React TypeScript application with a responsive design.

## Tech Stack

### Backend

* .NET 9
* Entity Framework Core with SQL Server
* MediatR (for CQRS)
* FluentValidation
* Swagger/OpenAPI

### Frontend

* React 18 with TypeScript
* Axios for API calls
* Modern, responsive CSS

### Infrastructure

* Docker & Docker Compose
* SQL Server 2022
* Nginx for serving frontend

## Features

* **Event Management**: Create, view, update, and delete events
* **Clean Architecture**: Separation of concerns and dependency inversion
* **CQRS**: Separate read and write operations
* **Domain Events**: Track important business operations
* **Validation**: Ensure data integrity with FluentValidation
* **Responsive UI**: Works on desktop and mobile
* **API Docs**: Interactive Swagger documentation

## Prerequisites

* Docker and Docker Compose
* Git

## Getting Started

1. Clone the repo:

```bash
git clone https://github.com/ElyaakoubiMohammed/event-booking-system
cd event-booking-system
```

2. Build and run everything using Docker Compose:

```bash
docker-compose up --build
```

3. Open the application:

* Frontend: [http://localhost:3000](http://localhost:3000)
* Backend API: [http://localhost:5050/api/event](http://localhost:5050/api/event)
* Swagger Docs: [http://localhost:5050/swagger](http://localhost:5050/swagger)

## API Endpoints

* `GET /api/event` – List all events
* `GET /api/event/{id}` – Get a single event
* `POST /api/event` – Add a new event
* `PUT /api/event/{id}` – Update an event
* `DELETE /api/event/{id}` – Remove an event

## Project Structure

```
EventBookingSystem/
├── EventBookingSystem.API/          # Web API layer
├── EventBookingSystem.Application/  # Handlers and services
├── EventBookingSystem.Domain/       # Business logic and entities
├── EventBookingSystem.Infrastructure/ # Database and external services
├── EventBookingSystem.Tests/        # Unit tests
├── frontend/                        # React app
├── docker-compose.yml               # Docker configuration
└── README.md
```

## Development

### Running Tests

The project includes unit tests for:

* Command/query handlers
* Repository operations
* Domain logic

```bash
dotnet test
```

### Database Migrations

Entity Framework Core handles migrations automatically on app startup.

## Domain Events

Important actions in the system trigger domain events:

* `EventCreatedEvent` – New event created
* `SeatBookedEvent` – Seat booked
* `SeatCancelledEvent` – Booking cancelled
* `EventCancelledEvent` – Event cancelled
* `EventCompletedEvent` – Event finished

## Validation Rules

* Event title is required
* Event date must be in the future
* Capacity must be greater than zero
* Cannot book more seats than available
* Cancelled or completed events cannot be modified

## Contributing

This project is a showcase of clean architecture, CQRS, and modern .NET development. Contributions that improve functionality, architecture, or documentation are welcome.


Do you want me to do that?
