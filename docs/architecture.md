# Architektúra leírás

## Áttekintés

A projekt egy egyszerű feladatkezelő rendszer, amely több komponensből álló architektúrával készül.

A cél egy olyan rendszer kialakítása, amely nemcsak alkalmazáslogikai szinten működik, hanem fejlesztési, konténerizációs, CI/CD és telepítési szempontból is rendezett felépítésű.

## Fő komponensek

A rendszer fő elemei:

- `WebUI`
- `TaskService`
- `McpService`
- `MongoDB`

## Komponensek szerepei

### WebUI

A `WebUI` a rendszer frontend komponense, amely Angular alapon készül.

Feladatai:

- feladatok megjelenítése
- új feladat létrehozása
- meglévő feladat szerkesztése
- feladat törlése
- státusz módosítása
- keresés és szűrés

A frontend a `TaskService` API-jával kommunikál.

### TaskService

A `TaskService` a fő backend komponens, amely ASP.NET alapon készül.

Feladatai:

- a feladatok kezeléséhez tartozó üzleti logika megvalósítása
- CRUD műveletek biztosítása
- szűrési és keresési műveletek kezelése
- adatbázis-kommunikáció megvalósítása

A `TaskService` közvetlenül a MongoDB adatbázist használja.

### McpService

A `McpService` külön backend komponensként készül.

Feladatai:

- MCP műveletek biztosítása a feladatkezelő domainhez
- a feladatok lekérdezése és kezelése MCP toolokon keresztül
- kapcsolódás a `TaskService` által biztosított funkciókhoz vagy azonos domain logikához

Tervezett MCP műveletek:

- `GetTasks`
- `SearchTasks`
- `CreateTask`
- `UpdateTaskStatus`
- `DeleteTask`

### MongoDB

A MongoDB a rendszer perzisztens adattárolója.

Feladatai:

- feladatok tárolása
- módosítások megőrzése
- lekérdezések kiszolgálása a backend számára

## Domain modell

A rendszer elsődleges domain entitása a `Task`.

Tervezett mezői:

- `id`
- `title`
- `description`
- `status`
- `priority`
- `dueDate`
- `createdAt`
- `updatedAt`

A `status` és a `priority` előre definiált értékkészletből választott mezők lesznek.

## Kapcsolatok

A rendszer fő kommunikációs irányai:

- a felhasználó a `WebUI` felületen keresztül használja a rendszert
- a `WebUI` HTTP API-n keresztül hívja a `TaskService` komponenst
- a `TaskService` a MongoDB adatbázissal kommunikál
- a `McpService` külön komponensként MCP műveleteket biztosít a feladatkezelő domainhez

## Telepítési szemlélet

A rendszer minden fő komponense konténerizált formában készül.

Tervezett telepítési környezet:

- Docker alapú build
- GitHub Actions alapú CI
- Kubernetes alapú futtatás
- ArgoCD alapú CD

A MongoDB helyi Kubernetes klaszteren Helm chart segítségével kerül telepítésre.

## Tervezési alapelvek

A projekt kialakításánál a következő szempontok a meghatározók:

- egyszerű domain
- jól elkülönülő komponensek
- könnyen bemutatható működés
- tárgyi követelmények teljes lefedése
- rendezett repository-struktúra
- fokozatosan felépíthető fejlesztési folyamat
