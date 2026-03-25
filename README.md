# DvlSql

**DvlSql** is a .NET DSL (Domain-Specific Language) library for building and executing SQL statements using a fluent, LINQ-like syntax. It supports **MS SQL Server** and **PostgreSQL** and abstracts away the boilerplate of ADO.NET code for `SELECT`, `INSERT`, `UPDATE`, `DELETE`, stored procedures, transactions, and more.

---

## 📦 Packages

| Package | Version | Description |
|---|---|---|
| `DvlSql` | 4.2.0 | Core abstractions, interfaces, and SQL expressions |
| `DvlSql.Extensions` | 4.2.0 | Helper extensions and SQL type factories |
| `DvlSql.SqlServer` | 4.2.0 | MS SQL Server implementation (uses `Microsoft.Data.SqlClient`) |
| `DvlSql.PostgreSql` | 0.0.1 | PostgreSQL implementation (uses `Npgsql`) |

---

## 🏗️ Solution Structure

```
DvlSql2/
├── DvlSql/                        # Core abstractions & SQL expression tree
├── DvlSql.Extensions/             # SqlType helpers & DataReader extensions
├── DvlSql.SqlServer/              # MS SQL Server implementation
├── DvlSql.PostgreSql/             # PostgreSQL implementation
├── DvlSql.PostgreSql.Console/     # Console sample for PostgreSQL
└── DvlSql.SqlServer.Tests/        # NUnit tests for SQL Server implementation
```

---

## 📦 Installation

### MS SQL Server
```powershell
NuGet\Install-Package DvlSql.SqlServer -Version 4.2.0
```

### PostgreSQL
```powershell
NuGet\Install-Package DvlSql.PostgreSql -Version 0.0.1
```

---

## 🚀 Quick Start

### SQL Server

```csharp
using DvlSql.SqlServer;
using static DvlSql.ExpressionHelpers;
using static DvlSql.Extensions.SqlType;

var sql = new DvlSqlMs(new() { ConnectionString = "your_connection_string" });
```

### PostgreSQL

```csharp
using DvlSql.PostgreSql;

var sql = new DvlPostgreSql("Host=localhost;Username=postgres;Password=secret;Database=mydb");
```

---

## 📖 Usage Examples

### SELECT

```csharp
// SELECT * FROM dbo.Words
var results = await sql.From("dbo.Words")
    .Select()
    .ToListAsync(r => r["Name"].ToString());

// SELECT Id, Name FROM dbo.Words WHERE Chars = @p1
var filtered = await sql.From("dbo.Words")
    .Where(ConstantExpCol("Chars") == 1)
    .Select("Id", "Name")
    .ToListAsync(r => (Id: (int)r["Id"], Name: r["Name"].ToString()));

// SELECT TOP 10 * FROM dbo.Words ORDER BY Id DESC
var top10 = await sql.From("dbo.Words")
    .SelectTop(10)
    .OrderBy(("Id", Ordering.Desc))
    .ToListAsync(r => r["Name"].ToString());
```

### SELECT with JOIN

```csharp
var results = await sql.From("dbo.Orders o")
    .InnerJoin<int>("dbo.Customers c", ConstantExpCol("o.CustomerId") == ConstantExpCol("c.Id"))
    .Select("o.Id", "c.Name")
    .ToListAsync(r => (OrderId: (int)r["Id"], Customer: r["Name"].ToString()));
```

### SELECT with GROUP BY

```csharp
var grouped = await sql.From("dbo.Words")
    .GroupBy("Category")
    .Having(Count("Id") > 5)
    .Select("Category")
    .ToListAsync(r => r["Category"].ToString());
```

### INSERT

```csharp
// INSERT INTO dbo.Words (Amount, Text) VALUES (@Amount1, @Text1), ...
await sql.InsertInto<int, string>("dbo.Words",
        DecimalType("Amount"),
        NVarCharType("Text", 100))
    .Values(
        (1, "Hello"),
        (2, "World")
    )
    .ExecuteAsync();
```

### UPDATE

```csharp
// UPDATE dbo.Words SET money = @money WHERE Amount = @p1
await sql.Update("dbo.Words")
    .Set(Money("money", new decimal(9.99)))
    .Where(ConstantExpCol("Amount") == 42)
    .ExecuteAsync();
```

### DELETE

```csharp
// DELETE FROM dbo.Words WHERE Id = @p1
await sql.From("dbo.Words")
    .Delete()
    .Where(ConstantExpCol("Id") == 5)
    .ExecuteAsync();
```

### Stored Procedure

```csharp
var result = await sql.Procedure("dbo.GetWordById")
    .WithParameter(Int("@Id", 42))
    .ExecuteAsync(r => r["Name"].ToString());
```

### Transaction

```csharp
await sql.BeginTransactionAsync(async transaction =>
{
    await transaction.Update("dbo.Words")
        .Set(NVarChar("Text", 50, "Updated"))
        .Where(ConstantExpCol("Id") == 1)
        .ExecuteAsync();

    await transaction.InsertInto<string>("dbo.Log", NVarCharType("Message", 200))
        .Values(("Record updated",))
        .ExecuteAsync();
});
```

### UNION

```csharp
var result = await sql.From("dbo.Words")
    .Select("Id", "Name")
    .Union(
        sql.From("dbo.OtherWords").Select("Id", "Name")
    )
    .ToListAsync(r => r["Name"].ToString());
```

### Dependency Injection (SQL Server)

```csharp
// In Program.cs / Startup.cs
services.AddDvlSqlSqlServer(options =>
{
    options.ConnectionString = "your_connection_string";
});

// Inject IDvlSql wherever needed
public class MyService(IDvlSql sql) { ... }
```

---

## ✅ Requirements

- .NET 8.0+
- SQL Server (for `DvlSql.SqlServer`) or PostgreSQL (for `DvlSql.PostgreSql`)

---

## 🧪 Running Tests

Tests are written with [NUnit](https://nunit.org/) and located in `DvlSql.SqlServer.Tests/`.

```bash
dotnet test DvlSql.SqlServer.Tests/
```

---

## 🤝 Contributing

Contributions are welcome! Feel free to open issues or submit pull requests on [GitHub](https://github.com/DVLKinoMan/DvlSql2).

---

## 📄 License

This project is licensed. See the `license.txt` file in each package for details.

---

## 🙌 Credits

Inspired by LINQ and the desire to eliminate repetitive ADO.NET boilerplate for SQL queries.  
Created by **David Dvali**.

