using IceCreamApi;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<IceCreamShopContext>(options => 
  options.UseSqlServer(builder.Configuration.GetConnectionString("ApiSqlServer")));

var app = builder.Build();
using (var scope =  app.Services.CreateScope())
{
  scope.ServiceProvider.GetRequiredService<IceCreamShopContext>().Database.EnsureCreated();
}
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();
app.UseHttpsRedirection();

app.Run();

