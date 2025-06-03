# FIAP Cloud Games - Fase 1

Projeto desenvolvido para o Tech Challenge da FIAP - Fase 1.

## 📚 Descrição

A FIAP Cloud Games (FCG) é uma plataforma de venda de jogos digitais com funcionalidades futuras e gerenciamento de servidores. Esta fase contempla a construção da API inicial para cadastro de usuários, autenticação e gerenciamento da biblioteca de jogos.

## 🎯 Funcionalidades Implementadas

- Cadastro de usuários com validação de e-mail e senha segura
- Autenticação via JWT com suporte a roles (Usuário e Administrador)
- Cadastro e listagem de jogos (admin)
- Middleware de tratamento de erros estruturado
- Documentação da API via Swagger
- Testes unitários com TDD aplicados ao módulo de validação de senha
- Testes de integração com banco de dados real via container

## 🛠️ Tecnologias Utilizadas

- .NET 8
- ASP.NET Core MVC
- Entity Framework Core
- SQL Server
- JWT (Json Web Token)
- FluentValidation
- xUnit
- Testcontainers for .NET (banco SQL Server em container para testes)
- Docker (opcional para banco de dados)
- Swagger

## 🧪 Testes

O projeto inclui testes automatizados com foco em qualidade e comportamento real da aplicação.

- ✅ Testes de unidade com `xUnit` (senha, validadores, lógica de autenticação)
- ✅ TDD aplicado ao módulo de segurança (registro de usuários e autenticação)
- ✅ Testes de integração utilizando **Testcontainers for .NET**, garantindo isolamento e criação de banco de dados SQL Server em tempo real para cenários de teste.

## 🚀 Como Executar

1. Configure o arquivo `.env` com os dados do banco e chave JWT
   ```bash
   # String de conexão com o banco de dados SQL Server
   ConnectionStrings__DefaultConnection="Server=localhost;Database={DB_NAME};User Id={DB_USER};Password='{DB_PASSWORD}';TrustServerCertificate=true;"
   
   # Configurações do JWT
   JwtSettings__Secret="{JWT_SECRET_KEY}"
   JwtSettings__Issuer="{JWT_ISSUER}"
   JwtSettings__Audience="{JWT_AUDIENCE}"
   JwtSettings__TokenExpirationInMinutes=60
   JwtSettings__TokenTimeToleranceInMinutes=1
   ```
2. Aplique as migrations:
   ```bash
   dotnet ef database update
   ```
3. Execute o projeto:
   ```bash
   dotnet run --project TechChallengeFIAP
   ```
4. Acesse o Swagger em:
   ```
   http://localhost:5136/swagger/index.html
   ```

## 📂 Estrutura do Projeto

- `TechChallengeFIAP`: API principal (Controllers, Program.cs)
- `TechChallengeFIAP.Application`: Serviços, validações e settings
- `TechChallengeFIAP.Domain`: Entidades e interfaces
- `TechChallengeFIAP.Data`: Repositórios, contextos e migrations
- `TechChallengeFIAP.Tests.Unit`: Testes de unidade com xUnit
- `TechChallengeFIAP.Tests.Integration`: Testes de integração com Testcontainers

## 👨‍💻 Discord dos Autores

- Armando José Vieira Dias de Oliveira (id: @armandojoseoliveira - User: Nando) - RM361112
- Marlon dos Santos Limeira (id: @marlonsantos4509 - User: Marlon Santos RM361866) - RM361866
- Matheus de Moraes Rodrigues (id: @.marmotinhas - User: MatheusMR) - RM362205
- Matheus Nascimento Costa (id: @matheus_coast - User: Matheus_coast) - RM363404
- Ricardo Noronha de Menezes (id: @ricardo_nm - User: ricardo_nm) - RM363183


## 🎥 Demonstração

O vídeo demonstrando o funcionamento da aplicação pode ser acessado em: **[VIDEO - YOUTUBE](https://youtu.be/a1n6iSAu_o0)**

## 📎 Documentação DDD

- Mapeamento de fluxos via Event Storming disponível em: **[DDD - MIRO](https://miro.com/app/board/uXjVIE9R-Pg=/?share_link_id=308339772603)**
