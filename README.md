# Task Manager - Alkalmazasfejlesztes technologiai

Ez a repo az **Alkalmazasfejlesztes technologiai** targyhoz keszult projektunket tartalmazza.

A projekt celja egy feladatkezelo rendszer megvalositasa volt, amely a modern (relativ) alkalmazasfejlesztes fobb lepeseit egy nyomonkovetheto megoldason keresztul mutatja be: a fejlesztoi kornyezettol a konteneres futtatason at egeszen a Kubernetes es ArgoCD alapu telepitesig.

## Task Manager

A rendszer maga egy feladatkezelo alkalmazas, amelyben a kovetkezo muveletek vegezhetok:

- feladatok listazasa
- uj feladatok letrehozasa
- feladatok torlese
- statusz modositas (ToDO - InProgress - Done)
- kereses es szures

## Fo komponensek

A rendszer fo elemei:

- `WebUI` - Angular alapu frontend
- `TaskService` - ASP.NET alapu backend szolgaltatas
- `McpService` - MCP komponens
- `MongoDB` - adatbazis

## Hasznalt technologiak

- Frontend: Angular, TypeScript
- Backend: ASP.NET, C#
- Adatbazis: MongoDB
- Kontenerizacio: Docker, Docker Compose
- CI: GitHub Actions
- Telepites: Kubernetes, Helm
- CD: ArgoCD

## Repo felepitese

- `.devcontainer/` - fejlesztoi kornyezet konfig
- `.github/workflows/` - CI workflow
- `deployment/` - Kubernetes es ArgoCD telepitesi allomanyok
- `diagrams/` - architektura abrak
- `docs/` - projekt dokumentacio
- `source/` - forraskod

## Dokumentacio

A reszletes dokumentacio a `docs` mappaban talalhato:

- [Architektura leiras](./docs/architecture.md)
- [Telepitesi utmutato](./docs/deployment-guide.md)
- [Felhasznaloi utmutato](./docs/user-guide.md)

## Jelenlegi allapot

A projekt jelenleg tartalmazza:

- domain modell implementaciojat (feladatkezeles)
- `TaskService` backend API-t MongoDB perzisztenciaval
- `McpService` MCP eszkozeit
- Angular frontendet
- Docker Compose alapu teljes stack futtatast
- GitHub Actions CI workflow-kat
- local es prod Kubernetes manifesteket
- ArgoCD alapu CD konfigot

## Gyors inditas

Lokalis konteneres futtatas:

```powershell
docker compose build
docker compose up
```

Fo eleresi pontok:

- `WebUI`: [http://localhost:4200](http://localhost:4200)
- `TaskService`: [http://localhost:5095/tasks](http://localhost:5095/tasks)
- `McpService`: [http://localhost:5044/mcp](http://localhost:5044/mcp)

Kubernetes es ArgoCD telepitesi lepesek:

- [docs/deployment-guide.md](./docs/deployment-guide.md)

## Megjegyzes

A projekt kialakitasanal a kapott mintarepo referenciakent szolgalt.