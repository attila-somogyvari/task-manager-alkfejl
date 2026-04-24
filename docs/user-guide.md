# Felhasznaloi utmutato

## Bevezetes

A rendszer egy feladatkezelo alkalmazas, amely lehetove teszi a felhasznalo szamara a feladatok nyilvantartasat es kezeleset.

A felhasznalo a webes feluleten keresztul tudja hasznalni a rendszer fo funkcioit.

## A rendszer celja

Az alkalmazas celja, hogy a felhasznalo egyszeruen tudjon:

- feladatokat megtekinteni
- feladatokat keresni
- statusz szerint szurni
- feladat statuszat modosittani
- feladatokat torolni

## Kezelt adatok

A rendszer feladatokat kezel.

Egy feladat a kovetkezo adatokat tartalmazhatja:

- cim
- leiras
- allapot
- prioritas
- hatarido
- letrehozas ideje
- modositas ideje

## WebUI fo funkciok

### Feladatok listazasa

A felhasznalo megtekintheti az osszes rogzitett feladatot egy listanezetben.

### Kereses

A feluleten szoveges keresomezo segiti a gyors szurest.

### Statusz szerinti szures

A felhasznalo valaszthat az alabbi nezetek kozott:

- `All`
- `Todo`
- `InProgress`
- `Done`

### Frissites

A `Frissites` gomb ujra lekerni a backendtol a legfrissebb allapotot.

### Kovetkezo statusz

A feladatkartyakon elerheto gomb a kovetkezo allapotba lepteti a feladatot:

- `Todo` -> `InProgress`
- `InProgress` -> `Done`
- `Done` -> `Todo`

### Torles

A `Torles` gomb eltavolitja a kivalasztott feladatot.

## Backend API hasznalat

A `TaskService` fo vegpontja a `/tasks`.

Peldak:

- `GET /tasks`
- `GET /tasks?status=Todo`
- `GET /tasks?search=teszt`
- `POST /tasks`
- `PATCH /tasks/{id}/status`
- `DELETE /tasks/{id}`

## MCP komponens szerepe

A rendszer resze egy kulon MCP komponens is.

Ennek szerepe, hogy a feladatkezelo domain bizonyos muveletei MCP eszkozokon keresztul is elerhetok legyenek.

Biztositott muveletek:

- feladatok lekerdezese
- keresesi muveletek
- uj feladat letrehozasa
- statuszmodositas
- torles

## Futtatasi modok

A rendszer tobb modon is futtathato:

- Dev Container alapu fejlesztoi kornyezetben
- Docker Compose stackkent
- minikube klaszteren Kubernetes manifestekkel
- ArgoCD altali GitOps szinkronizacioval

## Megjegyzes

A telepitesi lepesek es az uzemeltetesi reszletek a [Telepitesi utmutato](./deployment-guide.md) dokumentumban talalhatok.
