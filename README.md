
# 🧩 Stockly — Backend API

A lightweight **ASP.NET Core Web API** for inventory and order management.
Handles product tracking, stock adjustments, and order processing with automatic stock updates and analytics support.

---

<!-- ## 🖼️ System Overview

> 💡 *(Replace the placeholders below with screenshots or diagrams)*

| Database Schema | API Architecture |
|:----------------:|:----------------:|
| ![Database Diagram](./docs/images/db_schema.png) | ![API Flow](./docs/images/api_flow.png) |

--- -->

## ⚙️ Tech Stack

- 🧱 **.NET 8 / ASP.NET Core Web API**
- 🗃️ **Entity Framework Core**
- 🐘 **PostgreSQL / SQLite (Configurable)**
- 🧰 **AutoMapper** for DTO mapping
- 📊 **LINQ** for data aggregation and analytics

---

## 🧰 Setup Instructions

### 1️⃣ Clone the repository

```bash
git clone https://github.com/yourusername/stockly_server.git
cd stockly_server
````

### 2️⃣ Configure the database connection

Edit `appsettings.json`:

```json
"ConnectionStrings": {
  "DefaultConnection": "Host=localhost;Database=stockly_db;Username=postgres;Password=yourpassword"
}
```

### 3️⃣ Apply migrations and seed data

```bash
dotnet ef database update
```

This creates tables and seeds:

- 🏷️ 10 Products
- 🧾 100 Orders (spanning the past year)

### 4️⃣ Run the server

```bash
dotnet run
```

API will be available at:
👉 `http://localhost:5070/api`

---

## 🧩 Database Schema

**Tables:**

- `Products`
- `Orders`
- `OrderItems`
- `StockAdjustments`

### Entity Relationships

- **Product 1–N OrderItems**
- **Order 1–N OrderItems**
- **Product 1–N StockAdjustments**
- **Order 0–N StockAdjustments**

---

## 🧱 Models Overview

### 🧾 Product

```csharp
public class Product {
  public int Id { get; set; }
  public string Name { get; set; }
  public decimal Price { get; set; }
  public int Quantity { get; set; }
  public bool IsActive { get; set; }
  public DateTime CreatedAt { get; set; }
}
```

### 📦 Order

```csharp
public class Order {
  public int Id { get; set; }
  public string Customer_name { get; set; }
  public string Customer_contact { get; set; }
  public string Status { get; set; }
  public decimal Total_amount { get; set; }
  public List<OrderItem> Items { get; set; }
  public DateTime CreatedAt { get; set; }
}
```

### 🧾 OrderItem

```csharp
public class OrderItem {
  public int Id { get; set; }
  public int OrderId { get; set; }
  public int ProductId { get; set; }
  public int Quantity { get; set; }
  public decimal Price { get; set; }
}
```

### ⚙️ StockAdjustment

```csharp
public class StockAdjustment {
  public int Id { get; set; }
  public int Product_Id { get; set; }
  public int? Related_Order_Id { get; set; }
  public int Change { get; set; }
  public string Reason { get; set; }
  public DateTime CreatedAt { get; set; }
}
```

---

## 🔌 Main API Endpoints

### `/api/products`

| Method   | Endpoint             | Description                    |
| :------- | :------------------- | :----------------------------- |
| `GET`    | `/api/products`      | Get paginated list of products |
| `GET`    | `/api/products/{id}` | Get product by ID              |
| `POST`   | `/api/products`      | Create new product             |
| `PUT`    | `/api/products/{id}` | Update product info            |
| `DELETE` | `/api/products/{id}` | Delete a product               |

---

### `/api/orders`

| Method   | Endpoint           | Description                                |
| :------- | :----------------- | :----------------------------------------- |
| `GET`    | `/api/orders`      | List all orders (with search + pagination) |
| `GET`    | `/api/orders/{id}` | Get order by ID (includes items)           |
| `POST`   | `/api/orders`      | Create new order                           |
| `PUT`    | `/api/orders/{id}` | Update status or details                   |
| `DELETE` | `/api/orders/{id}` | Cancel or delete order                     |

---

### `/api/home`

| Method | Endpoint    | Description                                        |
| :----- | :---------- | :------------------------------------------------- |
| `GET`  | `/api/home` | Returns dashboard analytics (counts, charts, etc.) |

**Response example:**

```json
{
  "ProductsCount": 10,
  "OrdersCount": 100,
  "PendingOrdersCount": 8,
  "UnShippedOrdersCount": 12,
  "MostSoldProducts": [...],
  "OrdersPerMonth": [...]
}
```

---

## ⚙️ How It Works

1. **Frontend (React)** calls API endpoints (e.g. `/api/products`, `/api/orders`) via Axios.
2. **ASP.NET Core API** handles requests, validates input, and uses **EF Core** to query/update the database.
3. **Stock Adjustment** entries are automatically logged when orders change product quantities.
4. **Analytics Controller (`HomeController`)** aggregates sales and stock data for dashboards.

---

## 🧠 Future Enhancements

- 🔐 Add authentication (JWT)
- 📊 Export reports (CSV / PDF)
- 🧾 Integrate payments and receipts
- 🛰️ Add WebSocket-based real-time updates

---

## 📜 License

This project is licensed under the **MIT License**.

---

### 👨‍💻 Author

**Talal**
📧 Contact: [[talalbalnoob@gmail.com](mailto:talalbalnoob@gmail.com)]
