# Software Design

Focus: Software design is primarily concerned with the detailed specifications of how a system or application will be implemented.
It involves making decisions about individual modules, classes, and functions, including their structure, behavior, and interactions.

Scope: Design decisions at this level often involve low-level considerations such as algorithms, data structures, and the internal workings of specific components.
It is more about the "how" of implementation within a specific module or class.

- [Clean Architecture](#clean-architecture)
- [SOLID Principles](#solid-principles)
- [Hexagonal Architecture / Onion Architecture](#hexagonal-architecture--onion-architecture) 
- [Domain-Driven Design (DDD)](#domain-driven-design-ddd)
- [Command Query Responsibility Segregation (CQRS)](#command-query-responsibility-segregation-cqrs)

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
It allows to seamlessly switch from one database system (e.g., Oracle or SQL Server) to another (e.g., Mongo, BigTable, Couchbase) without imposing any constraints on the business rules.
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
