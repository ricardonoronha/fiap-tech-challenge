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
   https://localhost:{porta}/swagger
   ```

## 📂 Estrutura do Projeto

- `TechChallengeFIAP`: API principal (Controllers, Program.cs)
- `TechChallengeFIAP.Application`: Serviços, validações e settings
- `TechChallengeFIAP.Domain`: Entidades e interfaces
- `TechChallengeFIAP.Data`: Repositórios, contextos e migrations
- `TechChallengeFIAP.Tests.Unit`: Testes de unidade com xUnit
- `TechChallengeFIAP.Tests.Integration`: Testes de integração com Testcontainers

## 👨‍💻 Discord dos Autores

- Armando José Vieira Dias de Oliveira
- Marlon dos Santos Limeira (@Marlon Santos RM361866)
- Matheus de Moraes Rodrigues
- Matheus Nascimento Costa
- Ricardo Noronha de Menezes (@ricardo_nm RM363183)



## 🎥 Demonstração

O vídeo demonstrando o funcionamento da aplicação pode ser acessado em: [link_do_video]

## 📎 Documentação DDD

- Mapeamento de fluxos via Event Storming disponível em: [link_do_miro]
