
# GithubRepoSearch API (.NET 8)

Backend REST API for searching GitHub repositories and managing bookmarks, built with .NET 8

## Features

- **Search GitHub Repositories:** Query the public GitHub API for repositories by keyword.
- **Bookmark Management:** Authenticated users can bookmark repositories. Bookmarks are stored in-memory per user session (custom session implementation).
- **JWT Authentication:** Secure endpoints with JSON Web Tokens. Demo login accepts any username/password that meet validation requirements.
- **Validation:** Uses FluentValidation for request and model validation.
- **AutoMapper:** Maps GitHub API models to DTOs for clean API responses.
- **Swagger/OpenAPI:** Interactive API documentation (Swagger UI) available in development mode for exploring and testing endpoints.

## Requirements

- .NET 8

## Getting Started

To start the API locally:

1. **Run with IIS Express**
   - Open the solution in Visual Studio.
   - Select `IIS Express` as the run profile.
   - Press F5 or click the Run button.
   - The API will be available at the IIS Express URL shown in the browser (`https://localhost:44384`).

4. **API Endpoints**

   | Method | Endpoint                  | Description                                 | Auth Required |
   |--------|--------------------------|---------------------------------------------|--------------|
   | POST   | `/api/login`              | Get JWT token (demo login)                  | No           |
   | GET    | `/api/repositories`       | Search repositories (`?query=KEYWORD`)      | Yes          |
   | GET    | `/api/bookmarks`          | List all bookmarks for user                 | Yes          |
   | POST   | `/api/bookmarks`          | Add a repository to bookmarks               | Yes          |

   - All endpoints (except `/api/login`) require a valid JWT in the `Authorization: Bearer <token>` header.

## Notes

- This is a backend-only project. No frontend is included.
- For demo, authentication is mocked.
- Bookmarks are stored in-memory and will be lost on server restart.

