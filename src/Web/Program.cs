using Application;
using Application.Common.Interfaces;
using Infrastructure;
using Web.Services;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using Microsoft.OpenApi.Models;
using Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateAudience = false,
            ValidateIssuer = false,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(
                Encoding.UTF8.GetBytes(builder.Configuration.GetSection("JWT:IssuerSigningKey").Value))
        };
    });

builder.Services.AddInfrastructure(builder.Configuration);
builder.Services.AddApplication();
builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();

builder.Services.AddControllers()
    .AddFluentValidation();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(setup =>
{
    setup.AddSecurityDefinition("Authorization", new OpenApiSecurityScheme
    {
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT",
        Description = "For authorization you must login in User Controller"
    });
    setup.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference { Type = ReferenceType.SecurityScheme, Id = "Authorization"},
            },
            new string [] {}
        }
    });

    var filePath = Path.Combine(System.AppContext.BaseDirectory, "Web.xml");
    setup.IncludeXmlComments(filePath);
});


var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var serviceProvider = scope.ServiceProvider;
    var context = serviceProvider.GetRequiredService<IApplicationDbContext>();
    var authService = serviceProvider.GetRequiredService<IAuthenticationService>();

    await SeedData.Initialize(context, authService);
}

if(app.Environment.IsDevelopment() || app.Environment.IsProduction()) 
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
        

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();

