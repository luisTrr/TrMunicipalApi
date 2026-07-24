# 📚 Proyecto .NET 8 - Clean Architecture con Minimal API

## 🏗️ Arquitectura del Proyecto

Este proyecto está desarrollado en **.NET 8** utilizando:

- ✅ Clean Architecture
- ✅ Minimal API
- ✅ Entity Framework Core
- ✅ Patrón Repositorio
- ✅ Inyección de Dependencias (Dependency Injection)
- ✅ Patrón Result

---

# 📂 Estructura de Capas

La solución está organizada en capas siguiendo el principio de separación de responsabilidades.

---

# 1️⃣ 🧠 Domain (Dominio)

## 📌 Responsabilidad
Contiene el núcleo del negocio.

## Contiene:
- Entidades
- Value Objects
- Enumeraciones
- Interfaces (contratos)
- Reglas de negocio puras

## 🚫 No debe depender de:
- Infrastructure
- Base de datos
- Frameworks externos

> Es la capa más importante y completamente independiente.

---

# 2️⃣ ⚙️ Application (Aplicación)

## 📌 Responsabilidad
Orquesta los casos de uso del sistema.

## Contiene:
- DTOs
- Interfaces de servicios
- Interfaces de repositorios
- Casos de uso
- Validaciones
- Mapeos

## 🔁 Flujo
Recibe solicitudes desde la API → ejecuta reglas → llama a repositorios → devuelve resultados.

---

# 3️⃣ 🗄️ Infrastructure (Infraestructura)

## 📌 Responsabilidad
Implementación técnica.

## Contiene:
- DbContext (Entity Framework)
- Implementaciones de repositorios
- Servicios externos (S3, APIs, Email, etc.)
- Configuración de acceso a datos

Aquí se conectan:
- SQL Server
- Servicios externos
- APIs de terceros

> Esta capa implementa las interfaces definidas en Application.

---

# 4️⃣ 🌐 WebApi (Minimal API)

## 📌 Responsabilidad
Exponer endpoints HTTP.

Este proyecto utiliza **Minimal API**, una forma simplificada de crear APIs en .NET 8 sin necesidad de Controllers tradicionales.

### Ejemplo:

```csharp
app.MapGet("/users", async (IUserService service) =>
{
    return await service.GetAllAsync();
});
