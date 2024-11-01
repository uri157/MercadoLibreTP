using System.Text;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;

var builder = WebApplication.CreateBuilder(args);

// Configuración de CORS
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowSpecificOrigins", policy =>
    {
        policy.AllowAnyOrigin() // Agrega los dominios permitidos
              .AllowAnyMethod()                     // Permite todos los métodos HTTP (GET, POST, etc.)
              .AllowAnyHeader()                     // Permite todos los encabezados
              .AllowCredentials();                  // Permite el uso de credenciales (si es necesario)
    });
});

// Agregamos los controladores
builder.Services.AddControllers();

// Swagger para la documentación de la API
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    // Configuración básica de Swagger
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

// Configuración del contexto de base de datos para MercadoLibre
builder.Services.AddSqlServer<DbContext>(builder.Configuration.GetConnectionString("cnMercadoLibre"));

// Inyección de dependencias para los servicios de MercadoLibre
builder.Services.AddScoped<ICardService, CardDbService>();
builder.Services.AddScoped<ICardTypeService, CardTypeDbService>();
builder.Services.AddScoped<ICategoryService, CategoryDbService>();
builder.Services.AddScoped<INotificationService, NotificationDbService>();
builder.Services.AddScoped<IProductStateService, ProductStateDbService>();
builder.Services.AddScoped<IPublicationService, PublicationDbService>();
builder.Services.AddScoped<IPublicationStateService, PublicationStateDbService>();
builder.Services.AddScoped<IPhotoService, PhotoDbService>();
builder.Services.AddScoped<IColorService, ColorDbService>();
// builder.Services.AddScoped<IReviewService, ReviewDbService>();
builder.Services.AddScoped<IAccountService, AccountDbService>();
builder.Services.AddScoped<IPhotoPublicationService, PhotoPublicationDbService>();
builder.Services.AddScoped<ITransactionService, TransactionDbService>();
builder.Services.AddScoped<IShoppingCartService, ShoppingCartDbService>();
builder.Services.AddScoped<IPublicationVisitedService, PublicationVisitedDbService>();

//builder.Services.AddScoped<IUserService, UserDbService>(); // Ejemplo para el servicio de usuarios



// Agrega otros servicios según tu API

// Configurar el contexto de Identity (autenticación y autorización)
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
        ValidateIssuer = true, // Valida que el emisor del token sea el esperado
        ValidateAudience = true, // Valida que la audiencia del token sea la esperada
        ValidateLifetime = true, // Valida que el token no haya expirado
        ValidateIssuerSigningKey = true, // Verifica que el token esté firmado con la clave correcta
        ValidIssuer = builder.Configuration["Jwt:Issuer"], // Especifica el emisor esperado del token
        ValidAudience = builder.Configuration["Jwt:Audience"], // Especifica la audiencia esperada del token
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(builder.Configuration["Jwt:Key"])) // Clave secreta para firmar el token
    };
});

var app = builder.Build();

// Configuración del pipeline de solicitud HTTP
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowSpecificOrigins");

app.UseAuthentication(); // Asegúrate de que la autenticación esté habilitada
app.UseAuthorization();  // Asegúrate de que la autorización esté habilitada

app.MapControllers();

app.Run();

async Task CreateRoles(IServiceProvider serviceProvider)
{
    var roleManager = serviceProvider.GetRequiredService<RoleManager<IdentityRole<int>>>();
    string[] roleNames = { "admin", "User" };
    foreach (var roleName in roleNames)
    {
        if (!await roleManager.RoleExistsAsync(roleName))
        {
            await roleManager.CreateAsync(new IdentityRole<int>(roleName));
        }
    }
}

await CreateRoles(app.Services);
