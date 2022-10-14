# EduCar
Case final da capacitação em .Net ministrado pela Edusync em conjunto com a BRQ

## Objetivo
### Construção de uma API de uma concessionária que será responsável por fornecer dados para uma aplicação. 
#####    Essa aplicação poderá ser uma página WEB ou uma aplicação Mobile

### O que foi utilizado?
 - Linguagem C#
 - Asp.Net Core Web API versão 5.0
 - Azure Cloud
    - Azure SQL
    - CI - CD
    - Publicação da aplicação da API
    - Troca de senha com envio de Token por e-mail utilizando o Azure Communication Service
 - Criptografia de senhas com a biblioteca BCrypt
 - Autenticação com a biblioteca JWTBearer da Microsoft
 - Conceito de inversão de controle com injeção de dependência do repositório das models e do contexto
 - Repository Pattern com conceito de Generics para utilizar um Base Repository
 - ORM Entity Framework para acesso aos dados e persistência dos dados em um banco de dados
 - Conceito de criação do banco de dados: Code First
 - Criação de migration para atualização do banco de dados
 - SQL Server 
    - Inserção de dados com Script DML incluído no projeto na pasta Scripts
 - Entity Framework versão 5.0.17
    - CRUD básico utilizando o Base Repository com Insert, GetAll, GetById, Update, Patch e Delete utilizando o conceito de Generics
    - Consultas personalizadas utilizando relacionamentos entre as tabelas
 - Documentação via Swagger
 
 ### Configurações necessárias
 - No arquivo  appsettings.json substituir a string de conexão pela string de seu Banco de Dados SQL Server, caso queira utilizar o "SqlServer".
 - Caso queira utilizar a integração com a Azure, utilize a ConnectionString "Azure"

```
  "ConnectionStrings": {
    "SqlServer": "Server=.\\SQLEXPRESS;Database=EduCarDB;Trusted_Connection=true;",
    "Azure": "server=educardb.database.windows.net; Database = DesafioArquiteturaDB; User Id=educar; Password=Admin1234;"
  }
```
 - Caso utilize a aplicação em conjunto com o Azure, substituir na classe Startup a ConnectionString.
```
    options.UseSqlServer(Configuration.GetConnectionString("SqlServer")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
    
    options.UseSqlServer(Configuration.GetConnectionString("Azure")).UseQueryTrackingBehavior(QueryTrackingBehavior.NoTracking));
```
 - Caso queira utilizar o Sql Server localmente, execute o Update da Migration no banco de dados.
    
    - Executar o comando: 
    ```
      dotnet ef database update
    ```
    - Executar o DML que está na pasta Scripts no projeto.
    - Esse script foi desenvolvido para facilitar os testes dos da API sem a necessidade de persistir dados no banco através dos métodos POST

 - Necessário gerar o Token no Post de Login e utilizar esse token no botão "Authorize" no Swagger    
