# AspDotNetCore-ShoppingApp

## Table of Contents

- [Introduction](#introduction)
- [Installation](docs/installation.md)
- [Usage](docs/usage.md)
- [Design and Architecture](docs/architecture-and-design.md)
	- [Software Architecture](docs/architecture-and-design/software-architecture.md)
	- [Software Design](docs/architecture-and-design/software-design.md)
		- [Implementtion principles and patterns used in DDD](docs/architecture-and-design/domain-driven-design.md)
	- [Microservices Observability, Resilience and Monitoring](docs/architecture-and-design/observability-resilience-monitoring.md)
- [Contributing](docs/contributing.md)

## Introduction

Welcome in ASP.NET Core Web API-based microservices project! This application utilizes a wide range of frameworks and technologies to create a powerful and efficient basis for e-commerce platform.

In this project, there are implemented a set of microservices that cover various e-commerce modules, including Product, Basket, Discount, and Ordering.
These microservices leverage a combination of NoSQL databases (MongoDB, Redis) and Relational databases (PostgreSQL, SQL Server) for data storage.

Here's a glimpse of what you'll find:

- **ASP.NET Core Web Application**: The project includes an ASP.NET Core web application, featuring Bootstrap 4 and Razor templates for a user-friendly interface.

- **Microservices Development**: This project is designed as a collection of microservices, following REST API principles for CRUD (Create, Read, Update, Delete) operations.
This approach is used in specific APIs like Discounts API or Product API which does not play key part in wider context and could be simplified.
Other more important APIs are following different principles described in text belove.

![components_architecture](docs/images/AspDotNetCore-ShoppingApp.drawio.png)

Architecture in general in this repository embraces a monorepo approach housed within a single, unified repository.
Despite the widespread adoption of microservices and distributed systems, decision to maintain a monorepo structure is intentional
and driven by the pursuit of learning and simplicity.

- **Architectural Best Practices**: The project follows these principles, ensuring code quality and maintainability:

	- **Hexagonal Architecture**: The Hexagonal Architecture have been adopted, which promotes a clear separation of concerns and the independence of application layers for better maintainability.

	- **Domain-Driven Design (DDD)**: Our project follows Domain-Driven Design principles, which encourage a clear focus on the business domain, resulting in clean and maintainable code.
	DDD ensures that our code is designed around the core concepts of the application's domain.

	- **Command Query Responsibility Segregation (CQRS)**:
	In application there was implemented CQRS principles to separate the handling of commands (changes to the system's state) from queries (requests for information).
	This separation optimizes the performance and scalability of our microservices.

	- **Clean Architecture**: By adhering to these Clean Architecture principles, there is prioritized flexibility, maintainability, and testability,
	ensuring that our software system remains robust and adaptable to change while focusing on its core business logic.

	- **SOLID Principles**: The SOLID principles (Single Responsibility, Open-Closed, Liskov Substitution, Interface Segregation, Dependency Inversion)
	been used to design clean, maintainable, and extensible code.

- **gRPC Service Integration**: Inter-service synchronization is enabled by consuming the Discount gRPC service to calculate product final prices.

- **Message Queue Integration**: MassTransit and RabbitMQ are used to publish BasketCheckout events, facilitating highly performant inter-service communication.

- **RabbitMQ Message-Broker**: Asynchronous microservices communication is established using the RabbitMQ Message-Broker service,
including a Publish/Subscribe Topic Exchange model and MassTransit abstraction.

- **MediatR, FluentValidation, AutoMapper**: These packages are used to develop CQRS and validation logic for robust application functionality.

- **Ocelot API Gateway**: A microservices API gateway is developed using Ocelot, which aggregates services and enhances routing.

- **Docker Compose**: All microservices are containerized using Docker Compose, simplifying deployment and management.

- **Container Management**: Portainer is used for container management, offering an intuitive user interface to oversee different Docker environments.

- **NoSQL Databases**: Both MongoDB and Redis are used for efficient data storage and retrieval, and they are all containerized within Docker for seamless integration.

- **PostgreSQL Support**: PostgreSQL database connections are made and containerized for an efficient database solution.

- **Entity Framework Core**: SQL Server database connections are established using Entity Framework Core, ensuring smooth data management within Docker containers.

- **Swagger API Documentation**: Swagger Open API is implemented for comprehensive and interactive API documentation.

- **pgAdmin Tools**: For PostgreSQL, pgAdmin, an open-source administration and development platform, enhances database management.

- **Polly policies**: Implement Retry and Circuit Breaker patterns with exponential backoff with IHttpClientFactory and Polly policies

- **Elastic Stack (ELK)**: Implementing Centralized Distributed Logging with Elastic Stack (ELK); Elasticsearch, Logstash, Kibana and SeriLog for Microservices

- **HealthChecks**: Use the HealthChecks feature in back-end ASP.NET microservices

- **Watchdog**: Using Watchdog in separate service that can watch health and load across services, and report health about the microservices by querying with the HealthChecks

This project combines an array of technologies to provide a highly performant and maintainable microservices architecture.
Explore further to understand how these components work together to deliver a robust e-commerce solution.

#### Basket microservice:
* ASP.NET Web API application
* REST API principles, CRUD operations, simple tier (layer) architecture
* **Redis database** connection and containerization
* Consume Discount **Grpc Service** for inter-service sync communication to calculate product final price
* Publish Basket event to Queue with using **MassTransit and RabbitMQ**
  
#### Discount microservice:
* ASP.NET **Grpc Server** application
* Build a Highly Performant **inter-service gRPC Communication** with Basket Microservice
* Exposing Grpc Services with creating **Protobuf messages**
* **PostgreSQL database** connection and containerization

#### Order microservice:
* **Key microservice** where almost all important principles and code designs are implemented
* Implementing **DDD, CQRS, and Clean Architecture** with using Best Practices
* Developing **CQRS with using MediatR, FluentValidation**
* Consuming **RabbitMQ** BasketCheckout event queue with using **MassTransit-RabbitMQ** Configuration
* **SqlServer database** connection and containerization
* Using **Entity Framework Core ORM** and auto migrate to SqlServer when application startup

#### Product microservice: 
* ASP.NET Core Web API application 
* REST API principles, CRUD operations, simple tier (layer) architecture
* **MongoDB database** connection and containerization
* Repository Pattern Implementation
* Swagger Open API implementation	

#### Ocelot API Gateway microservice:
* Implement **API Gateways with Ocelot**
* Sample microservices/containers to reroute through the API Gateways
* Run multiple different **API Gateway/BFF** container types	
* The Gateway aggregation pattern in Shopping.Aggregator

#### ShoppingApp webapp:
* ASP.NET Core Web Application with Bootstrap 5 and Razor template
* Call **Ocelot APIs with HttpClientFactory** and **Polly**

#### Webstatus:
* ASP.NET Core Web application (Model-View-Controller)
* Health check UI based on Watchdog
* Queries other microservices and display information about their health

**Refer the main repository -> https://github.com/janvodak/AspnetMicroservices**
