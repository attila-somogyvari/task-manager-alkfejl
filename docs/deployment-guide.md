# Telepítési útmutató

## Cél

Ez a dokumentum bemutatja, hogyan lehet a projektet helyi környezetben futtatni és később Kubernetes klaszterre telepíteni.

A dokumentum jelenleg az előkészített projektstruktúrához tartozó alap telepítési tervet tartalmazza. A fejlesztés előrehaladtával a konkrét parancsok és pontos lépések is bekerülnek.

## Előfeltételek

A projekt futtatásához és telepítéséhez az alábbi eszközök szükségesek:

- Git
- Docker Desktop
- Visual Studio Code
- Dev Containers bővítmény
- minikube
- kubectl
- Helm

A fejlesztés során a szükséges fejlesztői eszközök jelentős része Dev Container környezetben lesz használva.

## Repository klónozása

A projekt klónozása GitHubról történik.

Példa:

```bash
git clone https://github.com/attila-somogyvari/task-manager-alkfejl.git
cd task-manager-alkfejl
```

## Fejlesztői környezet

A projekt tervezetten Dev Container alapú fejlesztői környezetet használ.

A cél az, hogy a szükséges SDK-k és CLI eszközök ne közvetlenül a host operációs rendszerre kerüljenek, hanem konténeres fejlesztői környezetben legyenek elérhetők.

A későbbi verzióban itt kerülnek dokumentálásra:

- a `.devcontainer` konfiguráció használata
- a konténer indítása
- a fejlesztői eszközök elérhetősége

## Konténerizáció

A rendszer fő komponensei külön Docker image-ként készülnek:

- `WebUI`
- `TaskService`
- `McpService`

A buildelt image-ek CI workflow segítségével registry-be kerülnek feltöltésre.

A későbbi verzióban itt kerülnek dokumentálásra:

- lokális build lépések
- image nevek
- registry elérési utak

## Kubernetes telepítés

A rendszer helyi Kubernetes klaszteren futtatható.

Tervezett komponensek:

- `WebUI`
- `TaskService`
- `McpService`
- `MongoDB`

A telepítési leírók a `deployment/` mappában kapnak helyet.

### Tervezett mappák

- `deployment/local/`
- `deployment/prod/`
- `deployment/argocd/`

### Local környezet

A `local` mappa a helyi klaszteres futtatáshoz szükséges manifesteket tartalmazza.

Itt lesznek például:

- namespace
- secret
- service
- deployment

### Prod környezet

A `prod` mappa a registry-be feltöltött image-eket használó manifesteket tartalmazza.

## MongoDB telepítése

A MongoDB tervezetten Helm chart segítségével kerül telepítésre helyi Kubernetes klaszterre.

A későbbi verzióban itt kerülnek dokumentálásra:

- a konkrét Helm parancsok
- a namespace használata
- a kapcsolat ellenőrzése

## ArgoCD

Az 5-ös szintű követelmény részeként a rendszer CD oldala ArgoCD segítségével kerül kialakításra.

A dokumentáció későbbi verziójában itt kerülnek leírásra:

- ArgoCD telepítése helyi klaszterre
- az Application erőforrás létrehozása
- a szinkronizáció ellenőrzése

## Ellenőrzés

A későbbi verzióban itt kerülnek összegyűjtésre azok az ellenőrzési lépések, amelyek alapján megállapítható, hogy a rendszer sikeresen települt.

Például:

- Podok futása
- Service-ek elérhetősége
- frontend megnyitása
- backend API válaszai
- MCP működésének ellenőrzése

## Megjegyzés

Ez a dokumentum jelenleg a telepítési terv első, előkészítő verziója. A fejlesztés előrehaladtával konkrét parancsokkal, fájlnevekkel és ellenőrzési lépésekkel lesz bővítve.
