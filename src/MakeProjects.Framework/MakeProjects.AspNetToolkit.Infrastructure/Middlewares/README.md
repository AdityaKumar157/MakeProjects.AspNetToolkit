# MakeProjects.AspNetToolkit.Infrastructure.Middlewares
Middlewares for ASP.NET Core applications that provide request handling capabilities, including logging and error handling.

## Exception and Status Code Mapping
This package includes a BaseRequestHandlingMiddleware that maps exceptions to HTTP status codes, allowing for consistent error handling across 
the application. It captures exceptions thrown during request processing and returns appropriate HTTP responses.
```csharp
case ArgumentNullException:
                case BadRequestException:
                    {
                        statusCode = HttpStatusCode.BadRequest;
                        break;
                    }
                case KeyNotFoundException:
                case NotFoundException:
                    {
                        statusCode = HttpStatusCode.NotFound;
                        break;
                    }
                case UnauthorizedAccessException:
                    statusCode = HttpStatusCode.Unauthorized;
                    break;
                case ConflictException:
                    statusCode = HttpStatusCode.Conflict;
                    break;
                default:
                    statusCode = HttpStatusCode.InternalServerError;
                    message = "An unexpected error occurred.";
                    break;
```