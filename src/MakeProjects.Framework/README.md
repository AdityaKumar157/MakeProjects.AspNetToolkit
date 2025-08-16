# MakeProjects.AspNetToolkit.Framework
Repository + Service abstractions and EF Core infrastructure.

## Installation
dotnet add package MakeProjects.AspNetToolkit.Framework

## Features
- Repository and Service abstractions
- Repository Pattern
- Unit of Work (UoW) Pattern
- CRUD Service abstractions and implementations
- EF Core integration
- Dependency Injection (DI) support

## Getting Started
1. Install the package:
   ```bash
   dotnet add package MakeProjects.AspNetToolkit.Framework
   ```
2. Configure your DbContext and services in your application:
   ```csharp
   public class MyDbContext : DbContext
   {
	   public MyDbContext(DbContextOptions<MyDbContext> options) : base(options) { }
	   // DbSets...
   }
   public void ConfigureServices(IServiceCollection services)
   {
	   services.AddDbContext<MyDbContext>(options => 
		   options.UseSqlServer("YourConnectionString"));
	   services.AddScoped<IRepository<MyEntity>, Repository<MyEntity>>();
	   services.AddScoped<IMyService, MyService>();

	   // Or use the extension method:
	   builder.Services.AddMakeProjectsInfrastructure();
   }
   ```
3. Use the repository and service in your application:
   ```csharp
   public class MyController : ControllerBase
   {
	   private readonly IMyService _myService;
	   public MyController(IMyService myService)
	   {
		   _myService = myService;
	   }
	   [HttpGet]
	   public async Task<IActionResult> Get()
	   {
		   var items = await _myService.GetAllAsync();
		   return Ok(items);
	   }
   }
   ```
4. Create your entity classes and define the DbSet properties in your DbContext:
   ```csharp
   public class MyEntity
   {
	   public int Id { get; set; }
	   public string Name { get; set; }
	   // Other properties...
   }
   ```
5. Create custom repository and service interfaces and implementations as needed:
   ```csharp
   public interface IEmployeeRepository : IRepository<Employee, Guid>
   {
       Task<IEnumerable<Employee>> GetEmployeesByDepartmentAsync(Guid departmentId);
   }
   ```
   ```csharp
   public interface IMyService : ICrudService<Entity, Guid>
   {
	   // Custom methods...
   }
   public class MyService : BaseCrudService<Entity, Guid>, IMyService
   {
	   public MyService(MyDbContext applicationDbContext, IEmployeeRepository employeeRepository, ILogger<EmployeeService> logger) : base(employeeRepository, logger) { }
	   // Implement custom methods...
   }
   ```
5. Run your application and start using the repository and service abstractions.