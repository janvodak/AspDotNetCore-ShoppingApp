# Domain-Driven Design (DDD)

**Domain-Driven Design (DDD)** is a design approach that emphasizes the importance of understanding and modeling the core business domain.
DDD provides a structured way to capture and express domain knowledge, ensuring that the software system closely aligns with real-world business processes.
It plays a significant role in the development of modern software architectures, including Hexagonal Architecture (Onion Architecture), and greatly influences Clean Architecture.

## Key Principles of Domain-Driven Design

1. **Ubiquitous Language**: DDD introduces the concept of a "ubiquitous language" where both technical and non-technical team members use the same terminology to describe the domain.
This shared language helps bridge the communication gap between business experts and developers.

2. **Bounded Contexts**: In DDD, the system is divided into "bounded contexts," each of which represents a specific aspect of the domain.
These bounded contexts contain their own models and are isolated from each other. This helps prevent conflicts and ambiguities when dealing with complex domains.

3. **Entities and Value Objects**: DDD distinguishes between "entities" and "value objects."
Entities represent objects with unique identities, while value objects are objects without distinct identities. This distinction helps in modeling domain concepts effectively.

4. **Aggregates and Repositories**: DDD introduces "aggregates" to group related entities and value objects.
"Repositories" are responsible for retrieving and storing aggregates. This pattern simplifies data access and ensures consistency.

## Implementtion principles and patterns in Domain layer

### Implement domain entities as POCO classes

In crafting the architecture of this system, there was used the principle of implementing domain entities as Plain Old CLR Objects
(POCO classes). These POCO classes serve as the building blocks of the domain model,
embodying the core entities that define the business logic.
Taking the Order class as an example, it not only represents an entity within the domain but also serves as the aggregate root.
Leveraging the Entity base class, I ensure a consistent and reusable foundation for common entity-related functionality.

It's essential to highlight that these base classes and interfaces are not imposed by an external ORM framework like Entity Framework Core;
they are meticulously crafted within the domain model project.
This underscores the autonomy and ownership of the domain model code, distinguishing it from infrastructure-related code.
By adhering to this approach, it is ensured that domain entities are true POCO classes, devoid of direct dependencies
on any specific infrastructure framework. This aligns seamlessly with the principles of Domain-Driven Design (DDD),
where the focus is on expressive, business-centric C# code that encapsulates the intricacies of the domain model.

The principle of Persistence Ignorance (PI) holds that classes modeling the business domain in a software application should not be i
mpacted by how they might be persisted. Thus, their design should reflect as closely as possible the ideal design needed
to solve the business problem at hand, and should not be tainted by concerns related to how the objects' state is saved and later retrieved.
Some common violations of Persistence Ignorance include domain objects that must inherit from a particular base class,
or which must expose certain properties. Sometimes, the persistence knowledge takes the form of attributes that must be applied to the class,
or support for only certain types of collections or property visibility levels.

### Encapsulation of Data in Domain Entities

In the context of domain-driven design (DDD), a crucial aspect of code design involves encapsulating data within domain entities
to safeguard against unintended manipulation that may violate business rules. To maintain control over data consistency and invariants,
it is essential to update entities only through methods within the entity or the constructor.
This means defining properties only with a get accessor and backing them with private fields. D
espite the need for EF Core to set these fields, most attributes remain read-only or private,
ensuring that updates consider business domain invariants and logic specified within the class methods.
It was emphasized that, to adhere to DDD patterns, entities should not have public setters in any property.
Any changes to an entity should be driven by explicit methods with a clear ubiquitous language about the change they are effecting.
Collections within the entity, such as order items, should be read-only properties,
allowing updates only from within the aggregate root class methods or the child entity methods.
This approach ensures that any operation against the entity's data or its child entities is performed through methods,
maintaining consistency and avoiding the pitfalls of implementing transactional script code.

### Persisting Value Objects with Owned Entity Types

Owned entity types in EF Core provide an efficient means to persist value objects within entities.
Despite certain gaps compared to DDD's value object pattern, owned entity types prove optimal for EF Core.
Introduced as a feature in EF Core, these types allow seamless mapping of entities and types without explicit identity,
such as value objects. An owned entity type, acting as a regular class, shares the CLR type with an entity.
Though seemingly identity-less, they possess identity, incorporating the owner's identity and a navigation property.
Collections of owned types introduce an independent component, supported in EF Core.
Declare owned types explicitly, as they aren't discovered by convention.

