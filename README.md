# rest-api-dotnet-10
Estudo inicial dotnet core 10

## Banco SQL Server com Docker Compose

### Pré-requisitos
- Docker Desktop em execução.

### Subir somente o banco
Na raiz do projeto, execute:

```powershell
docker compose up -d sqlserver
```

Ou use o script:

```powershell
.\scripts\up-db.ps1
```

### Verificar se o container está rodando
```powershell
docker compose ps
```

### Parar o banco
```powershell
docker compose stop sqlserver
```

### Remover container (mantendo volume)
```powershell
docker compose down
```
