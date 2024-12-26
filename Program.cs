using ErpCalciolari.Infra;
using ErpCalciolari.Repositories;
using ErpCalciolari.Services;
using ErpCalciolari.Middlewares;
using Microsoft.EntityFrameworkCore;
using FluentValidation.AspNetCore;
using FluentValidation;
using ErpCalciolari.DTOs.Create.Validators;
using ErpCalciolari.DTOs.Update.Validators;
using ErpCalciolari.Repositories.Interfaces;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddFluentValidationAutoValidation().AddFluentValidationClientsideAdapters();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<EmployeeUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<ProductUpdateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerCreateDtoValidator>();
builder.Services.AddValidatorsFromAssemblyContaining<CustomerUpdateDtoValidator>();

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configure PostgreSQL connection
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<MyDbContext>(options =>
    options.UseNpgsql(connectionString));

// Register all services and repositories
builder.Services.AddScoped<IEmployeeRepository, EmployeeRepository>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<IProductRepository, ProductRepository>();
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ICustomerRepository, CustomerRepository>();
builder.Services.AddScoped<CustomerService>();
builder.Services.AddScoped<IOrderRepository, OrderRepository>();
builder.Services.AddScoped<OrderService>();
builder.Services.AddScoped<IProductionRequirementRepository, ProductionRequirementRepository>();

// Add CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll", builder =>
    {
        builder.AllowAnyOrigin()
            .AllowAnyMethod()
            .AllowAnyHeader();
    });
});

// Debug logging
builder.Logging.AddConsole();

var app = builder.Build();

try
{
    using (var scope = app.Services.CreateScope())
    {
        var dbContext = scope.ServiceProvider.GetRequiredService<MyDbContext>();
        if (await dbContext.Database.CanConnectAsync())
        {
            Console.WriteLine(WelcomeMessage.GetMessage());
            Console.WriteLine();
            Console.WriteLine("Database connected. Program started!");
        }
    }
}
catch (Exception e)
{
    Console.WriteLine($"Failed to connect to the database: {e.Message}");
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionMiddleware>();

app.UseHttpsRedirection();

app.UseCors();

app.UseAuthorization();

app.MapControllers();

app.Run();
