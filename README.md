# LnpInfotechAssesment
# Assessment Application - Setup & Installation Guide

This document provides comprehensive instructions for setting up and running the Assessment application on any system.

## Prerequisites

Before starting, ensure you have the following installed on your system:

### Required Software
- **.NET 8 SDK** -
- **SQL Server** (or SQL Server Express)-
- **SQL Server Management Studio (SSMS)**
- **Visual Studio 2022** (or later)

## Environment Setup

### Step 1: Clone or Extract the Repository

```bash

\# If cloning from Git

git clone https://github.com/vishal4code/LnpInfotechAssesment.git

cd Assessment



\# If extracting from zip

\# Extract the zip file to your desired location

cd Assessment

```

### Step 2: Restore NuGet Packages

Navigate to the solution root directory and restore all dependencies:

```bash

dotnet restore

RUN 3 Commands
1) dotnet add Assessment.DAL package Microsoft.EntityFrameworkCore --version 8.0.2
2) dotnet add Assessment.DAL package Microsoft.EntityFrameworkCore.SqlServer --version 8.0.2
3) dotnet add Assessment.DAL package Microsoft.EntityFrameworkCore.Tools --version 8.0.2

```



### Step 3: Verify Project Structure

The solution should contain three main projects:

```

Assessment/

├── Assessment.UI/          (ASP.NET Core Razor Pages UI Layer)

├── Assessment.BAL/         (Business Logic Layer)

├── Assessment.DAL/         (Data Access Layer)

└── Assessment.sln          (Solution file)

```

---

## Database Configuration

### Update Connection String

Open `Assessment.UI/appsettings.json` and configure the database connection:

```json

{

 "ConnectionStrings": {

 "DefaultConnection": "Server=.\\SQLEXPRESS;Database=AssessmentDB;TrustedConnection=True;TrustServerCertificate=True;"

 }

}

```

### Create Database

*********Step 1: Create database**********

Open SQL Server Management Studio (SSMS) and run:
```sql
CREATE DATABASE AssessmentDB;
GO
```
*********** Step 2: Select database*********
```sql

USE AssessmentDB;
GO
```

********* Step 3: Create tables*************
```sql

CREATE TABLE Categories (
    CategoryId INT IDENTITY PRIMARY KEY,
    CategoryName NVARCHAR(100),
    Description NVARCHAR(250)
);

CREATE TABLE Products (
    ProductId INT IDENTITY PRIMARY KEY,
    ProductName NVARCHAR(150),
    Description NVARCHAR(250),
    Quantity INT,
    Price DECIMAL(10,2)
);

CREATE TABLE ProductCategories (
    ProductId INT NOT NULL,
    CategoryId INT NOT NULL,
    CONSTRAINT PK_ProductCategories PRIMARY KEY (ProductId, CategoryId),
    CONSTRAINT FK_PC_Product FOREIGN KEY (ProductId) REFERENCES Products(ProductId),
    CONSTRAINT FK_PC_Category FOREIGN KEY (CategoryId) REFERENCES Categories(CategoryId)
);
```

*********** Step 4: Create stored procedures*************
```sql

CREATE PROC sp_AddProduct
(
 @ProductName NVARCHAR(150),
 @Description NVARCHAR(250),
 @Quantity INT,
 @Price DECIMAL(10,2)
)
AS
BEGIN
    INSERT INTO Products(ProductName, Description, Quantity, Price)
    VALUES (@ProductName, @Description, @Quantity, @Price)
END
```

```sql

CREATE PROC sp_UpdateProduct
(
 @ProductId INT,
 @ProductName NVARCHAR(150),
 @Description NVARCHAR(250),
 @Quantity INT,
 @Price DECIMAL(10,2)
)
AS
BEGIN
    UPDATE Products
    SET ProductName = @ProductName,
        Description = @Description,
        Quantity = @Quantity,
        Price = @Price
    WHERE ProductId = @ProductId
END
```

```sql
CREATE PROC sp_DeleteProduct
(@ProductId INT)
AS
BEGIN
    DELETE FROM Products WHERE ProductId = @ProductId
END
```

---

## Running the Application

### Using Visual Studio

1. Open `Assessment.sln` in Visual Studio
2. Set `Assessment.UI` as the Startup Project
4. The application will open in your default browser at YOUR LOCALHOST

### Using Command Line

```bash

\# Navigate to the UI project

**cd Assessment.UI**



\# Restore dependencies

dotnet restore



\# Build the solution

dotnet build



\# Run the application

dotnet run





**## Application Features**



**### Products Management**



The application includes a complete Product management system with:



\- \*\*View Products\*\*: Browse all products with pagination

\- \*\*Filter Products\*\*: Filter by:

&nbsp; - Product name (search)

&nbsp; - Price range (min/max)

&nbsp; - Quantity

\- \*\*Create Product\*\*: Add new products to the system

\- \*\*Edit Product\*\*: Update product information

\- \*\*Delete Product\*\*: Remove products from the system



**## Project Architecture**



\### Assessment.UI (Presentation Layer)



\- \*\*Controllers\*\*: Product and Home controllers

\- \*\*Views\*\*: Razor Pages for UI rendering

\- \*\*Models\*\*: View models (ProductVM, ProductFilterVM, PaginatedResult, etc.)

\- \*\*wwwroot\*\*: Static files (CSS, JavaScript)



\### Assessment.BAL (Business Logic Layer)



\- \*\*Interfaces\*\*: IProductService

\- \*\*Services\*\*: ProductService (business logic implementation)

\- Contains validation and business rules



\### Assessment.DAL (Data Access Layer)



\- \*\*DbContext\*\*: AppDbContext for Entity Framework Core

\- \*\*Entities\*\*: Product, Category, ProductCategory domain models

\- \*\*Interfaces\*\*: IProductRepository

\- \*\*Repositories\*\*: ProductRepository (data access implementation)

\- \*\*Migrations\*\*: Database schema migrations


