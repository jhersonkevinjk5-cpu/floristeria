# 🌸 Floristería Web Application

Sistema web desarrollado para la gestión y comercialización de productos florales, diseñado bajo el patrón de arquitectura MVC utilizando tecnologías del ecosistema .NET.

El proyecto permite administrar productos, categorías, pedidos y usuarios, ofreciendo una experiencia orientada tanto a clientes como a la gestión interna del negocio.

---

## 📌 Descripción del Proyecto

**Floristería Web Application** es una plataforma web enfocada en la venta y administración de arreglos florales, permitiendo gestionar el catálogo de productos, procesar pedidos y brindar una experiencia organizada dentro de un entorno comercial digital.

Este proyecto fue desarrollado con fines académicos y como práctica de desarrollo full stack utilizando ASP.NET Framework.

---

## 🚀 Tecnologías Utilizadas

* ASP.NET MVC 5
* C#
* .NET Framework 4.8
* SQL Server
* ADO.NET / Entity Framework *(según lo que uses)*
* HTML5
* CSS3
* JavaScript
* Bootstrap
* Visual Studio 2022
* Git & GitHub

---

## ⚙️ Funcionalidades Principales

### Cliente

* Visualización del catálogo de productos
* Filtrado de productos por categorías
* Visualización detallada de productos
* Carrito de compras
* Registro e inicio de sesión
* Realización de pedidos

### Administración

* Gestión de productos (CRUD)
* Gestión de categorías
* Gestión de usuarios
* Administración de pedidos
* Gestión de inventario
* Panel administrativo

---

## 🏗 Arquitectura del Proyecto

El proyecto sigue el patrón de arquitectura **MVC (Model - View - Controller)**.

```text
WebApplicationfloristeria/
│
├── Controllers/        -> Lógica de control
├── Models/             -> Entidades y acceso a datos
├── Views/              -> Interfaz de usuario
├── Scripts/            -> JavaScript
├── Content/            -> CSS, imágenes, multimedia
├── App_Start/          -> Configuración inicial
└── Web.config          -> Configuración general
```

---

## 🗄 Base de Datos

El sistema utiliza **SQL Server** como gestor de base de datos.

Principales entidades:

* Usuarios
* Roles
* Productos
* Categorías
* Pedidos
* DetallePedido
* Clientes
* Inventario

---

## 🔧 Instalación y Ejecución

### 1. Clonar repositorio

```bash
git clone https://github.com/jhersonkevinjk5-cpu/floristeria.git
```

### 2. Abrir solución

Abrir el archivo:

```text
WebApplicationfloristeria.sln
```

en Visual Studio.

### 3. Restaurar paquetes

Restaurar dependencias NuGet automáticamente desde Visual Studio.

### 4. Configurar base de datos

* Crear base de datos en SQL Server
* Ejecutar script SQL correspondiente
* Configurar cadena de conexión en:

```text
Web.config
```

### 5. Ejecutar proyecto

Presionar:

```text
F5
```

o ejecutar desde IIS Express.

---

## 📷 Capturas del Sistema

*(Agregar screenshots del sistema aquí)*

Ejemplo:

* Página principal
* Catálogo de productos
* Panel administrativo
* Carrito de compras
* Formulario de pedidos

---

## 📂 Control de Versiones

El proyecto utiliza:

* Git
* GitHub

Repositorio oficial:

https://github.com/jhersonkevinjk5-cpu/floristeria

---

## 👨‍💻 Autor

**Jherson Serna**

Proyecto desarrollado como parte del proceso de aprendizaje en desarrollo web y arquitectura MVC utilizando tecnologías .NET.

GitHub:

https://github.com/jhersonkevinjk5-cpu

---

## 📄 Licencia

Proyecto desarrollado con fines educativos.

Uso libre para aprendizaje y referencia académica.
