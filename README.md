# Task Manager - Alkalmazásfejlesztés technológiái projekt

Ez a repository az **Alkalmazásfejlesztés technológiái** tárgyhoz készülő projektet tartalmazza.

A projekt célja egy egyszerű, de technológiailag teljes rendszer megvalósítása, amely a fejlesztéstől a telepítésig bemutatja egy modern alkalmazás életciklusának főbb lépéseit.

## Projekt célja

A rendszer egy feladatkezelő alkalmazás lesz, amelyben a felhasználó feladatokat tud:

- listázni
- létrehozni
- szerkeszteni
- törölni
- állapot szerint kezelni
- egyszerűen keresni és szűrni

A projekt célja nem egy bonyolult domain modell kialakítása, hanem egy jól strukturált, több komponensből álló rendszer elkészítése.

## Fő komponensek

A rendszer tervezett fő komponensei:

- `WebUI` - Angular alapú frontend
- `TaskService` - ASP.NET alapú backend szolgáltatás
- `McpService` - külön MCP komponens
- `MongoDB` - adatbázis

## Tervezett technológiák

A projekt jelenlegi technológiai iránya:

- Frontend: Angular
- Backend: ASP.NET
- Adatbázis: MongoDB
- Konténerizáció: Docker
- CI: GitHub Actions
- Telepítés: Kubernetes
- CD: ArgoCD

## Repository felépítése

A projekt főbb mappái:

- `.devcontainer/` - fejlesztői környezet konfiguráció
- `.github/workflows/` - CI workflow-k
- `deployment/` - Kubernetes és ArgoCD telepítési fájlok
- `diagrams/` - architektúra ábrák
- `docs/` - projekt dokumentáció
- `source/` - forráskód

## Dokumentáció

A részletes dokumentáció a `docs` mappában található:

- [Architektúra leírás](./docs/architecture.md)
- [Telepítési útmutató](./docs/deployment-guide.md)
- [Felhasználói útmutató](./docs/user-guide.md)

## Jelenlegi állapot

A repository jelenleg az alap projektstruktúrát és a tervezési kiindulópontot tartalmazza. A fejlesztés lépésről lépésre fog felépülni a backend, frontend, MCP, Docker, Kubernetes és ArgoCD részekkel együtt.

## Megjegyzés

A projekt kialakításánál a kapott mintarepo referenciaként és inspirációként szolgál, de a megoldás nem annak egy az egyben történő másolata.
