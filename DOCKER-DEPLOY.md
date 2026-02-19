# Guia de Docker e Deploy - REST API .NET 10

## 📋 Índice
- [Pré-requisitos](#pré-requisitos)
- [Build Local](#build-local)
- [Docker Compose](#docker-compose)
- [Azure DevOps Pipeline](#azure-devops-pipeline)
- [Deploy em Produção](#deploy-em-produção)

---

## 🔧 Pré-requisitos

- Docker e Docker Compose instalados
- .NET 10 SDK (para desenvolvimento local)
- Conta Azure com Container Registry (para pipeline)
- Azure DevOps configurado

---

## 🐳 Build Local da Imagem Docker

### Build da imagem
```bash
docker build -t restapi:latest .
```

### Build com tag específica
```bash
docker build -t restapi:1.0.0 -t restapi:latest .
```

### Executar o container localmente
```bash
docker run -d -p 5000:8080 \
  -e ConnectionStrings__DefaultConnection="Server=localhost,1433;Database=RestApiDb;User Id=sa;Password=Rafael.2024;TrustServerCertificate=True;" \
  --name restapi-container \
  restapi:latest
```

### Verificar logs
```bash
docker logs -f restapi-container
```

---

## 🚀 Docker Compose

O `docker-compose.yml` está configurado para executar toda a stack (API + SQL Server).

### Subir toda a aplicação
```bash
docker-compose up -d
```

### Verificar status dos containers
```bash
docker-compose ps
```

### Ver logs
```bash
# Logs de todos os serviços
docker-compose logs -f

# Logs apenas da API
docker-compose logs -f api

# Logs apenas do SQL Server
docker-compose logs -f sqlserver
```

### Reconstruir e subir
```bash
docker-compose up -d --build
```

### Parar e remover containers
```bash
docker-compose down
```

### Parar e remover containers + volumes (apaga dados do banco)
```bash
docker-compose down -v
```

### Acessar a aplicação
Após subir com docker-compose:
- API: http://localhost:5000
- Swagger: http://localhost:5000/swagger
- SQL Server: localhost:1433

---

## ☁️ Azure DevOps Pipeline

### Configuração Inicial

#### 1. Criar Azure Container Registry (ACR)
```bash
# Via Azure CLI
az acr create \
  --resource-group seu-resource-group \
  --name seuregistro \
  --sku Basic
```

#### 2. Configurar Service Connection no Azure DevOps

1. Acesse seu projeto no Azure DevOps
2. Vá em **Project Settings** > **Service connections**
3. Clique em **New service connection**
4. Selecione **Docker Registry**
5. Escolha **Azure Container Registry**
6. Configure:
   - Registry type: Azure Container Registry
   - Subscription: Sua subscription
   - Registry: Seu ACR criado
   - Service connection name: `your-acr-service-connection`

#### 3. Atualizar variáveis no azure-pipelines.yml

Edite o arquivo `azure-pipelines.yml` e altere:
```yaml
variables:
  dockerRegistryServiceConnection: 'your-acr-service-connection' # Nome da Service Connection criada
  imageRepository: 'restapi'
  containerRegistry: 'seuregistro.azurecr.io' # Seu ACR
```

#### 4. Criar a Pipeline

1. No Azure DevOps, vá em **Pipelines** > **Create Pipeline**
2. Selecione **Azure Repos Git** (ou GitHub se estiver lá)
3. Selecione seu repositório
4. Escolha **Existing Azure Pipelines YAML file**
5. Selecione o arquivo `azure-pipelines.yml`
6. Clique em **Run**

### Pipeline Stages

A pipeline possui 2 stages principais:

#### Stage 1: Build and Test
- Instala .NET 10 SDK
- Restaura pacotes NuGet
- Compila a aplicação
- Executa testes (se descomentado)

#### Stage 2: Build and Push Docker Image
- Faz login no ACR
- Constrói a imagem Docker
- Adiciona tags (BuildId e latest)
- Faz push da imagem para o ACR
- Faz logout do ACR

#### Stage 3: Deploy (Opcional)
Descomente no arquivo `azure-pipelines.yml` para habilitar deploy automático.

### Triggers

A pipeline executa automaticamente quando:
- Há push para branches `main` ou `develop`
- Há alteração nos arquivos da pasta `RestApi/`, `Dockerfile` ou `docker-compose.yml`
- É criado um Pull Request para `main` ou `develop`

---

## 🌐 Deploy em Produção

### Opção 1: Azure App Service

```bash
# Criar App Service Plan
az appservice plan create \
  --name restapi-plan \
  --resource-group seu-resource-group \
  --is-linux \
  --sku B1

# Criar Web App
az webapp create \
  --resource-group seu-resource-group \
  --plan restapi-plan \
  --name restapi-app \
  --deployment-container-image-name seuregistro.azurecr.io/restapi:latest

# Configurar variáveis de ambiente
az webapp config appsettings set \
  --resource-group seu-resource-group \
  --name restapi-app \
  --settings \
    ASPNETCORE_ENVIRONMENT=Production \
    ConnectionStrings__DefaultConnection="Server=seu-sql-server.database.windows.net;Database=RestApiDb;User Id=adminuser;Password=SuaSenha123!;TrustServerCertificate=True;"
```

### Opção 2: Azure Container Instances (ACI)

```bash
az container create \
  --resource-group seu-resource-group \
  --name restapi-container \
  --image seuregistro.azurecr.io/restapi:latest \
  --registry-login-server seuregistro.azurecr.io \
  --registry-username seuregistro \
  --registry-password $(az acr credential show --name seuregistro --query "passwords[0].value" -o tsv) \
  --dns-name-label restapi-unique \
  --ports 8080 \
  --environment-variables \
    ASPNETCORE_ENVIRONMENT=Production \
    'ConnectionStrings__DefaultConnection'='Server=seu-sql-server.database.windows.net;Database=RestApiDb;User Id=adminuser;Password=SuaSenha123!;TrustServerCertificate=True;'
```

### Opção 3: Azure Kubernetes Service (AKS)

Descomente a seção de deploy do AKS no `azure-pipelines.yml` e crie os manifestos Kubernetes.

---

## 🔐 Variáveis de Ambiente

### Desenvolvimento (docker-compose)
Já configuradas no `docker-compose.yml`:
```yaml
- ASPNETCORE_ENVIRONMENT=Production
- ConnectionStrings__DefaultConnection=Server=sqlserver,1433;Database=RestApiDb;...
```

### Produção (Azure)
Configure via:
- Azure Portal > App Service > Configuration
- Azure CLI (exemplos acima)
- Azure DevOps > Pipelines > Variables

### Variáveis Importantes
- `ASPNETCORE_ENVIRONMENT`: Development, Staging, Production
- `ASPNETCORE_URLS`: http://+:8080
- `ConnectionStrings__DefaultConnection`: String de conexão do banco

---

## 📊 Monitoramento e Logs

### Visualizar logs no Azure
```bash
# App Service
az webapp log tail \
  --name restapi-app \
  --resource-group seu-resource-group

# Container Instances
az container logs \
  --resource-group seu-resource-group \
  --name restapi-container
```

### Application Insights (Recomendado)
Adicione ao `appsettings.json`:
```json
{
  "ApplicationInsights": {
    "InstrumentationKey": "sua-chave"
  }
}
```

E instale o pacote:
```bash
dotnet add package Microsoft.ApplicationInsights.AspNetCore
```

---

## 🛠️ Troubleshooting

### Problema: Container não inicia
```bash
# Verificar logs
docker logs restapi-container

# Verificar saúde do SQL Server
docker exec restapi-sqlserver /opt/mssql-tools18/bin/sqlcmd -S localhost -U sa -P "Rafael.2024" -Q "SELECT 1" -C
```

### Problema: Erro de conexão com banco
- Verifique se o SQL Server está rodando
- Confirme a connection string
- Verifique se a network está correta (docker-compose)

### Problema: Pipeline falha
- Verifique se a Service Connection está configurada
- Confirme se o ACR existe e está acessível
- Verifique os logs da pipeline no Azure DevOps

---

## 📝 Comandos Úteis

```bash
# Listar imagens
docker images | grep restapi

# Remover imagens antigas
docker image prune -a

# Inspecionar imagem
docker inspect restapi:latest

# Executar comandos dentro do container
docker exec -it restapi-container bash

# Push manual para ACR
docker tag restapi:latest seuregistro.azurecr.io/restapi:1.0.0
docker push seuregistro.azurecr.io/restapi:1.0.0
```

---

## 📚 Referências

- [Docker Documentation](https://docs.docker.com/)
- [.NET Docker Images](https://hub.docker.com/_/microsoft-dotnet)
- [Azure DevOps Pipelines](https://docs.microsoft.com/azure/devops/pipelines/)
- [Azure Container Registry](https://docs.microsoft.com/azure/container-registry/)

---

**Autor**: Gerado para projeto REST API .NET 10  
**Data**: Fevereiro 2026
