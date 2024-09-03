# Документация API

Проект содержит два API-контроллера для редактирования таблиц `Doctors` и `Patients`.

## Операции API

### Добавление записи

**Метод:** `POST`  
**URL:** `http://localhost:5020/api/Doctor/`  
**Статус-код:** `200`

**Request Headers:**
```http
Content-Type: application/json
```
**Request Body**
```json
{
  "lastName": "Петров",
  "firstName": "Петор",
  "middleName": "Петрович",
  "sectorId": 11,
  "officeId": 6,
  "specializationId": 14
}
```
**Response Body**
```json
{
  "doctorId": 1030,
  "lastName": "Петров",
  "firstName": "Петор",
  "middleName": "Петрович",
  "office": 103,
  "sector": 2,
  "specialization": "Терапевт"
}
```
---
---
### Редактирование записи

**Метод:** `PUT`  
**URL:** `http://localhost:5020/api/Doctor/`  
**Статус-код:** `200`
**Request Headers:**
```http
Content-Type: application/json
```
**Request Body**
```json
{
  "doctorId": 1030,
  "lastName": "Сидоров",
  "firstName": "Сидр",
  "middleName": "Сидорович",
  "officeId": 6,
  "sectorId": 11,
  "specializationId": 14
}
```
**Response Body**
```json
{
  "doctorId": 1030,
  "lastName": "Сидоров",
  "firstName": "Сидр",
  "middleName": "Сидорович",
  "sectorId": 11,
  "officeId": 6,
  "specializationId": 14
}
```
---
---
### Удаление записи

**Метод:** `DELETE`  
**URL:** `http://localhost:5020/api/Doctor/1030`  
**Статус-код:** `200`<br>
**Response Body:**
```json
"Doctor with Id: 1030 deleted"
```
---
---
### Получение списка записей с поддержкой сортировки и постраничного возврата данных

**Метод:** `GET`  
**URL:** `http://localhost:5020/api/Doctor/GetSortList?sortField={FirstName}&page={10}&rows={2}`  
**Статус-код:** `200`<br>
**Response Body:**
```json
  {
    "doctorId": 1022,
    "lastName": "Константинов",
    "firstName": "Алексеевич",
    "middleName": "Иван",
    "office": 103,
    "sector": 2,
    "specialization": "Терапевт"
  },
  {
    "doctorId": 1023,
    "lastName": "Константинов",
    "firstName": "Алексеевич",
    "middleName": "Иван",
    "office": 103,
    "sector": 2,
    "specialization": "Терапевт"
   }
```
---
---
### Получение записи по id для редактирования

**Метод:** `GET`  
**URL:** `http://localhost:5020/api/Doctor/1019`  
**Статус-код:** `200`<br>
**Response Body:**
```json
  {
    "doctorId": 1019,
    "lastName": "rudnev",
    "firstName": "Алексеевич",
    "middleName": "Иван",
    "sectorId": 11,
    "officeId": 6,
    "specializationId": 14
  }

```
