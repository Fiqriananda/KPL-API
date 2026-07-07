# Ekspedisi Paket API

API untuk mengelola pengiriman paket dengan fitur CRUD lengkap, menggunakan Clean Code dan Database MySQL.

## Prerequisites

- .NET 10.0 atau lebih tinggi
- MySQL Server
- Visual Studio Code atau IDE lainnya

## Setup Database

1. Buat database MySQL:
```sql
CREATE DATABASE ExpedisiPaket;
USE ExpedisiPaket;
```

2. Update connection string di `appsettings.json`:
```json
"ConnectionStrings": {
  "DefaultConnection": "Server=localhost;Port=3306;Database=ExpedisiPaket;User=root;Password=YOUR_PASSWORD"
}
```

3. Jalankan migration (opsional untuk auto-create table):
```bash
dotnet ef migrations add InitialCreate
dotnet ef database update
```

## Instalasi Dependensi

```bash
cd ExpedisiPaketAPI
dotnet restore
```

## Menjalankan Aplikasi

```bash
dotnet run
```

API akan berjalan di `https://localhost:5001` atau `http://localhost:5000`

## API Endpoints

### 1. Get All Pakets
- **Method**: GET
- **URL**: `/api/pakets`
- **Response**: List semua paket

### 2. Get Paket By ID
- **Method**: GET
- **URL**: `/api/pakets/{id}`
- **Response**: Detail paket berdasarkan ID

### 3. Get Paket By Nomor Resi
- **Method**: GET
- **URL**: `/api/pakets/resi/{nomerResi}`
- **Response**: Detail paket berdasarkan nomor resi

### 4. Get Paket By Status
- **Method**: GET
- **URL**: `/api/pakets/status/{status}`
- **Response**: List paket berdasarkan status (PENDING, PICKED_UP, IN_TRANSIT, etc)

### 5. Get Paket By Kota
- **Method**: GET
- **URL**: `/api/pakets/kota/{kota}`
- **Response**: List paket berdasarkan kota tujuan

### 6. Create Paket
- **Method**: POST
- **URL**: `/api/pakets`
- **Body**:
```json
{
  "nomerResi": "PKG001",
  "pengirim": "Budi Santoso",
  "penerima": "Ahmad Wijaya",
  "alamatAsal": "Jl. Merdeka No. 1, Jakarta",
  "alamatTujuan": "Jl. Sudirman No. 5, Bandung",
  "beratKg": 2.5,
  "kota": "Bandung",
  "biaya": 50000
}
```

### 7. Update Paket
- **Method**: PUT
- **URL**: `/api/pakets/{id}`
- **Body**:
```json
{
  "pengirim": "Budi Santoso",
  "penerima": "Ahmad Wijaya",
  "alamatAsal": "Jl. Merdeka No. 1, Jakarta",
  "alamatTujuan": "Jl. Sudirman No. 5, Bandung",
  "beratKg": 2.5,
  "statusPengiriman": "IN_TRANSIT",
  "kota": "Bandung",
  "biaya": 50000
}
```

### 8. Delete Paket
- **Method**: DELETE
- **URL**: `/api/pakets/{id}`
- **Response**: Success message

## Status Pengiriman

- PENDING - Menunggu pengiriman
- PICKED_UP - Sudah diambil
- IN_TRANSIT - Dalam perjalanan
- IN_WAREHOUSE - Di gudang
- OUT_FOR_DELIVERY - Sedang diantarkan
- DELIVERED - Sudah diterima
- CANCELLED - Dibatalkan
- RETURN - Dikembalikan

## Struktur Project

```
ExpedisiPaketAPI/
├── Controllers/
│   └── PaketsController.cs
├── Models/
│   └── Paket.cs
├── DTOs/
│   ├── CreatePaketDto.cs
│   └── UpdatePaketDto.cs
├── Services/
│   ├── IPaketService.cs
│   └── PaketService.cs
├── Data/
│   └── ExpedisiDbContext.cs
├── Program.cs
├── appsettings.json
└── ExpedisiPaketAPI.csproj
```

## Clean Code Principles

- Tidak ada comment yang tidak perlu
- Naming convention yang jelas
- Separation of Concerns (Controller, Service, Data Access)
- DTOs untuk input/output
- Async/Await untuk operasi database
- Error handling yang proper

## Testing dengan cURL

```bash
curl -X GET https://localhost:5001/api/pakets

curl -X POST https://localhost:5001/api/pakets \
  -H "Content-Type: application/json" \
  -d '{"nomerResi":"PKG001","pengirim":"Budi","penerima":"Ahmad","alamatAsal":"Jakarta","alamatTujuan":"Bandung","beratKg":2.5,"kota":"Bandung","biaya":50000}'

curl -X PUT https://localhost:5001/api/pakets/1 \
  -H "Content-Type: application/json" \
  -d '{"pengirim":"Budi","penerima":"Ahmad","alamatAsal":"Jakarta","alamatTujuan":"Bandung","beratKg":2.5,"statusPengiriman":"DELIVERED","kota":"Bandung","biaya":50000}'

curl -X DELETE https://localhost:5001/api/pakets/1
```
