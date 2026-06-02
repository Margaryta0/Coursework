# Coursework 
# 📅 Electronic Schedule System
 
---
 
## 📋 Опис проекту
 
Консольне застосування для управління електронним розкладом навчального закладу. 
 
---
 
## 🛠️ Технології
 
| Категорія | Технологія |
|---|---|
| Мова | C# |
| База даних | MS SQL |
| API | ASP.NET WebAPI |
| Авторизація | JWT Bearer |
| Маппінг | AutoMapper |
| Тести | xUnit + Moq |
| DI | Microsoft.Extensions.DependencyInjection |
 
---
 
## 🏗️ Архітектура
 
Проект побудований на основі **трирівневої архітектури**:
 
```
[Console UI] ──HTTP──► [ASP.NET WebAPI] ──► [BLL] ──► [DAL] ──► [MS SQL]
```
 
```
ScheduleSystem.sln
├── ScheduleSystem.DAL          # Рівень доступу до даних
├── ScheduleSystem.BLL          # Рівень бізнес-логіки
├── ScheduleSystem.WebAPI       # Рівень представлення (ASP.NET WebAPI)
├── ScheduleSystem.ConsoleUI    # Консольний UI клієнт
└── ScheduleSystem.Tests        # Модульні тести (xUnit)
```
 
---
