# Zastosowane wzorce projektowe

## 1. Singleton  
- **NavigationService.cs** (`NavigationService.Instance`)  
  **Powód:** Zapewnia jedną, globalną instancję usługi nawigacji dla całej aplikacji — łatwy dostęp z dowolnego miejsca.  

## 2. Facade (Fasada)  
- **HttpAuthenticationApiClient.cs**  
  **Powód:** Ukrywa złożone API `HttpClient`, JSON-serializację i obsługę HTTP pod prostą metodą `LoginAsync`.  
