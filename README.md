# AsyncAPI Template
This template utilizes .NET 8 Minimal API along with Azure Service Bus.
-   AMQPProcessor.cs functions as a background service within the API; however, it can be moved to a Service Worker for improved scalability.
-   If there are multiple endpoints, consider placing them in an Extensions file to keep Programs.cs more organized.
