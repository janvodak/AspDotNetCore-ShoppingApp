## Clean Architecture

By adhering to these Clean Architecture principles, there is prioritized flexibility, maintainability, and testability,
ensuring that our software system remains robust and adaptable to change while focusing on its core business logic.

1. **Independent of Frameworks**: Architecture is designed to be independent of specific frameworks or libraries.
It does not rely on the existence of a particular library or feature-laden software. This flexibility allows us to view frameworks as tools to support our system,
rather than constraints that dictate how the system should operate.

2. **Testable**: The architecture is built with testability in mind. It enables to test the core business rules in isolation, without the need for the user interface,
database, web server, or any other external element. This makes the testing process efficient and reliable.

3. **Independent of UI**: Architecture ensures that the user interface (UI) is decoupled from the core business logic.
This means that the UI can be changed or replaced easily without affecting the rest of the system. For instance,
it can transition from a web-based UI to a console-based UI without altering the underlying business rules.

4. **Independent of Database**: The architecture is designed to be agnostic of the underlying database technology.
It allows to seamlessly switch from one database system (e.g., Oracle or SQL Server) to another (e.g., Mongo, BigTable, CouchDB) without imposing any constraints on the business rules.
Business logic remains free from database-specific dependencies.

5. **Independent of External Agencies**: Business rules have no knowledge of or dependencies on external agencies or the outside world.
This independence ensures that the business logic is solely focused on the core functions of the application, making it adaptable and maintainable in various contexts.

## SOLID Principles

The **SOLID** principles are a set of five design principles in object-oriented programming that aim to create more maintainable and scalable software.
These principles can be seamlessly integrated with architectural principles like Domain-Driven Design (DDD), Hexagonal Architecture, Clean Architecture,
and Command Query Responsibility Segregation (CQRS) to build robust and adaptable software systems.

1. **Single Responsibility Principle (SRP)**: A class should have only one reason to change, meaning it should have a single responsibility.
This principle promotes a clean separation of concerns.

2. **Open-Closed Principle (OCP)**: Software entities (classes, modules, functions) should be open for extension but closed for modification.
This encourages extending functionality through new code rather than altering existing code.

3. **Liskov Substitution Principle (LSP)**: Subtypes must be substitutable for their base types without altering the correctness of the program.
This principle ensures that derived classes can be used interchangeably with their base classes.

4. **Interface Segregation Principle (ISP)**: Clients should not be forced to depend on interfaces they do not use.
It encourages the creation of small, specific interfaces rather than large, monolithic ones.

5. **Dependency Inversion Principle (DIP)**: High-level modules should not depend on low-level modules. Both should depend on abstractions.
This principle promotes the use of abstractions to decouple components and establish a clear separation of concerns.

### Connection with Architectural Principles

**1. Connection with Domain-Driven Design (DDD)**

SOLID principles, especially the Single Responsibility Principle, align with DDD's emphasis on a clean separation of concerns within the domain.
DDD encourages domain models with a single responsibility, which fits well with SRP.
The use of abstractions in DDD and SOLID's Dependency Inversion Principle ensures that high-level domain policies don't depend on low-level implementation details.

**2. Connection with Hexagonal Architecture (Onion Architecture)**

In Hexagonal Architecture, the SOLID principles play a pivotal role in shaping the clean separation of concerns.
Hexagonal Architecture promotes SOLID's Open-Closed Principle, allowing for extensions without modifying existing components.
The Dependency Inversion Principle is essential in Hexagonal Architecture, enabling high-level policies to depend on abstractions, not concrete implementations.

**3. Connection with Clean Architecture**

Clean Architecture emphasizes SOLID principles to ensure maintainability, testability, and adaptability.
The Single Responsibility Principle aligns with Clean Architecture's focus on clear separations, making components more modular and easier to test.
The Dependency Inversion Principle is vital for decoupling high-level policies from low-level details.

**4. Connection with Command Query Responsibility Segregation (CQRS)**

CQRS leverages SOLID principles, such as the Single Responsibility Principle, to separate the handling of commands and queries.
Each of these responsibilities can adhere to SRP and other SOLID principles. Dependency Inversion is also relevant in CQRS, allowing high-level policies to depend on abstractions.

By integrating the SOLID principles with architectural principles like DDD, Hexagonal Architecture, Clean Architecture, and CQRS,
software systems benefit from maintainable, adaptable, and testable designs. These combined principles contribute to the creation of robust and efficient applications.

