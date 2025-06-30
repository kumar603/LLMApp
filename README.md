# LLMApp

LLMApp is an ASP.NET MVC 4.8 web application that integrates with Large Language Model (LLM) APIs such as OpenAI and Azure OpenAI. It allows users to manage AI providers, models, and prompts, execute prompts against selected models, and view results within the application.

## Features

- ASP.NET MVC 4.8 frontend with Razor views
- Entity Framework Code First with SQL Server
- CRUD operations for Providers, Models, and Prompts
- Integration with external LLM APIs using HttpClient
- Prompt execution and result display
- Learning suggestions and next-step tips
- Modular architecture with separate projects for data access and services

## Technologies Used

- ASP.NET MVC 4.8 (.NET Framework)
- Entity Framework 6 (Code First)
- SQL Server
- HttpClient for API integration
- NUnit/xUnit for unit testing

## Getting Started

1. Clone the repository
2. Configure the connection string in `Web.config`
3. Run EF migrations to create the database
4. Start the application in Visual Studio

## Folder Structure

- `LLMApp.Web` - MVC application (controllers, views)
- `LLMApp.Data` - EF models and DbContext
- `LLMApp.Services` - Business logic and API integration
- `LLMApp.Tests` - Unit tests

## License

This project is licensed under the MIT License.
