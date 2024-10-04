This project showcases the three primary lifecycles of objects in an Inversion of Control (IoC) container: Singleton, Scoped, and Transient. Each lifecycle manages object creation and disposal in different ways, illustrating how they affect application behavior and resource management.
1. Singleton

    Definition: A Singleton object is created once and shared throughout the application's lifetime. Every request for this service returns the same instance.
    Use Case: This lifecycle is ideal for stateless services or when you want to maintain a single shared state, such as caching or configuration settings.
    Example in the Project: A logging service that keeps a single instance to log messages consistently across the application.

2. Scoped

    Definition: A Scoped object is created once per request (or per scope). It is shared within the same request but not across different requests.
    Use Case: This lifecycle is beneficial for services that require sharing data during a single request, such as database contexts or services that need to maintain request-specific information.
    Example in the Project: A database context that manages database connections and transactions, ensuring that all operations during a single request use the same context.

3. Transient

    Definition: A Transient object is created each time it is requested. Every request for this service results in a new instance.
    Use Case: This lifecycle is suitable for lightweight, stateless services where each operation is independent, such as helper services or utilities.
    Example in the Project: A service that generates unique identifiers or temporary data where a fresh instance is needed for each operation.
