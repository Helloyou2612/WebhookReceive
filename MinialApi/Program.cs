using System.Text;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

var summaries = new[]
{
    "Freezing", "Bracing", "Chilly", "Cool", "Mild", "Warm", "Balmy", "Hot", "Sweltering", "Scorching"
};

app.MapGet("/weatherforecast", () =>
{
    var forecast = Enumerable.Range(1, 5).Select(index =>
        new WeatherForecast
        (
            DateTime.Now.AddDays(index),
            Random.Shared.Next(-20, 55),
            summaries[Random.Shared.Next(summaries.Length)]
        ))
        .ToArray();
    return forecast;
})
.WithName("GetWeatherForecast");

#region Webhook recevice

app.MapPost("/api/WebhookRecivice", async (HttpRequest request) =>
{
    return await request.WebhookData();
})
    .WithName("WebhookRecivice");

#endregion 

app.Run();

internal record WeatherForecast(DateTime Date, int TemperatureC, string? Summary)
{
    public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
}

public static class Enpoints
{    
    public static async Task<IResult> WebhookData(this HttpRequest request, Encoding? encoding = null, Stream? inputStream = null)
    {
        try
        {
            encoding ??= Encoding.UTF8;
            inputStream ??= request.Body;

            using (var reader = new StreamReader(inputStream, encoding))
                return Results.Ok(await reader.ReadToEndAsync());
        }
        catch (Exception ex)
        {
            return Results.Problem(ex.Message);
        }
    }
}