## Hexagonal Architecture / Onion Architecture

Hexagonal Architecture, also known as Onion Architecture, is a software architectural pattern that emphasizes clear and well-defined boundaries.
This architectural approach, influenced by Alistair Cockburn, aims to create adaptable, maintainable, and testable applications.

### Key Principles

1. **Core Business Logic**: Hexagonal Architecture places the core business logic at the center of the system.
This core represents the essential functions and operations and is isolated from external concerns.

2. **Ports and Adapters**: The architecture defines clear boundaries, known as "ports and adapters," between the core business logic and external components.
Ports define interfaces through which the core interacts with the external world, and adapters implement these interfaces.

3. **Dependency Inversion**: Hexagonal Architecture follows the Dependency Inversion Principle, ensuring that high-level policies and abstractions do not depend on low-level details.
The core does not depend on specific technologies or frameworks, with dependencies inverted to allow external components to depend on the core.

4. **Testability**: The architecture emphasizes testability. By isolating the core from external dependencies, it becomes easier to unit test the core without complex setups or external services.

5. **Independence of Frameworks**: Hexagonal Architecture is designed to be independent of frameworks or libraries,
offering flexibility for evolving with changing technology and framework replacements without altering the core business logic.

### Layers in Hexagonal Architecture

**1. Domain Layer**: This is the innermost layer, where the core business logic resides. It encapsulates the fundamental concepts, entities,
value objects, and domain services. The Domain Layer represents the heart of the application, defining what the system is and its fundamental rules.
It remains completely isolated from external concerns, ensuring that business logic is independent and focused on the core domain.

**2. Application Layer**: Sitting around the Domain Layer, the Application Layer acts as an intermediary between the core business logic and external components.
It orchestrates the use cases, which are the interactions between external actors and the domain.
The Application Layer is responsible for defining and exposing ports through which external components can interact with the core.
It maintains the use case logic and coordinates the flow of data between the domain and the external world.

**3. Infrastructure Layer**: The outermost layer is the Infrastructure Layer, which contains all the components that interact with the external world.
This layer includes user interfaces, databases, frameworks, and external services. Adapters are responsible for implementing the ports defined by the Application Layer.
The Infrastructure Layer also manages cross-cutting concerns such as configuration, logging, and external dependencies.

### Benefits

- **Adaptability**: Hexagonal Architecture enables easy adaptation to changing requirements and technologies, making the system robust against future changes.

- **Maintainability**: With clear boundaries and a focus on the core business logic, the architecture promotes maintainability and code readability.

- **Flexibility**: Independence from external technologies and frameworks makes the architecture versatile and adaptable to different use cases.

- **Test-Driven Development**: Hexagonal Architecture simplifies test writing and ensures thorough testing of core functionality.

- **Clean Separation of Concerns**: The architecture enforces a clean separation of concerns, reducing system complexity.

Incorporating Hexagonal Architecture into your application can result in a software system that is adaptable, highly maintainable, and testable.
It encourages a focus on the core business logic while isolating external dependencies, leading to a clean and organized codebase.

## Domain-Driven Design (DDD)

**Domain-Driven Design (DDD)** is a design approach that emphasizes the importance of understanding and modeling the core business domain.
DDD provides a structured way to capture and express domain knowledge, ensuring that the software system closely aligns with real-world business processes.
It plays a significant role in the development of modern software architectures, including Hexagonal Architecture (Onion Architecture), and greatly influences Clean Architecture.

### Key Principles of Domain-Driven Design

1. **Ubiquitous Language**: DDD introduces the concept of a "ubiquitous language" where both technical and non-technical team members use the same terminology to describe the domain.
This shared language helps bridge the communication gap between business experts and developers.

2. **Bounded Contexts**: In DDD, the system is divided into "bounded contexts," each of which represents a specific aspect of the domain.
These bounded contexts contain their own models and are isolated from each other. This helps prevent conflicts and ambiguities when dealing with complex domains.

3. **Entities and Value Objects**: DDD distinguishes between "entities" and "value objects."
Entities represent objects with unique identities, while value objects are objects without distinct identities. This distinction helps in modeling domain concepts effectively.