### Using Domain Events for Explicit Implementation of Side Effects

In Domain-Driven Design, leverage domain events to explicitly implement side effects across multiple aggregates.
This ensures better scalability and minimizes impact on database locks through eventual consistency.
A domain event signifies a past occurrence within the domain, notifying other parts within the same domain to react accordingly.
This explicit expression of side effects enhances maintainability. Domain events aid in expressing domain rules explicitly,
fostering a separation of concerns and alignment with domain experts' ubiquitous language.
It's crucial to ensure atomicity, similar to database transactions, for operations related to a domain event.
Unlike messaging-style events, domain events focus on immediate, in-process side effects within the same domain,
supporting both synchronous and asynchronous execution. Integration events, in contrast, are designed to be asynchronous.

### Utilizing Domain Events for Consistency Across Aggregates

In Domain-Driven Design, employ domain events to trigger side effects across multiple aggregates within the same domain.
For commands impacting one aggregate but necessitating domain rule executions on additional aggregates, use domain events.
Handling domain events in the application layer ensures explicit expression of side effects,
decoupling domain logic from infrastructure concerns like event handlers.
Domain events can trigger diverse application actions and remain open to future expansion. Multiple handlers for the same domain event
in the Application Layer enable various domain rules and actions without affecting existing code,
adhering to the Open/Closed and Single Responsibility Principles from SOLID.

### Choosing Between Single Transaction and Eventual Consistency

Deciding between a single transaction across aggregates and eventual consistency sparks debate in DDD.
Advocates like Eric Evans and Vaughn Vernon favor eventual consistency for scalability.
They suggest resolving dependencies through events or batch processing. On the contrary, some, like Jimmy Bogard,
endorse spanning a transaction across related aggregates. The choice depends on business needs, scalability goals,
and code complexity tolerance. Bogard suggests dispatching domain events just before committing the transaction for in-scope side effects.
The approach, whether eventual consistency or single transaction, hinges on careful consideration of business requirements
and collaboration with domain experts.

## Implementtion principles and patterns in Infrastructure layer

### Data Persistence Components in Microservices

Data persistence components play a crucial role in microservices, offering access to the data stored within a microservice's boundaries,
typically its database. These components encompass the implementation of key elements such as repositories and Unit of Work classes,
often realized through custom Entity Framework (EF) DbContext objects.

### The Repository Pattern
The Repository pattern, a cornerstone of Domain-Driven Design (DDD), serves the purpose of isolating persistence concerns
from the domain model. It introduces one or more persistence abstractions, usually in the form of interfaces defined within the domain model.
Implementations of these interfaces, acting as persistence-specific adapters, reside elsewhere in the application.

Repository implementations consolidate the logic needed to access data sources, providing a centralized and maintainable solution.
In the context of an Object-Relational Mapper (ORM) like Entity Framework, LINQ and strong typing simplify the code,
allowing developers to focus on data persistence logic rather than intricate data access plumbing.

### One Repository per Aggregate Root
An essential practice in microservices adopting DDD patterns is adhering to the principle of creating one repository class
for each aggregate root. This ensures a one-to-one relationship between repositories and aggregate roots,
putting the repositories in charge of the aggregate's invariants and transactional consistency.

While querying databases can be performed through other channels, updates must consistently go through repositories.
This strict control is critical for maintaining the integrity of the data and ensuring that changes align
with the business rules encapsulated in the aggregate roots.

### Enforcing Aggregate Root Relationships
To further reinforce the connection between repositories and aggregate roots, it's beneficial to structure the repository
design to enforce the rule that only aggregate roots should have repositories. This can be achieved by implementing
a generic IRepository base interface, specifying that entities must adhere to the IAggregateRoot marker interface.

### Unit of Work Pattern
The Unit of Work pattern, closely related to the Repository pattern, revolves around managing multiple insert,
update, or delete operations within a single transaction. This pattern becomes especially pertinent when a user action,
such as registration on a website, involves multiple database operations.

