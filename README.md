# 🚗 Vehicle Bid Calculator

## 🚀 Quick start with Docker (using Docker Compose)


```bash
# Clone the repository
git clone https://github.com/habartolom/Bid-Calculator.git
cd Progi

# Start application
docker-compose up -d --build
```
### Services

| Service           | URL                                             | Description                |
|-------------------|-------------------------------------------------|----------------------------|
| **Frontend**      | http://localhost:3000                           | User Interface - Vue.js    |
| **Backend API**   | http://localhost:8080                           | API REST .NET              |
| **Health Check**  | http://localhost:8080/api/BidCalculator/health  | Backend Health endpoint    |
| **PostgreSQL**    | localhost:5432                                  | Database                   |

###  PostgreSQL
- **Puerto**: 5432
- **Base de datos**: `bidcalculator`
- **Usuario**: `bidcalc_user`
- **Contraseña**: `bidcalc_password`

## 🛠️ Tecnologies Used
### Backend
- **.NET 8** - Core Framework
- **ASP.NET Core** - Web API
- **Entity Framework Core** - ORM
- **PostgreSQL** - Database
- **MediatR** - CQRS Pattern
- **FluentValidation** - Data Validation
- **xUnit** - Testing

### Frontend
- **Vue.js 3** - UI Framework
- **TypeScript** - Static Typing
- **Vite** - Build tool
- **Pinia** - State management
- **Vue Router** - Routing
- **Tailwind CSS** - Styles
- **Vitest** - Testing
- **Axios** - HTTP client

## 📋 Features
### Backend
- Clean Architecture
- CQRS Pattern with MediatR
- Robust validation with FluentValidation
- Persistence with Entity Framework Core
- Health checks configured
- Unit and integration tests
- Complete documentation
- Docker support

### Frontend
- Modern and responsive UI
- Mobile-first design
- Multi-screen support (14" to 34" ultra-wide)
- Reusable composables
- Centralized state with Pinia
- Reusable base components
- BEM pattern for CSS
- Unit and integration tests
- Documentation for Angular developers
- Docker support with Nginx

## The Bid Calculation Tool

### Objective
The purpose of the following exercise is to assess a programmer's ability to develop a minimum viable product. Note that this exercise does not simulate a real work situation. The objective is to develop a simple application using good programming practices.

### Details
- We expect a web application
- Since we are looking for a full-stack developer, you need to provide one backend and one frontend communicating together
- The programming language to use for the backend is the one specified in the job description
- The frontend (UI) should be built using an appropriate framework (ideally Vue.js)
- The code must be submitted via GitHub

### Evaluation Criteria
The final solution will be evaluated according to the following criteria, taking into account the expected level of experience:
- Clarity and readability of code, formatting, naming conventions, etc.
- Algorithm and calculation results
- Use of Object-Oriented Programming principles
- Implementation of good software architecture practices (Clean Code, SOLID, KISS, DRY, YAGNI, etc.)
- Proper use of frameworks, tools, and libraries related to the programming language used
- Implementation of unit tests

> **IMPORTANT:** If you make compromises on certain aspects of your code, please add a descriptive text by e-mail or in a comment, explaining what you would improve to make your code production-ready.

### Task Description
Develop an application that will allow a buyer to calculate the total price of a vehicle (common or luxury) at a car auction. The software must consider several costs in the calculation. The buyer must pay various fees for the transaction, all of which are calculated on the base price amount. The total amount calculated is the winning bid amount (vehicle base price) plus the fees based on the vehicle price and vehicle type. **Fees must be dynamically computed.**

### Requirements
- There is a field to enter the vehicle base price
- There is a field to specify the vehicle type (Common or Luxury)
- The list of fees and their amount are displayed
- The total cost is automatically computed and displayed every time the price or type changes

### Fixed and Variable Costs
- **Basic buyer fee:** 10% of the price of the vehicle  
  - **Common:** minimum $10 and maximum $50  
  - **Luxury:** minimum $25 and maximum $200
- **The seller's special fee:**  
  - **Common:** 2% of the vehicle price  
  - **Luxury:** 4% of the vehicle price
- **Added costs for the association based on the price of the vehicle:**  
  - $5 for an amount between $1 and $500  
  - $10 for an amount greater than $500 up to $1000  
  - $15 for an amount greater than $1000 up to $3000  
  - $20 for an amount over $3000
- **A fixed storage fee of $100**

### Calculation Example
- Vehicle Price (Common): **$1000**
- Basic buyer fee: **$50** (10%, min: $10, max. $50)
- Special fee: **$20** (2%)
- Association fee: **$10**
- Storage fee: **$100**
- **Total: $1180 = $1000 + $50 + $20 + $10 + $100**

### Test Cases

| Vehicle Price ($) | Vehicle Type | Basic Fee ($) | Special Fee ($) | Association Fee ($) | Storage Fee ($) | Total ($)    |
|------------------:|--------------|--------------:|----------------:|--------------------:|----------------:|-------------:|
| 398.00            | Common       | 39.80         | 7.96            | 5.00                | 100.00          | 550.76       |
| 501.00            | Common       | 50.00         | 10.02           | 10.00               | 100.00          | 671.02       |
| 57.00             | Common       | 10.00         | 1.14            | 5.00                | 100.00          | 173.14       |
| 1,800.00          | Luxury       | 180.00        | 72.00           | 15.00               | 100.00          | 2,167.00     |
| 1,100.00          | Common       | 50.00         | 22.00           | 15.00               | 100.00          | 1,287.00     |
| 1,000,000.00      | Luxury       | 200.00        | 40,000.00       | 20.00               | 100.00          | 1,040,320.00 |

