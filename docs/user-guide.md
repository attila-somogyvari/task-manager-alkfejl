# Felhasználói útmutató

## Bevezetés

A rendszer egy egyszerű feladatkezelő alkalmazás, amely lehetővé teszi a felhasználó számára a feladatok nyilvántartását és kezelését.

A felhasználó a webes felületen keresztül tudja használni a rendszer fő funkcióit.

## A rendszer célja

Az alkalmazás célja, hogy a felhasználó egyszerűen tudjon:

- feladatokat létrehozni
- meglévő feladatokat módosítani
- feladatokat törölni
- a feladatok állapotát követni
- keresést és szűrést alkalmazni

## Kezelt adatok

A rendszer a feladatokat kezeli.

Egy feladat várhatóan az alábbi adatokat tartalmazza:

- cím
- leírás
- állapot
- prioritás
- határidő
- létrehozás ideje
- módosítás ideje

## Fő funkciók

### Feladatok listázása

A felhasználó megtekintheti az összes rögzített feladatot egy listanézetben.

A lista célja, hogy gyors áttekintést adjon a rendszerben szereplő elemekről.

### Új feladat létrehozása

A felhasználó új feladatot hozhat létre a megfelelő űrlap segítségével.

A létrehozás során megadhatja a feladat legfontosabb adatait.

### Feladat módosítása

A rendszer lehetőséget biztosít egy meglévő feladat adatainak módosítására.

Ez különösen hasznos például akkor, ha változik a határidő, a prioritás vagy a feladat állapota.

### Feladat törlése

A felhasználó törölheti a már nem szükséges feladatokat.

### Állapot módosítása

A feladatok állapota módosítható.

Tervezett állapotok például:

- `Todo`
- `InProgress`
- `Done`

### Keresés és szűrés

A rendszer lehetőséget biztosít egyszerű keresésre és szűrésre.

Példák:

- keresés cím alapján
- szűrés állapot szerint

## MCP komponens szerepe

A rendszer része egy külön MCP komponens is.

Ennek szerepe, hogy a feladatkezelő domain bizonyos műveletei MCP toolokon keresztül is elérhetők legyenek.

Tervezett MCP műveletek:

- feladatok lekérdezése
- keresés
- új feladat létrehozása
- feladat státuszának módosítása
- feladat törlése

Ez a komponens elsősorban technológiai és architekturális célból része a projektnek.

## Jelenlegi állapot

Ez a dokumentum jelenleg az első, előkészítő verzió. A rendszer elkészülésével a felhasználói műveletek pontos menete, a felületi elemek és a használati példák is részletesen dokumentálásra kerülnek.