In the context of EF, the Unit of Work pattern is executed when the SaveChanges method is called on the DbContext.
This approach improves application performance, reduces the risk of inconsistencies, and minimizes transaction blocking in database tables.

### Repositories and Testing
The Repository pattern significantly aids in testing application logic with unit tests.
By defining repository interfaces in the domain model layer and using Dependency Injection,
developers can implement mock repositories for unit tests.
This decoupled approach facilitates unit testing without requiring database connectivity.

### Custom Repositories: Optional Yet Valuable
While custom repositories prove valuable for many reasons, they are not mandatory components in DDD designs or general .NET development.
Some developers, like Jimmy Bogard, argue against repositories, emphasizing that they might obscure the crucial details
of the underlying persistence mechanism. Choosing to use the Repository pattern or not depends on the specific project requirements
and the preference for alternative patterns such as MediatR for commands.

In summary, data persistence components, incorporating the Repository and Unit of Work patterns,
form a pivotal part of microservices architecture.
The Repository pattern, with its emphasis on maintaining transactional consistency and enforcing aggregate root relationships,
provides a structured approach to data access.
Whether to use custom repositories depends on the project's needs and the development team's preferences,
with a focus on rich domain models and aggregates as the central tenet of DDD.

### Table Mapping in Entity Framework Core

Table mapping is a crucial aspect of Entity Framework Core (EF Core), determining how domain entities relate to database tables.
EF Core relies heavily on conventions to answer questions about table names and primary keys.
By convention, each entity maps to a table named after the DbSet<TEntity> property in the context or, if absent, using the class name.

#### Data Annotations vs. Fluent API

EF Core conventions, including table mapping, can be modified using either data annotations or Fluent API within the OnModelCreating method.
Data annotations are applied directly to entity model classes, a more intrusive approach from a Domain-Driven Design (DDD) perspective.
In contrast, Fluent API offers a cleaner, non-intrusive way to adjust conventions in the data persistence infrastructure layer,
maintaining a clean and decoupled entity model.

#### The Hi/Lo Algorithm

The highlighted code demonstrates the use of the Hi/Lo algorithm for key generation.
This algorithm assigns unique identifiers to table rows without an immediate database storage dependency.
It ensures unique IDs before committing changes, aligning with the Unit of Work pattern.
By obtaining IDs in batches, minimizing database round trips, and generating human-readable identifiers,
the Hi/Lo algorithm provides an efficient and user-friendly key generation strategy, supported in EF Core through the UseHiLo method.

#### Mapping Fields Instead of Properties

EF Core, since version 1.1, enables the direct mapping of columns to fields in entity classes, eliminating the need for properties.
This feature is particularly useful for mapping internal state stored in private fields that do not require external access.
The PropertyAccessMode.Field configuration, showcased in the provided code, demonstrates how to implement this field-based mapping.

#### Shadow Properties in EF Core

Shadow properties in EF Core are properties absent in the entity class model, managed solely within the ChangeTracker class
at the infrastructure level. These properties are hidden from the entity model,
offering a way to include additional information without impacting the model's structure.

In summary, understanding table mapping, utilizing the Hi/Lo algorithm, mapping fields directly,
incorporating shadow properties are key considerations when working with EF Core for effective
data persistence in microservices architectures.

## Implementtion principles and patterns in Appliation layer

In the microservices architecture, managing commands and their handlers is crucial for maintaining transactional consistency
and enforcing business logic. This section discusses the implementation of the Command and Command Handler patterns
within the context of Entity Framework Core (EF Core) and its integration with the overall microservices ecosystem.

### Command Pattern Overview

The **Command pattern** is closely tied to the Command Query Responsibility Segregation (CQRS) pattern.
It defines a request for the system to perform an action that changes the system's state.
Commands are imperative, named with a verb in the imperative mood (e.g., "create" or "update"), and must be processed just once.
Unlike events, commands are not facts from the past; they are requests and can be refused.

Commands can originate from user interactions, such as UI actions, or from process managers directing aggregates to perform specific actions.
A crucial aspect of commands is their need for idempotency to ensure that processing them multiple times does not alter the system's state.

### Command Class

