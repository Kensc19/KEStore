using System;
using System.Text;
using System.Threading.Tasks;
using Core;
using KEStoreApi;
using KEStoreApi.Data;
using KEStoreApi.Hubs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using MySqlConnector;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddSignalR();

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(
    policy =>
    {
        policy.WithOrigins("http://localhost:3000")
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials();
    });
});
builder.Services.AddScoped<SaleLogic>();
builder.Services.AddScoped<CampaignService>();

var security = Environment.GetEnvironmentVariable("Security");

builder.Services.AddSwaggerGen(setup =>
{
    var jwtSecurityScheme = new OpenApiSecurityScheme
    {
        BearerFormat = "JWT",
        Name = "JWT Authentication",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = JwtBearerDefaults.AuthenticationScheme,
        Description = "Put **_ONLY_** your JWT Bearer token on textbox below!",

        Reference = new OpenApiReference
        {
            Id = JwtBearerDefaults.AuthenticationScheme,
            Type = ReferenceType.SecurityScheme
        }
    };

    setup.AddSecurityDefinition(jwtSecurityScheme.Reference.Id, jwtSecurityScheme);

    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        { jwtSecurityScheme, Array.Empty<string>() }
    });
});

// JWT Authentication configuration
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, options =>
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

        options.Events = new JwtBearerEvents
        {
            OnAuthenticationFailed = context =>
            {
                return Task.CompletedTask;
            }
        };
    });

var app = builder.Build();

string connection;
var dbConnection = Environment.GetEnvironmentVariable("DB");

if (string.IsNullOrEmpty(dbConnection))
{
    builder.Configuration.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
    connection = builder.Configuration.GetConnectionString("MyDatabase");
}
else
{
    connection = dbConnection;
}

DatabaseConfiguration.Init(connection);
try
{
    DatabaseStore.VerifyAndCreateDatabase(connection);
}
catch (Exception ex)
{
    throw new Exception("Error connecting to MySQL or creating the database.", ex);
}

if (app.Environment.IsDevelopment())
{
    try
    {
        DatabaseStore.Store_MySql();
    }
    catch (Exception ex)
    {
        throw new Exception("Error initializing the database.", ex);
    }
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseRouting();
app.UseCors();

app.UseAuthentication();
app.UseAuthorization();

app.UseEndpoints(endpoints =>
{
    endpoints.MapControllers();
    endpoints.MapHub<CampaignHub>("/hub/campaignsHub");     
});

app.Run();
