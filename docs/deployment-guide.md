# Telepitesi utmutato

## Cel

Ez a dokumentum bemutatja, hogyan lehet a projektet helyi kornyezetben futtatni, Docker Compose-szal inditani, majd minikube klaszterre es ArgoCD-vel telepiteni.

## Elofeltetelek

A projekt futtatasahoz es telepitesehez az alabbi eszkozok szuksegesek:

- Git
- Docker Desktop
- Visual Studio Code
- Dev Containers bovitmeny
- minikube
- kubectl
- Helm

## Repository klonozasa

```bash
git clone https://github.com/attila-somogyvari/task-manager-alkfejl.git
cd task-manager-alkfejl
```

## Fejlesztoi kornyezet

A projekt Dev Container alapu fejlesztoi kornyezetet hasznal.

1. Nyisd meg a repositoryt Visual Studio Code-ban.
2. Valaszd a `Reopen in Container` lehetoseget.
3. A containerben elerheto lesz a .NET 10 SDK, Node.js, Angular CLI, kubectl es Helm.

Gyors ellenorzes:

```bash
dotnet --list-sdks
dotnet build ./TaskManager.sln
```

## Kontenerizacio

A rendszer fo komponensei kulon Docker image-kent keszulnek:

- `WebUI`
- `TaskService`
- `McpService`

Teljes stack lokalis inditasa:

```powershell
docker compose build
docker compose up
```

Elerheto vegpontok:

- `WebUI`: [http://localhost:4200](http://localhost:4200)
- `TaskService`: [http://localhost:5095/tasks](http://localhost:5095/tasks)
- `McpService`: [http://localhost:5044/mcp](http://localhost:5044/mcp)

## Kubernetes telepites

A `deployment/` mappa harom reszre van bontva:

- `deployment/local/` - helyi image-ekkel hasznalt manifestek
- `deployment/prod/` - registrybol huzott image-ekkel hasznalt manifestek
- `deployment/argocd/` - ArgoCD telepitesi fajlok

### MongoDB telepitese Helm charttal

A MongoDB kulon, a `taskmanager-infra` namespace-be kerul.

```powershell
helm install taskmanager-mongodb oci://registry-1.docker.io/cloudpirates/mongodb `
  --namespace taskmanager-infra `
  --create-namespace `
  --version 0.12.2 `
  --set auth.enabled=false `
  --set architecture=standalone `
  --set persistence.size=1Gi
```

Ellenorzes:

```powershell
kubectl get pods -n taskmanager-infra
```

### Local Kubernetes telepites

Inditsd el a minikube klasztert:

```powershell
minikube start
kubectl get nodes
```

Buildeld le a Docker image-eket:

```powershell
docker compose build
```

Toltsd be a helyi image-eket a minikube-ba:

```powershell
minikube image load task-manager-alkfejl-taskservice:latest
minikube image load task-manager-alkfejl-mcpservice:latest
minikube image load task-manager-alkfejl-webui:latest
```

Alkalmazd a local manifesteket:

```powershell
kubectl apply -f .\deployment\local\namespace.yaml
kubectl apply -f .\deployment\local\configmap.yaml
kubectl apply -f .\deployment\local\taskservice.yaml
kubectl apply -f .\deployment\local\mcpservice.yaml
kubectl apply -f .\deployment\local\webui.yaml
```

Ha korabbi hibas podok mar futnak, inditsd ujra a deploymenteket:

```powershell
kubectl rollout restart deployment/taskservice -n taskmanager-app
kubectl rollout restart deployment/mcpservice -n taskmanager-app
kubectl rollout restart deployment/webui -n taskmanager-app
```

Ellenorzes:

```powershell
kubectl get pods -n taskmanager-app
kubectl get pods -n taskmanager-infra
```

A frontend elerese:

```powershell
minikube service webui -n taskmanager-app --url
```

### Prod manifestek

A `deployment/prod/` mappa a GitHub Container Registry-bol huzott image-eket hasznalja:

- `ghcr.io/attila-somogyvari/task-manager-alkfejl/taskservice:latest`
- `ghcr.io/attila-somogyvari/task-manager-alkfejl/mcpservice:latest`
- `ghcr.io/attila-somogyvari/task-manager-alkfejl/webui:latest`

Ezeket a GitHub Actions workflow-k epitik es pusholjak a `main` branch-re torteno push eseten.

## ArgoCD

Az 5-os szintu kovetelmeny CD oldala ArgoCD-vel van kialakitva.

Az ArgoCD manifestek:

- `deployment/argocd/namespace.yaml`
- `deployment/argocd/project.yaml`
- `deployment/argocd/application.yaml`

### ArgoCD telepitese helyi klaszterre

```powershell
kubectl apply -f .\deployment\argocd\namespace.yaml
kubectl apply -n argocd -f https://raw.githubusercontent.com/argoproj/argo-cd/stable/manifests/install.yaml
```

Varj, amig az ArgoCD podok felallnak:

```powershell
kubectl get pods -n argocd
```

### Task Manager application letrehozasa

```powershell
kubectl apply -f .\deployment\argocd\project.yaml
kubectl apply -f .\deployment\argocd\application.yaml
```

Ellenorzes:

```powershell
kubectl get applications -n argocd
```

### ArgoCD UI elerese

```powershell
kubectl port-forward svc/argocd-server -n argocd 8081:443
```

Az ArgoCD UI ezutan itt erheto el:

- [https://localhost:8081](https://localhost:8081)

Az alap admin jelszo kiolvasasa:

```powershell
kubectl get secret argocd-initial-admin-secret -n argocd -o jsonpath="{.data.password}" | %{[System.Text.Encoding]::UTF8.GetString([System.Convert]::FromBase64String($_))}
```

## Ellenorzes

Sikeres telepites eseten az alabbiak ellenorizhetok:

- a `taskmanager-infra` namespace-ben a MongoDB pod `1/1 Running`
- a `taskmanager-app` namespace-ben a `taskservice`, `mcpservice` es `webui` podok `1/1 Running`
- a `WebUI` megnyithato
- a `TaskService` API valaszol
- uj task letrehozhato, statusza modosithato es torolheto
- ArgoCD-ben a `taskmanager-prod` application `Synced` es `Healthy` allapotba kerul
