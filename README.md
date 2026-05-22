# coffee-shop-management-winform
A C# Windows Forms application for coffee shop management, including products, categories, tables, orders, invoices, CRUD operations, and database management.
# Coffee Shop Management System

A desktop application for managing coffee shop operations, built with **C# Windows Forms** and **SQL Server**. The system supports product management, table management, order processing, invoice generation, revenue tracking, and database backup/restore.

## Features

- User login and role-based access for Admin and Staff
- Manage tables, products, categories, orders, and invoices
- Add items to orders and calculate total price
- Apply discount and process checkout
- Generate and print invoices
- View revenue by date range
- Export revenue data to Excel
- Manage user accounts and reset passwords
- Backup and restore SQL Server database

## Technologies Used

- **Language:** C#
- **Framework:** .NET Framework 4.7.2
- **UI:** Windows Forms
- **Database:** SQL Server
- **Data Access:** ADO.NET
- **Architecture:** DAL / DTO structure
- **Libraries:** EPPlus, Microsoft Office Interop Excel

## Database

The database is named `QL_QuanCaFe` and includes:

- `Account`
- `TableFood`
- `FoodCategory`
- `Food`
- `Bill`
- `BillInfo`

Database script:

```text
Data.sql
