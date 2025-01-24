# api-rest-full

## Descrição

Este projeto é uma API RESTful para acessar e gerenciar registros públicos e diários oficiais.

## Estrutura do Projeto

- `.github/`: Contém workflows do GitHub Actions.
- `config/`: Contém arquivos de configuração.
- `DiarioOficial.API/`: Contém a aplicação principal da API.
  - `Endpoints/`: Contém os endpoints da API.
  - `Middlewares/`: Contém middlewares personalizados.
  - `Program.cs`: Arquivo principal para configuração e inicialização da aplicação.
- `DiarioOficial.Application/`: Contém a lógica de aplicação.
- `DiarioOficial.CrossCutting/`: Contém classes e enums que são compartilhados entre diferentes camadas.
- `DiarioOficial.Domain/`: Contém as interfaces e entidades de domínio.
- `DiarioOficial.Infraestructure/`: Contém a implementação da infraestrutura, como acesso a dados e serviços externos.
- `DiarioOficial.Tests/`: Contém os testes unitários e de integração.

## Configuração

### Variáveis de Ambiente

Certifique-se de configurar as seguintes variáveis de ambiente no arquivo `config/env.txt`:

- `Jwt:Issuer`
- `Jwt:Audience`
- `Jwt:Key`
- `ConnectionStrings:OfficialDiaryDb`

### Dependências

- .NET 6.0
- RestSharp
- Microsoft.AspNetCore.Authentication.JwtBearer
- Microsoft.EntityFrameworkCore

## Executando a Aplicação

1. Clone o repositório:
   ```sh
   git clone https://github.com/Ton-Chyod-s/api-rest-full.git```

2. Navegue até o diretório do projeto:
   
    ```cd api-rest-full/DiarioOficial.API```

3. Restaure as dependências:
   
    ```dotnet restore```

4. Execute a aplicação:
   
    ```dotnet run```

### Endpoints

### Autenticação

```POST /login:``` Cria ou atualiza um login.

### Diário Oficial

```GET /diary/official-electronic-diary:``` Obtém registros do diário oficial eletrônico.

### Pessoa

```POST /person:``` Adiciona ou atualiza uma pessoa.

### Email

```POST /mail/send-email:``` Envia um email.

### Contribuição

1. Faça um fork do projeto.
2. Crie uma nova branch (git checkout -b feature/nova-feature).
3. Commit suas mudanças (git commit -am 'Adiciona nova feature').
4. Faça um push para a branch (git push origin feature/nova-feature).
5. Abra um Pull Request.

#### Licença

Este projeto está licenciado sob a licença MIT. Veja o arquivo LICENSE para mais detalhes.

