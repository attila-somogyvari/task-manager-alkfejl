# Architektura leiras

## Attekintes

A projekt egy feladatkezelo rendszer, amely tobb komponensbol allo architekturaval keszult.

A cel egy olyan rendszer kialakitasa, amely nemcsak alkalmazaslogikai szinten mukodik, hanem fejlesztesi, kontenerizacios, CI/CD es telepitesi szempontbol is rendezett felepitesu.

## Fo komponensek

A rendszer fo elemei:

- `WebUI`
- `TaskService`
- `McpService`
- `MongoDB`

## Komponensek szerepei

### WebUI

A `WebUI` a rendszer Angular alapu frontendje.

Feladatai:

- a feladatok megjelenitese
- statusz szerinti szures
- keresesi feltetelek kezelese
- ujratoltes
- statuszmodositas inditasa
- torles inditasa

A frontend HTTP API-n keresztul a `TaskService` komponenssel kommunikal.

### TaskService

A `TaskService` a fo backend szolgaltatas, amely ASP.NET minimal API alapon keszult.

Feladatai:

- a feladatokhoz tartozo uzleti logika megvalositasa
- CRUD jellegu API biztositas
- szuresi es keresesi muveletek kiszolgalasa
- MongoDB perzisztencia kezelese

A `TaskService` kozvetlenul a MongoDB adatbazist hasznalja.

### McpService

A `McpService` kulon backend komponenskent keszult.

Feladatai:

- MCP toolok biztositasa a feladatkezelo domainhez
- a `TaskService` vegpontjainak elerese HTTP kliensen keresztul
- MCP alapu kerdezesek es modositasok kiszolgalasa

Megvalositott MCP muveletek:

- `GetTasks`
- `SearchTasks`
- `CreateTask`
- `UpdateTaskStatus`
- `DeleteTask`

### MongoDB

A MongoDB a rendszer perzisztens adattaroloja.

Feladatai:

- feladatok tarolasa
- modositasok megorzese
- lekerdezesek kiszolgalasa a backend szamara

## Domain modell

A rendszer elsoleges domain entitasa a `Task`.

Fobb mezoi:

- `id`
- `title`
- `description`
- `status`
- `priority`
- `dueDate`
- `createdAt`
- `updatedAt`

A `status` es a `priority` enum alapu mezok:

- `TaskStatus`: `Todo`, `InProgress`, `Done`
- `TaskPriority`: `Low`, `Medium`, `High`

## Kommunikacios kapcsolatok

A rendszer fo kommunikacios iranyai:

- a felhasznalo a `WebUI` feluleten keresztul hasznalja a rendszert
- a `WebUI` a `TaskService` HTTP API-t hivja
- a `TaskService` a MongoDB adatbazissal kommunikal
- a `McpService` a `TaskService` API-ra epitve biztosit MCP toolokat

## Telepitesi szemlelet

A rendszer minden fo komponense kontenerizalt formaban keszul.

A telepitesi lanc elemei:

- Dev Container alapu fejlesztoi kornyezet
- Docker es Docker Compose alapu lokalis futtatas
- GitHub Actions alapu CI
- Kubernetes alapu futtatas
- Helm alapu MongoDB telepites
- ArgoCD alapu CD

## Tervezesi alapelvek

A projekt kialakitasanal a kovetkezo szempontok voltak meghatarozok:

- egyszeru domain
- jol elkulonulo komponensek
- konnyen bemutathato mukodes
- targyi kovetelmenyek teljes lefedese
- rendezett repository-struktura
- fokozatosan felepitheto fejlesztesi folyamat
