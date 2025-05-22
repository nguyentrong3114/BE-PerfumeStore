# 💎 BE-AMPerfume - ASP.NET Core 8.0 RESTful API

## 📦 Kiến trúc

Ứng dụng được xây dựng theo mô hình **3 lớp (Three-layer architecture)**:
- **API Layer (`BE-AMPerfume.API`)**: Giao tiếp giữa client (Next.js) và server thông qua các REST API.
- **Business Logic Layer - BLL (`BE-AMPerfume.BLL`)**: Xử lý nghiệp vụ.
- **Data Access Layer - DAL (`BE-AMPerfume.DAL`)**: Truy cập và tương tác cơ sở dữ liệu thông qua Entity Framework Core.
- **Core (`BE-AMPerfume.Core`)**: Chứa các model chung (DTOs, Entities), constants và interface dùng giữa các tầng.

---

## 🚀 Công nghệ sử dụng

-  .NET 8.0 (ASP.NET Core Web API)
-  Entity Framework Core 8 (với Pomelo cho MySQL)
-  MySQL
-  AutoMapper
-  JWT Authentication
-  Swagger UI (tài liệu API)
-  Dependency Injection
-  Clean Architecture (cơ bản)

---

## ⚙️ Cấu hình

### 1. Connection String
Cập nhật chuỗi kết nối trong `appsettings.json`:

1. Cài đặt package
   dotnet restore
2. Tạo CSDL và migration
  dotnet ef migrations add InitialCreate -s BE-AMPerfume.API
   dotnet ef database update -s BE-AMPerfume.API
3. Chạy API
   dotnet run --project BE-AMPerfume.API
