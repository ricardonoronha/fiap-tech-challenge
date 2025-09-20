# Etapa 1: Build
FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
WORKDIR /src

# Copia tudo
COPY . .

# Restaura dependências
RUN dotnet restore

# Publica o projeto
RUN dotnet publish -c Release -o /app/publish

# Etapa 2: Runtime
FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS runtime
WORKDIR /app

# Copia os arquivos publicados para a imagem final
COPY --from=build /app/publish .

# Expõe a porta
EXPOSE 8080

# Define o ENTRYPOINT para rodar a aplicação
ENTRYPOINT ["dotnet", "UsuariosService.dll"]
