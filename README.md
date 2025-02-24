# SEO Ranking System

## **IMPORTANT**: Google Search uses a really advanced bot detection system! I try to use **Selenium** to maximize the chance to get the ranking results, but if the results are not returned, please consider switching to a different network!!!

## Overview
SEO Ranking System is a web application designed to scrape search engine results and rank them based on specified keywords. The backend is built using ASP.NET Core 8, while the frontend is developed using React.

## User interface
<img src="./img/result-google.png" width="768">

## High-level architecture
<img src="./img/high-level-architecture.png" width="768">

## High-level design decisions:
- [**Selenium**](https://www.selenium.dev/documentation/overview/) is used because Google's advanced bot detection system makes scraping the HTML content via simple GET requests really hard.
- [**Selenium Grid**](https://www.selenium.dev/documentation/grid/) is used to distribute the load to scrape the search results to **multiple Selenium nodes/workers**, which makes **horizontal scaling** very easy. **Total sessions = (number of Selenium nodes) x (number of concurrent sessions per node, default is 5)**.
- [**Seq**](https://datalust.co/seq) is used to collect, search and analyze logs.
- [**Prometheus**](https://prometheus.io/) collects metrics from the backend every 5 seconds.
- [**Grafana**](https://grafana.com/) is used visualize metrics from Prometheus.
- Currently, the system does not persist data. **TODO** in the future!

## Low-level design details:
### Scraper
<img src="./img/scraper-low-level-design.png" width="768">
<br />

- `ISearchEngineScraper` (Interface): Defines a contract for the search engine scraper with the method:
  + `GetSearchRankings()`: A method that retrieves search rankings.

- `SearchEngineScraperStrategy` (Abstract class): Implements the `ISearchEngineScraper` interface
  + Contains an method GetSearchRankings(), which is implemented as a template method.

- Concrete Search Engine Scraper Strategies: These classes inherit from `SearchEngineScraperStrategy` and override specific steps of the algorithm from the template method:
  + `GoogleSearchEngineScraperStrategy`: Google search rankings.
  + `BingSearchEngineScraperStrategy`: Bing search rankings.

- `SearchEngineScrapeStrategyFactory`: 
  + Provides a method GetSearchEngineScrapeStrategies() that returns the appropriate scraping strategies based on the required search engines (Google, Bing, etc.).
  + This allows for dynamic selection of the search engine strategy at runtime.
  + **If multiple search engines are chosen, the scraping process will run concurrently.**

=> Design patterns used:
  - [Strategy Design Pattern](https://refactoring.guru/design-patterns/strategy)
  - [Template Method Design Pattern](https://refactoring.guru/design-patterns/template-method)
  - [Factory Method Design Pattern](https://refactoring.guru/design-patterns/factory-method)

## How to run
### With Docker
- In the root folder, simply run `docker compose -f docker-compose.yml -f docker-compose.override.yml up -d` and wait until it finishes.
- Frontend will be served via [http://localhost:3000/](http://localhost:3000/).
- Swagger page will be served via [http://localhost:8888/swagger/index.html](http://localhost:8888/swagger/index.html).
- Selenium Grid: [http://localhost:4444/](http://localhost:4444/)
- Seq: [http://localhost:5341/](http://localhost:5341/)
- Grafana: [http://localhost:9999/](http://localhost:9999/):
  + User name: admin
  + Password: admin

### Without Docker
- In the root folder, run `docker compose -f docker-compose.yml -f docker-compose.override.yml up -d seq redis selenium-hub selenium-node-chrome` and wait to start all other services
- To start the backend:
  + Navigate to **/src/Host** folder
  + Run `dotnet restore`, `dotnet build` and then `dotnet run` to start the backend.
  + Swagger page will be served via [http://localhost:8080/swagger/index.html](http://localhost:8080/swagger/index.html).
- To start the frontend:
  + Navigate to **fe** folder
  + Run `npm install` and then `npm run dev` to start the frontend. **Remember** to change the env `VITE_API_URL` to `http://localhost:8080/api`!
  + Frontend will be served via [http://localhost:3000/](http://localhost:3000/).

## Backend
The backend of the SEO Ranking System is implemented using ASP.NET Core 8. It includes various services and middleware to handle scraping, caching, logging, and more.

### Key Components
- **Scraper**: Uses Selenium Grid to distribute the load to scrape the search results to multiple Selenium nodes/workers.
- **Caching**: Implements caching to store and retrieve search results efficiently, support Redis cache and local cache.
- **Logging**: Uses Serilog for logging requests and responses, logs will be pushed to a Seq server.
- **Monitoring**: Metrics data will be pulled by Prometheus every 5 seconds, and a Grafana dashboard is provided to view the metrics.
- **Middleware**: Handles exceptions and logs requests and responses.
- **Testing**: Uses **NUnit** as a unit-testing framework and **NSubstitute** as a mocking library.

### Project Structure: Following Clean Architecture
- `src/Host`: Contains the host for the backend.
- `src/Infrastructure`: Contains the infrastructure services and configurations.
- `src/Core/Application`: Contains the application logic and interfaces.
- `tests/Infrastructure.Tests`: Contains unit tests for the infrastructure services.
- `tests/Application.Tests`: Contains unit tests for the application logic.

## Frontend
The frontend of the SEO Ranking System is built using React. It provides a user interface to input keywords and view the search engine rankings.

### Key Components
- **React**: The main library for building the user interface.
- **Vite**: A build tool that aims to provide a faster and leaner development experience for modern web projects
- **Axios**: Used for making HTTP requests to the backend.
- **Tailwind CSS**: Used for styling the application.

### Project Structure
- `fe/src`: Contains the React components and pages.
- `fe/public`: Contains the public assets.

## Contributing
Contributions are welcome! Please fork the repository and submit a pull request with your changes.
