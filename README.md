# Hacker News API Client

This project is an ASP.NET Core application that serves as a client for the Hacker News API. It retrieves and displays the best stories based on their scores.

## Prerequisites

- [.NET 8.0 SDK](https://dotnet.microsoft.com/download/dotnet/8.0)
- [Visual Studio](https://visualstudio.microsoft.com/) or [Visual Studio Code](https://code.visualstudio.com/) (optional)

## Getting Started

1. Clone this repository to your local machine.

```bash
git clone https://github.com/llmsjraj/Bamboo-card.git
```

## Open the project in your preferred development environment.

-  For Visual Studio, open the HackerNewsApi.sln solution file.
-  For Visual Studio Code, navigate to the project folder.

Configure the Hacker News API settings.

Open the appsettings.json file and update the HackerNewsApi section with your Hacker News API details:
```
{
  "HackerNewsApi": {
    "BaseUrl": "https://hacker-news.firebaseio.com/v0/"
  }
}
```
Build and run the application.

Press F5 in Visual Studio.

Run the following commands in the terminal for Visual Studio Code:
```
dotnet build
dotnet run
```
Open your web browser and navigate to https://localhost:5001/api/hackernews/topstories/{count} to retrieve the best stories. Replace {count} with the desired number of stories.
## API Endpoints
-  Swagger can be accessed at the `/swagger` endpoint.
-  GET /api/hackernews/topstories/{count}: Retrieve the best N stories, where N is specified by the caller.