A command is implemented as a class containing data fields or collections necessary to execute a specific action.
It serves as a special Data Transfer Object (DTO), focused on requesting changes or transactions.
Commands are immutable data structures that do not contain behavior.
Naming conventions for commands typically include imperative verbs and relevant aggregate types.

### Command Handler Class

Each command has a corresponding **command handler** class responsible for executing the command.
The command handler plays a vital role in achieving the Single Responsibility Principle (SRP)
by encapsulating the logic related to command execution. It receives a command, validates it,
instantiates the relevant aggregate root, executes the required method on the aggregate, and persists the changes to the database.

The steps involved in a command handler typically include:
1. Receiving the command object (DTO).
2. Validating the command.
3. Instantiating the target aggregate root.
4. Executing the method on the aggregate root.
5. Persisting the new state of the aggregate to the database.

Complexity in command handlers may indicate a need for refactoring, moving domain logic into the domain model (aggregates),
ensuring a clean and maintainable architecture.

Understanding the Command and Command Handler patterns in EF Core is essential for orchestrating transactions,
enforcing business rules, and maintaining a clean separation of concerns in microservices architecture.
By adhering to these patterns, developers can implement scalable and maintainable solutions,
facilitating unit testing, and allowing for future domain logic refactoring without impacting the application or infrastructure layers.

### Implementing Command Process Pipeline in EF Core

#### Command Process Invocation

The question arises of how to invoke a command handler without creating tight coupling.
Two recommended options are explored: utilizing an in-memory Mediator pattern artifact or incorporating an asynchronous message queue.

##### In-Memory Mediator Pattern

In a CQRS approach, an intelligent mediator, acting as an in-memory bus, directs commands to the appropriate handler.
The Mediator pattern simplifies the processing of complex requests in enterprise applications, allowing the addition
of cross-cutting concerns like logging, validation, audit, and security. Decorators and behaviors,
akin to Aspect-Oriented Programming (AOP), enhance the Mediator pattern's flexibility by providing a centralized
and transparent means to apply extra behaviors.

For example, in Order microservice implements behaviors such as the LogBehavior and ValidatorBehavior classes.
These behaviors are seamlessly integrated into the Mediator pattern, ensuring clean and explicit cross-cutting concerns.

##### Message Queues in Command's Pipeline

Another option involves using asynchronous messages via message queues. This approach,
can be combined with the Mediator pattern just before reaching the command handler. Asynchronous messaging,
while offering improved scalability and performance, introduces complexity in handling command results,
especially in communicating success or failure back to the client application.

Asynchronous commands, although beneficial for scaling requirements or inter-microservice communication,
significantly increase system complexity. Due to the lack of a straightforward way to indicate failures,
additional components and custom communication mechanisms may be necessary.
Burtsev Alexey and Greg Young caution against unnecessary complexity in adopting asynchronous commands,
emphasizing their applicability in specific scenarios.

##### Implementation with MediatR

It was decided to use the Mediator pattern for an in-process pipeline, driven by the MediatR open-source library.

MediatR simplifies the processing of in-memory messages like commands, facilitating the application of decorators or behaviors.
The Mediator pattern aids in reducing coupling, isolating concerns, and connecting commands to their respective handlers.

Jimmy Bogard, the creator of MediatR, highlights its value in testing by providing a consistent window into the system's behavior.
The MediatR library is one of several open-source options for implementing the Mediator pattern in .NET.

#### Idempotent Commands

Idempotent commands, in the context of command-driven systems, ensure that executing the same command multiple times produces
the same result as the first execution. These commands have no unintended side effects, making them safe to repeat.
Idempotency is crucial in distributed systems to prevent issues arising from duplicate command execution, ensuring consistency and reliability.

Idempotent commands are implemented by wrapping the business command in a generic IdentifiedCommand,
tracked by a unique ID for ensuring idempotency. The IdentifiedCommandHandler checks for existing IDs to prevent duplicate processing,
ensuring that the command is processed only once.

#### Cross-Cutting Concerns with Behaviors in MediatR

The MediatR-based pipeline allows the application of cross-cutting concerns through behaviors.
The Autofac registration module demonstrates the registration of custom behaviors like LoggingBehavior and ValidatorBehavior.
This extensibility enables the addition of various custom behaviors to the mediator pipeline,
enhancing the flexibility and maintainability of the system.
