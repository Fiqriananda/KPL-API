SET NAMES utf8mb4;

DROP DATABASE IF EXISTS ExpedisiPaket;

CREATE DATABASE ExpedisiPaket;

USE ExpedisiPaket;

CREATE TABLE Pakets (
    Id INT PRIMARY KEY AUTO_INCREMENT,
    NomerResi VARCHAR(50) NOT NULL UNIQUE,
    Pengirim VARCHAR(100) NOT NULL,
    Penerima VARCHAR(100) NOT NULL,
    AlamatAsal VARCHAR(255) NOT NULL,
    AlamatTujuan VARCHAR(255) NOT NULL,
    BeratKg DECIMAL(10, 2) NOT NULL,
    StatusPengiriman VARCHAR(50) NOT NULL,
    TanggalDikirm DATETIME NOT NULL,
    TanggalDiterima DATETIME NULL,
    Kota VARCHAR(100) NOT NULL,
    Biaya DECIMAL(12, 2) NOT NULL,
    INDEX idx_status (StatusPengiriman),
    INDEX idx_kota (Kota)
) ENGINE=InnoDB DEFAULT CHARSET=utf8mb4 COLLATE=utf8mb4_unicode_ci;
