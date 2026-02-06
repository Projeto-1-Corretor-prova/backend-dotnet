# Autenticação JWT - Documentação

## Visão Geral

Este projeto implementa autenticação JWT (JSON Web Token) para o backend .NET, usando a classe `Teacher` (Professor) como modelo de usuário.

## Componentes Implementados

### 1. Serviços

#### **IJwtService / JwtService**
- **Localização**: `services/JwtService.cs`
- **Responsabilidades**:
  - `GenerateToken(Teacher teacher)`: Gera um token JWT para um professor autenticado
  - `ValidateToken(string token)`: Valida um token JWT e retorna o ID do professor

#### **IAuthService / AuthService**
- **Localização**: `services/AuthService.cs`
- **Responsabilidades**:
  - `HashPassword(string password)`: Cria hash SHA256 de uma senha
  - `VerifyPassword(string password, string hashedPassword)`: Verifica se a senha corresponde ao hash

### 2. Middleware

#### **JwtMiddleware**
- **Localização**: `middleware/JwtMiddleware.cs`
- **Função**: Intercepta todas as requisições HTTP, valida o token JWT no header `Authorization` e anexa o ID do professor ao contexto da requisição (`HttpContext.Items["TeacherId"]`)

### 3. Atributo de Autorização

#### **AuthorizeAttribute**
- **Localização**: `attributes/AuthorizeAttribute.cs`
- **Uso**: Aplica-se a controllers ou métodos para proteger endpoints que requerem autenticação
- **Exemplo**:
```csharp
[backend.attributes.Authorize]
[HttpGet("profile")]
public async Task<IActionResult> GetProfile()
{
    var teacherId = (int?)HttpContext.Items["TeacherId"];
    // ... lógica do endpoint
}
```

### 4. Controller de Autenticação

#### **TeacherController**
- **Endpoints implementados**:

1. **POST /api/teacher/login**
   - Autentica um professor e retorna um token JWT
   - **Body**:
   ```json
   {
     "email": "professor@example.com",
     "password": "senha123"
   }
   ```
   - **Resposta de sucesso**:
   ```json
   {
     "token": "eyJhbGciOiJIUzI1NiIsInR5cCI6IkpXVCJ9...",
     "teacher": {
       "id": 1,
       "name": "João Silva",
       "email": "professor@example.com"
     }
   }
   ```

2. **GET /api/teacher/profile**
   - Retorna o perfil do professor autenticado (requer autenticação)
   - **Header**: `Authorization: Bearer {token}`
   - **Resposta de sucesso**:
   ```json
   {
     "id": 1,
     "name": "João Silva",
     "email": "professor@example.com"
   }
   ```

## Configuração

### appsettings.json

```json
{
  "JwtSettings": {
    "SecretKey": "your-super-secret-key-with-at-least-32-characters-for-security",
    "Issuer": "backend-api",
    "Audience": "backend-client",
    "ExpirationMinutes": "60"
  }
}
```

**⚠️ IMPORTANTE**: Altere a `SecretKey` para uma chave secreta forte em produção!

## Como Usar

### 1. Login
```bash
curl -X POST http://localhost:5000/api/teacher/login \
  -H "Content-Type: application/json" \
  -d '{
    "email": "professor@example.com",
    "password": "senha123"
  }'
```

### 2. Acessar Endpoint Protegido
```bash
curl -X GET http://localhost:5000/api/teacher/profile \
  -H "Authorization: Bearer SEU_TOKEN_AQUI"
```

## Protegendo Endpoints

Para proteger qualquer endpoint, adicione o atributo `[backend.attributes.Authorize]`:

```csharp
[ApiController]
[Route("api/[controller]")]
public class MinhaController : ControllerBase
{
    [HttpGet]
    [backend.attributes.Authorize]
    public IActionResult EndpointProtegido()
    {
        // Obter o ID do professor autenticado
        var teacherId = (int?)HttpContext.Items["TeacherId"];
        
        // Sua lógica aqui
        return Ok();
    }
}
```

## Fluxo de Autenticação

1. **Login**: Cliente envia email e senha para `/api/teacher/login`
2. **Validação**: Sistema verifica credenciais no banco de dados
3. **Token**: Se válido, retorna um token JWT
4. **Uso**: Cliente inclui token no header `Authorization: Bearer {token}` em requisições subsequentes
5. **Validação**: Middleware valida o token e anexa o ID do professor ao contexto
6. **Acesso**: Endpoints protegidos verificam a presença do ID no contexto

## Segurança

- Senhas são armazenadas como hash SHA256
- Tokens JWT expiram após o tempo configurado (padrão: 60 minutos)
- Tokens são validados em cada requisição
- Endpoints protegidos retornam 401 Unauthorized se o token for inválido

## Dependências

- `Microsoft.AspNetCore.Authentication.JwtBearer` (10.0.2)
- `System.IdentityModel.Tokens.Jwt` (8.15.0)
