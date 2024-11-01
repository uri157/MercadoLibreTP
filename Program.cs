using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

try
{
    // Configuración de CORS para permitir todos los orígenes temporalmente (solo para desarrollo)
    builder.Services.AddCors(options =>
    {
        options.AddPolicy("AllowAll", policy =>
        {
            policy.AllowAnyOrigin()    // Permitir todos los orígenes (temporalmente para desarrollo)
                  .AllowAnyMethod()    // Permitir todos los métodos HTTP
                  .AllowAnyHeader();   // Permitir todos los encabezados
        });
    });

    // Agregamos los controladores
    builder.Services.AddControllers();

    // Swagger para la documentación de la API
    builder.Services.AddEndpointsApiExplorer();
    builder.Services.AddSwaggerGen(c =>
    {
        c.SwaggerDoc("v1", new OpenApiInfo { Title = "MercadoLibre API", Version = "v1" });

        // Configuración de seguridad para JWT
        c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
        {
            Name = "Authorization",
            Type = SecuritySchemeType.Http,
            Scheme = "Bearer",
            BearerFormat = "JWT",
            In = ParameterLocation.Header,
            Description = "Introduce el token JWT en este formato: Bearer {token}"
        });

        c.AddSecurityRequirement(new OpenApiSecurityRequirement
        {
            {
                new OpenApiSecurityScheme
                {
                    Reference = new OpenApiReference
                    {
                        Type = ReferenceType.SecurityScheme,
                        Id = "Bearer"
                    }
                },
                new string[] {}
            }
        });
    });

    // Configuración del contexto de base de datos
    builder.Services.AddSqlServer<DbContext>(builder.Configuration.GetConnectionString("cnMercadoLibre"));

    // Inyección de dependencias para los servicios
    builder.Services.AddScoped<ICardService, CardDbService>();
    builder.Services.AddScoped<ICardTypeService, CardTypeDbService>();
    builder.Services.AddScoped<ICategoryService, CategoryDbService>();
    builder.Services.AddScoped<INotificationService, NotificationDbService>();
    builder.Services.AddScoped<IProductStateService, ProductStateDbService>();
    builder.Services.AddScoped<IPublicationService, PublicationDbService>();
    builder.Services.AddScoped<IPublicationStateService, PublicationStateDbService>();
    builder.Services.AddScoped<IPhotoService, PhotoDbService>();
    builder.Services.AddScoped<IColorService, ColorDbService>();
    builder.Services.AddScoped<IAccountService, AccountDbService>();
    builder.Services.AddScoped<IPhotoPublicationService, PhotoPublicationDbService>();
    builder.Services.AddScoped<ITransactionService, TransactionDbService>();
    builder.Services.AddScoped<IShoppingCartService, ShoppingCartDbService>();
    builder.Services.AddScoped<IPublicationVisitedService, PublicationVisitedDbService>();

    // Configuración del contexto de Identity (autenticación y autorización)
    builder.Services.AddDbContext<DbContext>(options =>
        options.UseSqlServer(builder.Configuration.GetConnectionString("cnMercadoLibre")));

    // Configurar Identity
    builder.Services.AddIdentity<User, IdentityRole<int>>()
        .AddEntityFrameworkStores<DbContext>()
        .AddDefaultTokenProviders();

    // Configurar JWT para autenticación
    builder.Services.AddAuthentication(options =>
    {
        options.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
        options.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
    })
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            ValidIssuer = builder.Configuration["Jwt:Issuer"],
            ValidAudience = builder.Configuration["Jwt:Audience"],
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"]))
        };
    });

    var app = builder.Build();

    // Configuración del pipeline de solicitud HTTP
    if (app.Environment.IsDevelopment())
    {
        app.UseSwagger();
        app.UseSwaggerUI();
    }

    // Aplica la política de CORS configurada anteriormente (AllowAll para desarrollo)
    app.UseCors("AllowAll");

    app.UseAuthentication();
    app.UseAuthorization();

    app.MapControllers();

    // Crear roles al iniciar la aplicación
    await CreateRoles(app.Services);

    app.Run();
}
catch (Exception ex)
{
    Console.WriteLine($"Exception: {ex.Message}");
    Console.WriteLine(ex.StackTrace);
}

async Task CreateRoles(IServiceProvider serviceProvider)
{
    using (var scope = serviceProvider.CreateScope())
    {
        var roleManager = scope.ServiceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
        string[] roleNames = { "admin", "User" };
        foreach (var roleName in roleNames)
        {
            if (!await roleManager.RoleExistsAsync(roleName))
            {
                await roleManager.CreateAsync(new IdentityRole<int>(roleName));
            }
        }
    }
}