4. **Aggregates and Repositories**: DDD introduces "aggregates" to group related entities and value objects.
"Repositories" are responsible for retrieving and storing aggregates. This pattern simplifies data access and ensures consistency.

### Connection with Hexagonal Architecture

**Hexagonal Architecture (Onion Architecture)** borrows concepts from DDD to structure software systems.
DDD's focus on a clean separation of concerns, the definition of core domain models, and clear boundaries aligns well with Hexagonal Architecture's principles.
The Domain Layer in Hexagonal Architecture closely resembles the core domain models in DDD. It isolates the core business logic, ensuring that it remains independent of external concerns.

### Influence in Clean Architecture

DDD's influence extends to Clean Architecture as well. Clean Architecture emphasizes the separation of concerns,
and DDD provides the modeling and domain-specific knowledge to achieve this separation effectively. By modeling the core domain and encapsulating it within the Domain Layer,
Clean Architecture ensures that business logic is at the center while being isolated from external dependencies and technologies. This results in a clean and maintainable codebase.

Incorporating DDD principles in conjunction with Hexagonal and Clean Architectures promotes a clear understanding of the core business domain, leading to adaptable,
maintainable, and testable software systems. It ensures that the software closely reflects the real-world domain it serves, and this alignment is a crucial aspect of modern software design.

## Command Query Responsibility Segregation (CQRS)

**Command Query Responsibility Segregation (CQRS)** is an architectural pattern that separates the handling of commands
(changes to the system's state) from queries (requests for information) within a software system.
CQRS promotes a clear division of responsibilities and can be seamlessly integrated with architectural principles like Domain-Driven Design (DDD), Hexagonal Architecture, and Clean Architecture.

### Key Principles of CQRS

1. **Command and Query Separation**: CQRS separates the responsibilities for commands (writing data) and queries (reading data).
Commands modify the state of the system, while queries retrieve information.

2. **Specialized Models**: CQRS often involves the use of specialized models for commands and queries.
These models are tailored to the specific requirements of each responsibility.

3. **Event Sourcing**: Event sourcing is a common technique in CQRS where all changes to the system's state are captured as a series of events.
This event log serves as the source of truth, and the current state can be reconstructed by replaying these events.

### Connection with Architectural Principles

**1. Connection with Domain-Driven Design (DDD)**

CQRS complements DDD principles by providing a clear structure for handling domain events. In DDD, the domain model focuses on the core business logic,
while CQRS efficiently manages and processes changes to this core. This separation allows for a more focused and scalable approach to handling commands and queries in complex domains.

**2. Connection with Hexagonal Architecture (Onion Architecture)**

CQRS aligns with the principles of Hexagonal Architecture by emphasizing the separation of concerns. In Hexagonal Architecture,
the Application Layer often coordinates the execution of commands and queries, which is a perfect fit for the CQRS pattern.
The core domain logic remains at the center of the architecture, while the application layer handles the segregation of command and query responsibilities.

**3. Connection with Clean Architecture**

Clean Architecture places a strong emphasis on the separation of concerns and independence from external frameworks and technologies.
CQRS neatly fits into Clean Architecture by providing a structured way to separate the responsibilities of writing and reading data.
This clean separation ensures that the core business logic remains unaffected by external concerns and technology choices.

By incorporating CQRS into architectural principles like DDD, Hexagonal Architecture, and Clean Architecture,
software systems benefit from a clear separation of responsibilities, improved scalability, and a robust approach to handling commands and queries.
These principles collectively contribute to the development of adaptable, maintainable, and testable applications.

## Event Sourcing and Its Connection with CQRS

**Event Sourcing** is a software architectural pattern that focuses on capturing and storing every change made to an application's state as a sequence of immutable events.
This approach differs from traditional CRUD (Create, Read, Update, Delete) systems where the current state is directly modified.
Event Sourcing can be seamlessly integrated with the **Command Query Responsibility Segregation (CQRS)** pattern, enhancing the adaptability and scalability of software systems.

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

## Communication between Microservices:

To facilitate communication between these microservices,there was adopted a RabbitMQ-based Event Driven Communication approach.
This allows seamless interaction and data exchange between the different modules.

### Grpc

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

### API Gateway Integration:

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

### Key Features and Use Cases:

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

### Specific Use Case: Shopping Aggregation API

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

This combination of microservices, databases, event-driven communication, and API gateway integration forms the foundation of this e-commerce solution.
