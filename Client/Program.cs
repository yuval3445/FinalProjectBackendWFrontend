var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.UseStaticFiles();
app.UseDefaultFiles();
app.Run();
