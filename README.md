# Hacker News Best Stories API

## Descripción

Esta es una API RESTful creada con ASP.NET Core que permite recuperar los detalles de las mejores historias de Hacker News, según su puntuación. La API consulta el Hacker News API y devuelve una lista de las mejores `n` historias en orden descendente de puntuación.

## Requisitos

- .NET 3.1 SDK o superior
- Una conexión a Internet para acceder al Hacker News API

## Acceso a al API

GET /api/stories?n={number_of_stories}
## Ejemplo de Response
[
  {
    "title": "A uBlock Origin update was rejected from the Chrome Web Store",
    "uri": "https://github.com/uBlockOrigin/uBlock-issues/issues/745",
    "postedBy": "ismaildonmez",
    "time": "2019-10-12T13:43:01+00:00",
    "score": 1716,
    "commentCount": 572
  },
  {
    "title": "Otra historia de ejemplo",
    "uri": "https://example.com/story",
    "postedBy": "exampleuser",
    "time": "2020-01-01T10:00:00+00:00",
    "score": 1500,
    "commentCount": 300
  }
  // más historias...
]

## Mejoras y Cambios Propuestos

* Implementacion de pruebas unitarias para asegurar todos loc omponentes de la apliacion funcionan como se tiene esperado
* Autenticacion y autorizacion para controlar el acceso a la api
* Nuevas apis para obtener comentarios entre las noticias 
* Implementacion de nueva estrategia de caching para el manejo de grandes volumenes de solicitudes
* Configurar el despliegye de la apliacion con un proveedor de nube 


## Contacto

Para cualquier consulta o sugerencia, por favor contacta a fsalmonl@outlook.com