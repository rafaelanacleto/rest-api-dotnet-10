# Stage 1: Build
FROM mcr.microsoft.com/dotnet/sdk:10.0 AS build
WORKDIR /src

# Copiar arquivos de projeto e restaurar dependências
COPY RestApi/RestApi.API/RestApi.API.csproj RestApi/RestApi.API/
RUN dotnet restore "RestApi/RestApi.API/RestApi.API.csproj"

# Copiar o restante dos arquivos e fazer build
COPY RestApi/RestApi.API/ RestApi/RestApi.API/
WORKDIR /src/RestApi/RestApi.API
RUN dotnet build "RestApi.API.csproj" -c Release -o /app/build

# Stage 2: Publish
FROM build AS publish
RUN dotnet publish "RestApi.API.csproj" -c Release -o /app/publish /p:UseAppHost=false

# Stage 3: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:10.0 AS runtime
WORKDIR /app

# Criar usuário não-root para segurança
RUN groupadd -r appuser && useradd -r -g appuser appuser

# Copiar arquivos publicados
COPY --from=publish /app/publish .

# Expor porta
EXPOSE 8080
EXPOSE 8081

# Configurar variáveis de ambiente
ENV ASPNETCORE_URLS=http://+:8080
ENV ASPNETCORE_ENVIRONMENT=Production

# Mudar para usuário não-root
USER appuser

# Entrada da aplicação
ENTRYPOINT ["dotnet", "RestApi.API.dll"]
