# WebVoluntariado

**WebVoluntariado** es una plataforma web desarrollada para la gestión de programas de voluntariado, orientada a conectar organizaciones con personas interesadas en participar en actividades sociales y comunitarias.

El proyecto fue desarrollado con un enfoque académico y práctico, aplicando principios de **ingeniería de software**, **arquitectura por capas** y **buenas prácticas de desarrollo web**, utilizando tecnologías modernas tanto en el backend como en el frontend.

---

## Introducción

WebVoluntariado surge como una solución tecnológica para facilitar la administración de voluntarios, actividades y procesos de participación en organizaciones sociales. La plataforma permite gestionar usuarios, roles y actividades de voluntariado, garantizando una estructura clara, segura y escalable.

El proyecto se diseñó bajo una arquitectura modular, separando responsabilidades entre la capa de presentación, lógica de negocio y acceso a datos, lo que permite su mantenimiento y evolución futura.

---

## Objetivo del proyecto

Desarrollar una aplicación web que permita:

- Gestionar usuarios con distintos roles (administrador y voluntario).
- Administrar actividades y oportunidades de voluntariado.
- Facilitar la inscripción y seguimiento de voluntarios.
- Aplicar conceptos fundamentales de seguridad, persistencia de datos y arquitectura web.

---

## Alcance

En su versión actual, la plataforma contempla:

- Registro e inicio de sesión de usuarios.
- Gestión de roles (administrador y voluntario).
- CRUD de actividades de voluntariado.
- Inscripción de voluntarios en actividades disponibles.
- Visualización del historial de participación.
- Interfaz web funcional y estructurada.

El sistema está preparado para ampliaciones futuras, como notificaciones, reportes y métricas de impacto social.

---

## Arquitectura

El proyecto sigue una **arquitectura por capas**, organizada de la siguiente manera:

- **Frontend:** Interfaz de usuario desarrollada con tecnologías web modernas.
- **Backend:** API REST desarrollada con **ASP.NET Core**.
- **Persistencia:** Base de datos relacional gestionada mediante **Entity Framework Core**.
- **Seguridad:** Manejo de autenticación y autorización basada en roles.

---

## Tecnologías utilizadas

### Backend
- ASP.NET Core
- Entity Framework Core
- SQL Server
- Arquitectura MVC
- Autenticación y autorización

### Frontend
- HTML5
- CSS3
- Bootstrap
- Razor Pages

---

## Requerimientos funcionales

- **RF1.** El sistema debe permitir el registro e inicio de sesión de usuarios.
- **RF2.** El sistema debe gestionar roles de usuario (administrador y voluntario).
- **RF3.** El administrador debe poder crear, editar y eliminar actividades de voluntariado.
- **RF4.** El voluntario debe poder visualizar e inscribirse en actividades disponibles.
- **RF5.** El sistema debe permitir consultar el historial de actividades realizadas.
- **RF6.** El sistema debe validar la información ingresada por los usuarios.

---

## Requerimientos no funcionales

- **RNF1.** El sistema debe seguir el patrón MVC.
- **RNF2.** La aplicación debe garantizar la integridad y seguridad de los datos.
- **RNF3.** El código debe estar organizado por capas y responsabilidades.
- **RNF4.** La plataforma debe ser escalable y mantenible.
- **RNF5.** El sistema debe ofrecer una interfaz clara y accesible.

---

## Posibles mejoras futuras

- Implementación de notificaciones por correo.
- Panel de reportes y métricas de participación.
- Integración con servicios externos.
- Optimización de seguridad y control de accesos.
- Migración a arquitectura basada en servicios.

---

## Autor

**Jose Leonardo Chavarro**  
Estudiante de Ingeniería de Sistemas y Computación  
Proyecto académico orientado al desarrollo de aplicaciones web con impacto social.
