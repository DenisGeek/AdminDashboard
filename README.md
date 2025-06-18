# Тестовое задание: Admin Dashboard - Denis Anokhin  
---
### основное особенности:

- использование чистой архитектуры, также использование MediatR для UseCases

- использование общего DbContext для различных БД (пример использования InfrastructureSQLite)
- использование миграций, 
- в случае с in-memory SQLite используется синглтон, миграции применяются каждый старте приложения
  
---
- Запуск проекта:  
  - Backend:    
    1. Перейдите в папку проекта  
    2. Восстановите зависимости (если нужно)
        - Команда: `dotnet restore`
    1. Запустите API
        - Команда: `dotnet run`
        - Порт: `:5000`  
  - Frontend:  
    1. Перейдите в папку проекта  
    2. Восстановите зависимости (если нужно)
        - Команда: `npm install`
    1. Запустите Front
        - Команда: `npm run dev`  
        - Порт: `:5173`  

- Данные для входа:  
  - Логин: `admin@mirra.dev`  
  - Пароль: `admin123`  
---
- Примеры запросов:  

  - Авторизация (`POST /auth/login`):  
    ```bash  
    curl -X 'POST' \  
      'http://localhost:5000/auth/login' \  
      -H 'accept: application/json' \  
      -H 'Content-Type: application/json' \  
      -d '{  
      "email": "admin@mirra.dev",  
      "password": "admin123"  
    }'  
    ```  
    - Ответ:  
      ```json  
        {
          "access": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI5YWM1MTMwZi1lNmU5LTQ1ZWItYjFmZS0zMmRjM2VlMGU4Y2QiLCJlbWFpbCI6ImFkbWluQG1pcnJhLmRldiIsInJvbGUiOiJBZG1pbiIsIm5iZiI6MTc1MDIzNDg0MSwiZXhwIjoxNzUwMjM1MDIxLCJpYXQiOjE3NTAyMzQ4NDEsImlzcyI6Imh0dHBzOi8vbWlycmEucGx1cy8iLCJhdWQiOiJodHRwczovL21pcnJhLnBsdXMvIn0.GIxP826oyoKm2aIw4SFYO5mb6lKh5SJb0IriJcnrdwI",
          "refresh": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9.eyJuYW1laWQiOiI5YWM1MTMwZi1lNmU5LTQ1ZWItYjFmZS0zMmRjM2VlMGU4Y2QiLCJuYmYiOjE3NTAyMzQ4NDEsImV4cCI6MTc1MDgzOTY0MSwiaWF0IjoxNzUwMjM0ODQxLCJpc3MiOiJodHRwczovL21pcnJhLnBsdXMvIiwiYXVkIjoiaHR0cHM6Ly9taXJyYS5wbHVzLyJ9.C7ODJJA--KIOATqNjEGg0EEv7WXztRwtbHXG484JSGo"
        } 
      ```  

  - Получение платежей (`GET /payments?take=N`):  
    ```bash  
    curl -X 'GET' \  
      'http://localhost:5000/payments?take=1' \  
      -H 'accept: application/json'  
    ```  
    - Ответ:  
      ```json  
      [  
        {  
          "id": "f46d3368-fbc8-4098-8f13-dfa0442fff3f",  
          "clientId": "d34f4654-e1ed-4247-aa7b-1ded5071244c",  
          "amount": 150,  
          "currency": "USD",  
          "date": "2025-06-16T17:52:19.2492089",  
          "description": "Тестовый платеж",  
          "status": "Completed"  
        }  
      ]  
      ```  

- Дополнительно:  
  - Postman коллекция: `Postman example.json`  