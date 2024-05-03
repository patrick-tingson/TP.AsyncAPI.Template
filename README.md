### Modern and Scalable RESTful Web Services with .NET 8 Minimal API and Azure Service Bus
Welcome to the repository for a pre-built codebase that kickstarts your journey into developing modern and scalable RESTful web services using .NET 8 Minimal API and Azure Service Bus. The code adheres to an N-Tier architecture.

------------

##### Overview
This project provides a solid foundation for building asynchronous RESTful web services. By leveraging .NET 8 Minimal API, you'll benefit from its simplicity and performance while developing robust APIs. Azure Service Bus is integrated into the solution for seamless messaging and event-driven architecture.

------------

##### Components
- AMQPProcessor.cs serves as a critical component within this solution, acting as a background service within the API. It's designed to handle asynchronous processing efficiently. For further scalability improvements, consider relocating this component to a Service Worker.
- To keep the Programs.cs file organized, especially when dealing with multiple endpoints, consider placing them in an Extensions file. This ensures a clean and maintainable structure as your project grows.

------------

##### License
This project is licensed under the MIT License, which means you're free to use, modify, and distribute the code for both commercial and non-commercial purposes.

------------

##### Acknowledgements
Special thanks to the creators of .NET 8 Minimal API and Azure Service Bus for providing the tools necessary to build modern and scalable web services.

------------

### End
