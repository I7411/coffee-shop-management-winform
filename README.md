# Coffee Shop Management System

A desktop application for managing coffee shop operations, built with **C# Windows Forms** and **SQL Server**. The system supports product management, category management, table management, order processing, invoice generation, revenue reporting, account management, and database backup/restore.

## Features

- **Authentication & Authorization**
  - Login system for Admin and Staff accounts
  - Role-based access to management features

- **Table Management**
  - Display coffee shop tables
  - Track table status such as available or occupied
  - Transfer active orders between tables

- **Product & Category Management**
  - Add, update, delete, and view products
  - Manage food and beverage categories
  - Store product price and category information

- **Order Processing**
  - Add products to selected tables
  - Update item quantities
  - Calculate total order price
  - Apply discounts during checkout

- **Invoice Management**
  - Generate invoices for customer orders
  - Store bill and bill detail information
  - Print invoices

- **Revenue Reporting**
  - View revenue by date range
  - Display order and invoice statistics
  - Export revenue reports to Excel

- **Account Management**
  - Manage user accounts
  - Update account information
  - Reset or change passwords

- **Database Backup & Restore**
  - Backup SQL Server database
  - Restore database when needed

## Technologies Used

| Category | Technology |
|---|---|
| Language | C# |
| Framework | .NET Framework 4.7.2 |
| UI | Windows Forms |
| Database | SQL Server |
| Data Access | ADO.NET, Stored Procedures |
| Architecture | DAL / DTO |
| Excel Export | EPPlus, Microsoft Office Interop Excel |
| IDE | Visual Studio |

## Project Structure

```text
coffee-shop-management-winform/
├── QuanLyQuanCaPhe/
│   ├── DAL/                         # Data Access Layer
│   │   ├── DataProvider.cs          # Database connection and query execution
│   │   ├── AccountDAL.cs            # Account data operations
│   │   ├── BillDAL.cs               # Bill and invoice operations
│   │   ├── BillInfoDAL.cs           # Bill detail operations
│   │   ├── CategoryDAL.cs           # Product category operations
│   │   ├── FoodDAL.cs               # Product operations
│   │   ├── MenuDAL.cs               # Menu display operations
│   │   └── TableDAL.cs              # Table operations
│   │
│   ├── DTO/                         # Data Transfer Objects
│   │   ├── AccountDTO.cs
│   │   ├── BillDTO.cs
│   │   ├── BillInfoDTO.cs
│   │   ├── CategoryDTO.cs
│   │   ├── FoodDTO.cs
│   │   ├── MenuDTO.cs
│   │   └── TableDTO.cs
│   │
│   ├── Login.cs                     # Login form
│   ├── TableManager.cs              # Main order and table management form
│   ├── Admin.cs                     # Admin management form
│   ├── AccountProfile.cs            # Account profile form
│   ├── Program.cs                   # Application entry point
│   ├── App.config                   # Application configuration
│   └── QuanLyQuanCaPhe.csproj       # Project file
│
├── Data.sql                         # SQL Server database script
├── README.md                        # Project documentation
└── QuanLyQuanCaPhe.sln              # Visual Studio solution file
```

> The actual folder structure may vary slightly depending on how the project is organized in Visual Studio.

## Database

The database is named **`QL_QuanCaFe`**.

### Main Tables

| Table | Description |
|---|---|
| `Account` | Stores user login information and role type |
| `TableFood` | Stores coffee shop table information and table status |
| `FoodCategory` | Stores product category information |
| `Food` | Stores food and beverage products with prices |
| `Bill` | Stores invoice/order records |
| `BillInfo` | Stores order details for each bill |

### Database Script

The database script is included in:

```text
Data.sql
```

The script is used to create the database, tables, stored procedures, and initial sample data.

## How to Run the Project

### Prerequisites

Make sure the following tools are installed:

- Visual Studio 2019 or later
- SQL Server 2019 or later
- SQL Server Management Studio (SSMS)
- .NET Framework 4.7.2 or higher

