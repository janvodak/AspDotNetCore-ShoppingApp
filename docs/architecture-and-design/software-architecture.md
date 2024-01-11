# Software Architecture

Focus: Software architecture, on the other hand, has a broader focus.
It is concerned with the overall structure of a software system, including high-level design decisions that dictate how various components will interact.
It addresses the organization of the entire system.

Scope: Architectural decisions encompass components, their relationships, and the overall layout of the system.
It often involves choosing patterns, frameworks, and defining the major building blocks of the application.

## Table of Contents

- [Event Sourcing and Its Connection with CQRS](#event-sourcing-and-its-connection-with-cqrs)
- [Grpc](#grpc)
- [API Gateway Integration](#api-gateway-integration)
- [Microservices Aggregation Pattern](#microservices-aggregation-pattern)

## Event Sourcing and Its Connection with CQRS

**Event Sourcing** is a software architectural pattern that focuses on capturing and storing every change made to an application's state as a sequence of immutable events.
This approach differs from traditional CRUD (Create, Read, Update, Delete) systems where the current state is directly modified.
Event Sourcing can be seamlessly integrated with the **Command Query Responsibility Segregation (CQRS)** pattern, enhancing the adaptability and scalability of software systems.

To facilitate communication between these microservices,there was adopted a RabbitMQ-based Event Driven Communication approach.
This allows seamless interaction and data exchange between the different modules.

### Key Principles of Event Sourcing

1. **Immutable Event Log**: Event Sourcing maintains an immutable event log that records every state-changing action in the system.
These events serve as a historical record of how the system reached its current state.

2. **Reconstruction of State**: The current state of the system is determined by replaying the events from the event log.
The state can be reconstructed at any point in time, which is invaluable for historical analysis or debugging.

3. **Temporal Modeling**: Events are modeled to represent changes in the system over time.
Events capture the intent and meaning behind state changes, making them a valuable source of historical data.

4. **Scalability**: Event Sourcing can scale horizontally by distributing event processing across multiple components, providing a robust solution for high-throughput systems.

### Connection with CQRS

**CQRS (Command Query Responsibility Segregation)**, as a pattern that separates the handling of commands (writes) and queries (reads),
aligns naturally with Event Sourcing principles. Here's how the two concepts connect:

1. **Separation of Concerns**: CQRS separates command processing (changing the system state) from query handling (reading the system state).
Event Sourcing enables the recording of all state-changing commands as events, neatly fitting the command-handling aspect of CQRS.

2. **Historical Record**: Events produced by Event Sourcing serve as a historical record of all state-changing commands.
CQRS, in its query handling, benefits from the historical event log to provide insights into the system's past states.

3. **Event Publication**: In CQRS, events are typically published after a command is processed.
Event Sourcing is a natural fit for this, as it captures and stores these events for later query purposes.

4. **Event Replay**: Event Sourcing enables event replay, allowing the system to reconstruct its state at any given point in time.
This aligns with CQRS's need to provide historical data and enables time-travel queries.

5. **Scalability**: CQRS's separation of command and query processing can be enhanced with Event Sourcing.
The event log can be processed and distributed separately for commands and queries, allowing for high scalability.

By combining Event Sourcing with CQRS, software systems can achieve a high level of adaptability, scalability, and historical data analysis.
This integration allows for a clear separation of responsibilities between command handling and query processing, making it a powerful pattern for modern software architectures.

## Grpc

In order to enhance the application's functionality, there was introduced gRPC, a modern and efficient Remote Procedure Call (RPC) framework, which is utilized by various services.

**Understanding gRPC:**

gRPC is a high-performance, language-agnostic RPC framework that facilitates efficient communication between various services in a distributed system.
It utilizes Protocol Buffers (protobufs) for data serialization and supports multiple programming languages, making it well-suited for microservices architecture.

**Role as a gRPC Server:**

In this configuration, the "Discount Grpc" service functions as a gRPC server, exposing a set of APIs to enable other services,
such as the Basket API, to request and receive discount information for products.

**Integration with Basket API as a gRPC Client:**

The Basket API acts as a gRPC client, communicating with the "Discount Grpc" service to request discounts for each product in a user's shopping basket.
This client-server interaction ensures efficient discount calculations, resulting in an optimized and responsive user experience.

Utilizing gRPC, the services can communicate seamlessly and perform actions like discount calculations, enhancing the overall functionality of the e-commerce application.

> **Note:** Grpc was introduced for communication between Dicnount and Basket API,
but for testing purpose and simplicity there was introduced separated Discount API, which enable possibility to directly communicate with Discount API and manipulate with discounts.
So there is communication between these two backend services, but there is also separate API, which enables direct manipulation with discounts in one shared database. This is just for learing and testing purpose.

## API Gateway Integration

Ocelot API Gateway stands as a versatile and indispensable component within our microservices architecture. 
Serving as a gateway for our distributed system, Ocelot offers a unified entry point for external clients,
streamlining communication with our microservices.
It acts as a traffic director, intelligently managing and routing incoming requests to their respective destinations.

**Key Features and Use Cases:**

* **Routing and Aggregation:** Ocelot excels in dynamically routing requests to the appropriate microservices based on predefined rules.
It supports route aggregation, enabling the composition of multiple microservices responses into a single,
cohesive response to fulfill complex client requests.

* **Load Balancing:** To ensure optimal performance and resource utilization, Ocelot integrates load balancing mechanisms.
This feature evenly distributes incoming requests across multiple instances of microservices,
enhancing system resilience and responsiveness.

* **Middleware Pipeline:** Ocelot provides a flexible middleware pipeline that allows for customizations
and enrichments of incoming requests or outgoing responses.
This extensibility ensures adaptability to specific project requirements.

* **Security:** With its role as a security perimeter, Ocelot enables the enforcement of security policies and authentication mechanisms.
It acts as a shield for microservices, controlling access and validating requests before they reach the internal components.

* **Caching:** Ocelot includes caching capabilities, optimizing performance by storing and serving frequently requested data.
This enhances response times, reduces the load on microservices, and contributes to a more scalable and responsive system.

* **Rate Limiting:** The gateway incorporates Rate Limiting functionalities,
allowing us to control the rate at which requests are accepted.
This prevents potential abuse and ensures fair usage of resources, contributing to the overall stability of the system.

**Specific Use Case:**

In this specific implementation, Ocelot further extends its capabilities.
By utilizing caching, performance is enhanced and reducing the strain on microservices.
The implementation of Rate Limiting provides with granular control over resource utilization,
preventing abuse and ensuring a fair distribution of resources.

Moreover, Ocelot allows to manage endpoint visibility, enabling to hide certain endpoints from external clients while exposing others.
This fine-tuned control enhances security, encapsulates the complexities of microservices architecture,
and presents a unified and coherent API surface to external consumers.

In essence, Ocelot API Gateway proves to be a versatile and essential tool for our microservices ecosystem.
Its rich feature set, combined with our specific use cases, ensures efficient communication, optimal performance,
and robust security for both internal and external interactions.

## Microservices Aggregation Pattern

The Aggregation Pattern for Microservices is a powerful architectural pattern that enables a service to act as an orchestrator,
aggregating data from multiple microservices to fulfill a specific request.
This pattern is particularly useful in scenarios where a client requires a consolidated view of information from various microservices.

### Key Features:

* **Orchestration:** The aggregating service acts as an orchestrator,
coordinating and making requests to various microservices to gather the required data.

* **Data Combination:** The pattern involves collecting data from different microservices and combining it into a unified response.
This can include merging data, transforming formats, or performing other necessary operations.

* **Optimized Client Interaction:** By consolidating data at the server level,
the Aggregation Pattern reduces the need for clients to make multiple requests to different microservices.
This results in improved efficiency and reduced network overhead.

* **Unified Endpoint:** The aggregating service provides a single endpoint for clients,
abstracting the complexities of interacting with multiple microservices.
This simplifies the client experience and enhances maintainability.

* **Increased Performance:** Aggregating data at the server reduces the number of round-trips between the client
and various microservices, leading to improved performance and responsiveness.

### Intended Use Case: Shopping Aggregation API

In this microservices architecture, the Shopping Aggregation API exemplifies the Aggregation Pattern in action.
This service receives a request for Basket data, orchestrates requests to the Basket API for detailed product information
(from the Product API) and the Order API for order-related data.

The Aggregation Pattern implemented here ensures that clients requesting shopping data receive
a comprehensive response without the need to make separate calls to different microservices.
The Shopping Aggregation API acts as the central point for gathering and combining information,
presenting a unified view of a user's shopping experience.

**Key Features in our Implementation:**

* **Product Information:** By aggregating data from the Product API, the Shopping Aggregation API provides detailed information
about products within a user's basket.

* **Order Details:** The service orchestrates requests to the Order API to include relevant order information in the aggregated response.

* **Simplified Client Interaction:** Clients interacting with the Shopping Aggregation API experience
a simplified and unified endpoint for retrieving comprehensive shopping data.

* **Optimized Performance:** The Aggregation Pattern optimizes performance by consolidating data at the server level,
reducing the number of external calls and enhancing overall system responsiveness.

In summary, the Aggregation Pattern for Microservices, as exemplified by our Shopping Aggregation API,
provides a robust solution for efficiently gathering and presenting data from multiple microservices.
This pattern enhances the client experience, simplifies interactions, and optimizes performance in scenarios
where consolidated information is required.

> **Note:** Aggregation pattern was introduced as a means to simplify communicationn with multiple APIs,
but in reality, this aggregation is currently not used for direct communication between some two services and stay unused. It was created just for learing purpose.

This combination of microservices, databases, event-driven communication, and API gateway integration forms the foundation of this e-commerce solution.
