# TinyLedger

Set of apis to power a simple ledger.

---

## API Usage Examples

You can go directly on Visual Studio/Rider and run the app. If you don't have them, you just run this command:
- `dotnet run \Presentation --project Presentation`

All routes require a `customerId` as part of the URL.

### Deposit

**Request**

`POST /api/transactions/{customerId}/deposit`

**Body**
```json
{
  "amount": 100.0
}
```

---

### Withdraw

**Request**

`POST /api/transactions/{customerId}/withdraw`

**Body**
```json
{
  "amount": 50.0
}
```

---

### Get Balance

**Request**

`GET /api/transactions/{customerId}/balance`

---

### Get Transaction History

**Request**

`GET /api/transactions/{customerId}/history`


---

## Sample `ledger.json`

```json
[
  {
    "id": "3c882b61-81c0-4d9b-9c0d-5acb2e7db85e",
    "timestamp": "2025-04-03T14:45:00Z",
    "type": 0,
    "amount": 100.0,
    "customerId": "user1"
  },
  {
    "id": "e2a1c891-a16b-4b14-873f-95a0d79867e1",
    "timestamp": "2025-04-03T15:10:00Z",
    "type": 1,
    "amount": 20.0,
    "customerId": "user1"
  }
]
```

---

## Features

- Record deposits and withdrawals
- View customer balance
- View transaction history
- Data is persisted to `ledger.json` in the root of the project
- Multi-customer support
- Swagger UI for interactive testing

---

## Technologies

- Clean layered architecture:
  - `Domain`
  - `Application`
  - `Data`
  - `API` (Presentation)
- Swagger (Swashbuckle)


---

## Design Decisions

### Project Structure

- A layered architecture was adopted (`Domain`, `Application`, `Data`, `Presentation`) to reflect organization and scalability.

### Persistence

- File-based persistence (`ledger.json`) is used to simulate data durability while avoiding unnecessary complexity from database setup.
- Data is serialized and written to disk after each transaction, ensuring no information is lost between runs.

### Multi-customer Support

- All endpoints are filtered by `customerId`, making the system flexible.
- Transactions are filtered per customer before any calculation.
#### NOTE:
There are no validations related with customer besides the one when getting the balance/history/deposit or withdraw,
this means that, as we are not creating customers, that any customerId can be inserted, and if it doesn't exist it will 
create a new entry, so make sure that if you want to test all the flow, to introduce the same customerId exactly because 
there will be no message warning that "This customerId doesn't exist".

### Why there is no `Customer` model

- No additional data or logic related to customers is currently needed beyond identifying them by ID.
- Avoided introducing unused abstractions to keep the code focused and intentional.
- The system can easily evolve to include a `Customer` model if/when needed.

### Why there are no unit tests

- Priority was given to delivering a fully functional, maintainable, and demonstrable API under time constraints.
- All logic is encapsulated in the `LedgerService`, and dependencies are injected — making it ready for unit testing without needing refactoring.
- The next natural step is to write unit tests for deposit, withdrawal, and balance operations.

---

## Swagger UI

You can go directly on Visual Studio/Rider and run the app. Once the app is running, you can open:

```
http://localhost:65348/index.html
```

Swagger UI allows exploring and testing all endpoints interactively.

---
