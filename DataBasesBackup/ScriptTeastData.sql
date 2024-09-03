INSERT INTO Sectors (Number) VALUES (1);
INSERT INTO Sectors (Number) VALUES (2);
INSERT INTO Sectors (Number) VALUES (3);
GO
INSERT INTO Specializations (Title) VALUES (N'Терапевт');
INSERT INTO Specializations (Title) VALUES (N'Педиатр');
INSERT INTO Specializations (Title) VALUES (N'Хирург');
GO
INSERT INTO Offices (Number) VALUES (101);
INSERT INTO Offices (Number) VALUES (102);
INSERT INTO Offices (Number) VALUES (103);
GO
INSERT INTO Patients (LastName, FirstName, MiddleName, Address, DateOfBirth, Sex, SectorID)
VALUES (N'Иванов', N'Иван', N'Иванович', N'г. Москва, ул. Ленина, д. 1', '1980-01-01', 'M', 10);

INSERT INTO Patients (LastName, FirstName, MiddleName, Address, DateOfBirth, Sex, SectorID)
VALUES (N'Петрова', N'Мария', N'Сергеевна', N'г. Москва, ул. Гагарина, д. 2', '1990-05-15', 'F', 11);

INSERT INTO Patients (LastName, FirstName, MiddleName, Address, DateOfBirth, Sex, SectorID)
VALUES (N'Сидоров', N'Алексей', N'Алексеевич', N'г. Москва, ул. Крупской, д. 3', '1975-08-22', 'M', 12);
GO
INSERT INTO Doctors (LastName, FirstName, MiddleName, OfficeID, SpecializationID, SectorID)
VALUES (N'Смирнов', N'Андрей', N'Викторович', 4, 16, 10);

INSERT INTO Doctors (LastName, FirstName, MiddleName, OfficeID, SpecializationID, SectorID)
VALUES (N'Кузнецова', N'Ольга', N'Павловна', 5, 15, 12);

INSERT INTO Doctors (LastName, FirstName, MiddleName, OfficeID, SpecializationID, SectorID)
VALUES (N'Николаев', N'Игорь', N'Александрович', 6, 14, 11);
GO