### Step 1: Clone the Repository

```bash
git clone https://github.com/l7411/coffee-shop-management-winform.git
cd coffee-shop-management-winform
```

### Step 2: Create the Database

1. Open **SQL Server Management Studio**.
2. Open the `Data.sql` file.
3. Execute the script.
4. Make sure the database **`QL_QuanCaFe`** is created successfully.

### Step 3: Update the Connection String

Open the file:

```text
QuanLyQuanCaPhe/DAL/DataProvider.cs
```

Update the SQL Server connection string according to your local machine:

```csharp
string connectionSTR = "Data Source=YOUR_SERVER_NAME;Initial Catalog=QL_QuanCaFe;Integrated Security=True;Encrypt=False";
```

Example for SQL Server Express:

```csharp
string connectionSTR = "Data Source=.\\SQLEXPRESS;Initial Catalog=QL_QuanCaFe;Integrated Security=True;Encrypt=False";
```

If the backup/restore feature has its own connection string, update it in the related form or configuration file as well.

### Step 4: Restore NuGet Packages

In Visual Studio:

```text
Right-click Solution → Restore NuGet Packages
```

### Step 5: Build and Run

1. Open `QuanLyQuanCaPhe.sln` in Visual Studio.
2. Select **Build → Build Solution**.
3. Press **F5** or select **Debug → Start Debugging**.
4. The application will start with the login screen.

## How to Use

### Staff Flow

1. Log in with a Staff account.
2. Select a table from the main screen.
3. Add food or beverage items to the selected table.
4. Adjust quantity if needed.
5. Apply discount if applicable.
6. Process checkout.
7. Print or save the invoice.

### Admin Flow

1. Log in with an Admin account.
2. Open the Admin management screen.
3. Manage products, categories, tables, and user accounts.
4. View revenue reports by date range.
5. Export reports to Excel.
6. Backup or restore the database when needed.

## Deployment

This is a Windows desktop application, so deployment is different from a web application.

### Option 1: Run from Visual Studio

Use this option during development or demonstration:

1. Open the solution in Visual Studio.
2. Configure the database connection string.
3. Build and run the project.

### Option 2: Build Release Version

Use this option when you want to share the application executable:

1. Open the project in Visual Studio.
2. Change build mode from **Debug** to **Release**.
3. Select **Build → Build Solution**.
4. Go to the output folder:

```text
QuanLyQuanCaPhe/bin/Release/
```

5. Run the `.exe` file.

### Deployment Notes

Before running the application on another computer:

- Install .NET Framework 4.7.2 or higher.
- Install SQL Server or connect to an available SQL Server instance.
- Run `Data.sql` to create the database.
- Update the connection string to match the target SQL Server.
- Make sure required NuGet packages and external libraries are included.

## Default Accounts

| Username | Password | Role |
|---|---|---|
| `K8` | `1` | Admin |
| `R9` | `1` | Staff |

> These accounts are for testing and demonstration purposes only.

## What I Learned

- Built a desktop application using C# Windows Forms.
- Implemented CRUD operations with SQL Server.
- Designed and worked with a relational database schema.
- Used ADO.NET and stored procedures for database operations.
- Applied DAL and DTO layers to organize project structure.
- Implemented login, role-based access, order processing, invoice generation, and revenue reporting.
- Improved practical understanding of database-driven application development.

## Future Improvements

- Hash user passwords instead of storing plain text passwords.
- Move the database connection string to `App.config`.
- Use parameterized queries consistently to reduce SQL injection risks.
- Improve input validation and exception handling.
- Add audit logs for important admin actions.
- Improve UI/UX design.
- Add unit tests for business logic and database operations.
- Add inventory tracking and low-stock alerts.
- Create a setup installer for easier deployment.

## Author

**Huỳnh Thanh Minh Tâm**  
Software Engineering Student

## License

This project is created for educational purposes.
