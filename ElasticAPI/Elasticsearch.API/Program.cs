using Elasticsearch.API;
using Elasticsearch.API.Middlewares;
using Elasticsearch.API.Repositories;
using Elasticsearch.API.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddElastic(builder.Configuration);
builder.Services.AddAutoMapper(typeof(Program));
builder.Services.AddScoped<ProductService>();
builder.Services.AddScoped<ProductRepository>();
builder.Services.AddScoped<ECommerceRepository>();
builder.Services.AddScoped<ECommerceService>();
var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.ConfigureExcepitonHandler();
app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
