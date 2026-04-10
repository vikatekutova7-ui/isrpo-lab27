using System.Data;
using System.Data.Common;
using System.Diagnostics;
using System.Reflection.Emit;
using System.Runtime.InteropServices.Marshalling;

var builder = WebApplication.CreateBuilder(args);
var app = builder.Build();

app.Use(async (context, next) =>
{
    Console.WriteLine($"[LOG] Ответ отправлен: {context.Request.Method} {context.Request.Path}");
        await next(context);
    Console.WriteLine($"[LOG] Ответ отправлен {context.Request.Method} {context.Request.Path}");
});

app.Use(async (context, next) =>
{
    context.Response.Headers.Append("X-powered-By", "ASP.NET Core Lab27");
    await next(context);
});

app.MapGet("/", () => "Добро пожаловать на сервер!");
app.MapGet("/about", () => "Это мой первый ASP.NET Core сервер");
app.MapGet("/time", () => $"Время на сервере: {DateTime.Now}");
app.MapGet("/hello/{name}", (string name) => $"Привет, {name}!");
app.MapGet("/hello/{name}", (string name) => $"Привет, {name}!");
app.MapGet("/sum/{a}/{b}", (int a, int b) => $"{a + b}");

app.MapGet("/student", () => new
{
    Name = "Текутова Вика",
    Group = "ИСП-231",
    Year = 3,
    IsActive = true
});

app.MapGet("/subjects", () => new[]
{
    "РПМ",
    "РМП",
    "ИСРПО",
    "СП",
});

app.MapGet("/product/{id}", (int id) => new Product
(
    Id: id,
    Name: $"Товар #{id}",
    Price: id * 99.99m,
    InStock: id % 2 == 0
));

app.Run();

record Product(int Id, string Name, decimal Price, bool InStock);