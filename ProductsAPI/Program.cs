using System.Text;
using Azure.Storage.Blobs;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using ProductsAPI.Data;
using ProductsAPI.Filters;
using ProductsAPI.Services;

var builder = WebApplication.CreateBuilder(args);

var tokenValParams = new TokenValidationParameters
{
    ValidateIssuer = true,
    ValidateAudience = true,
    ValidateLifetime = true,
    ValidateIssuerSigningKey = true,
    ValidIssuer = "products-issuer",
    ValidAudience = "products-audience",
    IssuerSigningKey =
        new SymmetricSecurityKey(Encoding.UTF8.GetBytes("[k#%Yq~u1/*r1Oa%1!NN+TyF[$8Bs32/2Kjsko&%ci0jsdc"))
};

var blobConnectionString = builder.Configuration.GetSection("AzureBlobStorage:ConnectionString").Value;
var containerName = builder.Configuration.GetSection("AzureBlobStorage:ContainerName").Value;

builder.Services.AddSingleton(x => new BlobServiceClient(blobConnectionString));
builder.Services.AddSingleton(x =>
{
    var blobServiceClient = x.GetRequiredService<BlobServiceClient>();
    return blobServiceClient.GetBlobContainerClient(containerName);
});
builder.Services.AddTransient<ProductServices>();
builder.Services.AddTransient<UserServices>();
builder.Services.AddTransient<BillServices>();

// Add services to the container.

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options => { options.TokenValidationParameters = tokenValParams; });

builder.Services.AddControllers(options =>
{
    options.Filters.Add<RouteTrackingFilter>(); // Add Filter globally
    options.Filters.Add<AuthorizationFilter>(); // Add Filter globally
});

builder.Services.AddDbContext<ApiDbContext>(options =>
    options.UseMySql(builder.Configuration.GetConnectionString("DefaultConnection"),
        new MySqlServerVersion(new Version(8, 0, 21)))); // Version de MySQL

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthorization();
builder.Services.AddLogging();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();