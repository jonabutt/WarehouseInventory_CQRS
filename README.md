# Warehouse Inventory

## Overview

**Warehouse Inventory** is a .NET 8 project designed to manage warehouse inventory. The project leverages the CQRS (Command Query Responsibility Segregation) pattern without utilizing the traditional Repository and Unit of Work patterns. Integration testing is performed using Testcontainers library.

## Project Structure
The solution is organized into several layers, each responsible for different aspects of the application:

- **WarehouseInventory.API**: The API layer, responsible for exposing endpoints to interact with the warehouse inventory system.
- **WarehouseInventory.Application**: This layer contains the business logic and the CQRS handlers. It orchestrates commands and queries to manage the inventory.
- **WarehouseInventory.Core**: This layer includes shared constants, enums, and other core utilities that are used across the application.
- **WarehouseInventory.DB**: This is the data layer where the DbContext and data models are defined.
- **WarehouseInventory.Tests**: The integration testing layer, which uses Testcontainers to simulate the database environment.
## Technologies Used
- **.NET 8**: The latest version of the .NET framework.
- **CQRS**: Command Query Responsibility Segregation pattern, used to separate the read and write operations of the application.
- **Entity Framework Core**: For database access and management.
- **Redis**: A caching database for the read store.
- **Testcontainers**: A .NET library that enables lightweight, throwaway instances of Docker containers for testing purposes.

## Getting Started

### Prerequisites

- [.NET 8 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Docker](https://www.docker.com/get-started) (for running Testcontainers)