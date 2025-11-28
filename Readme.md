# Impulse Club - API de Gestión de Clubes Deportivos

## 📌 1. Explicación del Proyecto
Impulse Club es un sistema web desarrollado con **.NET 9** para la gestión integral de **clubes deportivos**, entrenamientos, recursos y miembros.  
Permite que los usuarios se registren, se unan a clubes, participen en entrenamientos y reserven recursos deportivos (balones, canchas, pesas, etc.).  

El objetivo principal es ofrecer una **interfaz segura, rápida y escalable**, con gestión de roles y relaciones complejas entre entidades.

---

## 🧩 2. Arquitectura del Sistema
El sistema sigue una **Arquitectura por Capas (Layered Architecture)** usando **Entity Framework Core** y JWT:

- **Controllers:** Manejan las peticiones HTTP.
- **Services:** Contienen la lógica de negocio y validaciones.
- **Repositories:** Acceso a datos y consultas a la base.
- **Models/Entities:** Definición de las entidades y sus relaciones.
- **DTOs:** Transferencia de datos entre cliente y servidor.

---

## 🔹 3. Funcionalidades del Sistema

### Gestión de Usuarios
- Registro e inicio de sesión seguro.
- Autenticación con **JWT** y renovación de tokens.
- Roles: `Admin`, `User`, `Founder`.

### Gestión de Clubes Deportivos
- CRUD completo de clubes (solo Founder/Admin puede crear y editar).
- Unirse a clubes.
- Gestión de miembros.

### Gestión de Entrenamientos
- CRUD completo de entrenamientos por club.
- Participación de usuarios.
- Asignación de recursos a entrenamientos.

### Gestión de Recursos
- CRUD completo de recursos (solo Founder/Admin).
- Reservas de recursos por usuarios.
- Control de disponibilidad y estado (`Disponible`, `En Uso`, `Mantenimiento`).

---

## 🏛️ 4. Entidades y Relaciones

| Entidad       | Atributos Principales                                   | Relaciones                                   |
| ------------- | ------------------------------------------------------ | ------------------------------------------- |
| **Usuario**   | Id, Nombre, Email, PasswordHash, Rol                   | 1:1 con Club (si es Founder), N:M con Club, N:M con Entrenamiento, N:M con Recurso |
| **Club**      | Id, Nombre, TipoDeDeporte, FounderId                  | 1:1 con Usuario, 1:N Entrenamientos, 1:N Recursos, N:M Miembros |
| **Entrenamiento** | Id, Nombre, Fecha, Duración, ClubId, RecursosUsados | 1:N con Club, N:M con Usuario, N:M con Recursos |
| **Recurso**   | Id, Nombre, Tipo, CantidadTotal, Estado, ClubId       | 1:N con Club, N:M con Usuario |

---

## 🔐 5. Autenticación y Roles
- **JWT** para asegurar las peticiones.
- Roles del sistema:
  - **Admin:** Acceso total a todos los recursos.
  - **Founder:** Puede crear/editar su club, entrenamientos y recursos.
  - **User:** Puede unirse a clubes, entrenamientos y reservar recursos.

**Uso del Token:**  
En los endpoints protegidos, agregar en header:
Authorization: Bearer <tu_token_jwt_aqui>

---

## 🌐 6. Endpoints de la API

### **AuthController**
| Método | Endpoint           | Permiso | Body (JSON) |
| ------ | ----------------- | ------- | ----------- |
| POST   | `/api/v1/auth/register` | Público | `{ "name": "Juan", "email": "juan@mail.com", "password": "123456", "role": "User" }` |
| POST   | `/api/v1/auth/login`    | Público | `{ "email": "juan@mail.com", "password": "123456" }` |
| POST   | `/api/v1/auth/refresh`  | Público | `{ "token": "tu_refresh_token" }` |

---

### **ClubController**
| Método | Endpoint                    | Permiso | Body (JSON) |
| ------ | --------------------------- | ------- | ----------- |
| GET    | `/api/v1/club`              | Público | N/A |
| GET    | `/api/v1/club/{id}`         | Público | N/A |
| POST   | `/api/v1/club`              | Founder/Admin | `{ "name": "Futbol Club Cochabamba", "sportType": "Futbol" }` |
| PUT    | `/api/v1/club/{id}`         | Founder/Admin | `{ "name": "Futbol Club Cochabamba", "sportType": "Futbol" }` |
| DELETE | `/api/v1/club/{id}`         | Admin | N/A |
| POST   | `/api/v1/club/{id}/join`    | Usuario | N/A |
| GET    | `/api/v1/club/{id}/members` | Usuario | N/A |

---

### **ResourceController**
| Método | Endpoint                        | Permiso | Body (JSON) |
| ------ | ------------------------------- | ------- | ----------- |
| GET    | `/api/v1/resource`              | Público | N/A |
| GET    | `/api/v1/resource/{id}`         | Público | N/A |
| POST   | `/api/v1/resource`              | Founder/Admin | `{ "name": "Balón Oficial", "type": "Ball", "totalQuantity": 10, "status": "Available", "clubId": "guid_del_club" }` |
| PUT    | `/api/v1/resource/{id}`         | Founder/Admin | `{ "name": "Balón Oficial", "type": "Ball", "totalQuantity": 10, "status": "Available", "clubId": "guid_del_club" }` |
| DELETE | `/api/v1/resource/{id}`         | Founder/Admin | N/A |
| POST   | `/api/v1/resource/{id}/reserve` | Usuario | `{ "quantity": 2 }` |

---

### **TrainingController**
| Método | Endpoint                         | Permiso | Body (JSON) |
| ------ | -------------------------------- | ------- | ----------- |
| GET    | `/api/v1/training`               | Público | N/A |
| GET    | `/api/v1/training/{id}`          | Público | N/A |
| GET    | `/api/v1/training/club/{clubId}`| Público | N/A |
| POST   | `/api/v1/training`               | Founder/Admin | `{ "name": "Entrenamiento Matutino", "date": "2025-12-01T08:00:00Z", "duration": 90, "clubId": "guid_del_club", "usedResources": "[{\"ResourceId\": \"guid_del_recurso\", \"Quantity\": 2}]" }` |
| PUT    | `/api/v1/training/{id}`          | Founder/Admin | Igual que POST |
| DELETE | `/api/v1/training/{id}`          | Founder/Admin | N/A |
| POST   | `/api/v1/training/{id}/join`     | Usuario | N/A |

---

## 📝 7. Consejos para probar en Postman
1. **Orden recomendado:**
   1. `register` → `login` → obtener JWT
   2. Crear **Club**
   3. Crear **Resource** y **Training**
   4. Reservar recursos / unirse a entrenamientos


