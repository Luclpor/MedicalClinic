-- Создание базы данных
CREATE DATABASE MedicalClinic;
GO

-- Использование базы данных
USE MedicalClinic;
GO

-- Таблица "Участки"
CREATE TABLE Sectors (
    SectorID INT PRIMARY KEY IDENTITY(1,1),  -- Уникальный идентификатор участка
    Number INT NOT NULL                         -- Номер участка
);
GO

-- Таблица "Специализации"
CREATE TABLE Specializations (
    SpecializationID INT PRIMARY KEY IDENTITY(1,1),  -- Уникальный идентификатор специализации
    Title NVARCHAR(100) NOT NULL                   -- Название специализации
);
GO

-- Таблица "Кабинеты"
CREATE TABLE Offices (
    OfficeID INT PRIMARY KEY IDENTITY(1,1),  -- Уникальный идентификатор кабинета
    Number INT NOT NULL                        -- Номер кабинета
);
GO

-- Таблица "Пациенты"
CREATE TABLE Patients (
    PatientID INT PRIMARY KEY IDENTITY(1,1),     -- Уникальный идентификатор пациента
    LastName NVARCHAR(100) NOT NULL,              -- Фамилия
    FirstName NVARCHAR(100) NOT NULL,                  -- Имя
    MiddleName NVARCHAR(100),                      -- Отчество
    Address NVARCHAR(255) NOT NULL,                 -- Адрес
    DateOfBirth DATE NOT NULL,                 -- Дата рождения
    Sex CHAR(1) CHECK (Sex IN ('M', 'F')) NOT NULL,  -- Пол (M - мужской, F - женский)
    SectorID INT,                               -- Ссылка на участок
    FOREIGN KEY (SectorID) REFERENCES Sectors(SectorID)  -- Внешний ключ на "Участки"
);
GO

-- Таблица "Врачи"
CREATE TABLE Doctors (
    DoctorID INT PRIMARY KEY IDENTITY(1,1),        -- Уникальный идентификатор врача
    LastName NVARCHAR(100) NOT NULL,              -- Фамилия
    FirstName NVARCHAR(100) NOT NULL,                  -- Имя
    MiddleName NVARCHAR(100),                      -- Отчество                   
    OfficeID INT,                                -- Ссылка на кабинет
    SpecializationID INT,                        -- Ссылка на специализацию
    SectorID INT,                               -- Ссылка на участок (для участковых врачей)
    FOREIGN KEY (OfficeID) REFERENCES Offices(OfficeID),          -- Внешний ключ на "Кабинеты"
    FOREIGN KEY (SpecializationID) REFERENCES Specializations(SpecializationID),  -- Внешний ключ на "Специализации"
    FOREIGN KEY (SectorID) REFERENCES Sectors(SectorID)          -- Внешний ключ на "Участки"
);
GO