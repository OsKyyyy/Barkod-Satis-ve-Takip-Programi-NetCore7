using WebUI;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy =>
        {
            policy.AllowAnyOrigin() // Tarayýcý tabanlý istekler için
                  .AllowAnyHeader()
                  .AllowAnyMethod();
        });
});
var startup = new Startup(builder.Configuration);
startup.ConfigureServices(builder.Services); // calling ConfigureServices method
var app = builder.Build();
app.UseCors("AllowAll");
startup.Configure(app, builder.Environment); // calling Configure method