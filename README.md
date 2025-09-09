# Correspondence Document Management System

A robust document management system built with .NET Core 9 and MS SQL Server, designed to streamline, organize, and secure all organizational correspondence documents. 
It supports uploading, categorizing, searching, versioning, 
and tracking the lifecycle of all official communication documents.

## Technologies Used
| Technology            | Description                     |
| --------------------- | ------------------------------- |
| .NET Core 9           | Backend Web Server Side         |
| MS SQL Server         | Relational database for storage |
| Entity Framework Core | ORM for data access             |
| ASP.NET Core Web      | Repository API architecture     |
| JWT / OAuth2          | Authentication (if applicable)  |
| FluentValidation      | Input validation                |
| AutoMapper            | Model mapping                   |


## Project Structure
```plaintext
/CorrespondenceDocumentManagement
│
├── StreamLinerApp                 # ASP.NET Core Web 
├── StreamLinerDL                  # EF Core, DB Context, Migrations
├── StreamLinerEntitiesLayer       # Models , Entities
├── StreamLinerLL                  # Core Business Logic 
├── StreamLinerRepositoryLayer     # Shared Repository , Interfaces
├── StreamLinerViewModelLayer      # DTOs
├── StreamLinerApp.sln             # (Optional) Authentication/Authorization
└── README.md
```
## Features

- Upload, Create, Scan Documents 
- manage incoming/outgoing correspondence
- Document versioning and history
- Generate reports and logs
- Sync Calendar with outlook
- Manage Tasks

## Configuration
- .NET Core SDK 9.0+
- MS SQL Server 2022
- Visual Studio 2022+ / VS Code
- SQL Server Management App

## Environment Variables
  "ConnectionStrings":
    "DefaultConnection": "Server=.;Database=CorrespondenceDB;Trusted_Connection=True;"

## Developed by Streamliner
- Hellana Adel
- Mariam Safwat
- Christina Ayad

## Repository
- GitHub: https://github.com/StreamsGitHub/NewStreamLinerV2.0.git
- 


  






