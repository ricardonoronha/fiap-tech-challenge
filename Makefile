# Caminhos dos projetos
DATA_PROJ=TechChallengeFIAP.Data
API_PROJ=TechChallengeFIAP

#Executa a API
run:
	dotnet run --project $(API_PROJ)

# Aplica as migrations no banco
update-db:
	dotnet tool run dotnet-ef database update --project $(DATA_PROJ) --startup-project $(API_PROJ)

# Cria uma migration (passar nome com: make add-migration NAME=MinhaMigration)
add-migration:
	dotnet tool run dotnet-ef migrations add $(NAME) --project $(DATA_PROJ) --startup-project $(API_PROJ)

# Lista migrations existentes
list-migrations:
	dotnet tool run dotnet-ef migrations list --project $(DATA_PROJ)

# Roda os testes
test:
	dotnet test

# Restaura ferramentas
tools-restore:
	dotnet tool restore
