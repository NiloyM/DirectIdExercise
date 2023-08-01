using DirectIdExercise.Configuration;
using DirectIdExercise.MiddleWare;
using Microsoft.AspNetCore.HttpLogging;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddMediatR((configuration) => configuration.RegisterServicesFromAssembly(typeof(Program).Assembly));
builder.Services.AddHttpLogging(logging =>
{
    logging.LoggingFields = HttpLoggingFields.All;
    logging.RequestHeaders.Add("statement-generation");
    logging.ResponseHeaders.Add("statement- generation-response");
    logging.MediaTypeOptions.AddText("application/json");
    logging.RequestBodyLogLimit = 4096;
    logging.ResponseBodyLogLimit = 4096;

});
builder.Services.Configure<DirectIdConfiguration>(
    builder.Configuration.GetSection("DirectIdConfiguration"));
builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationInsightsTelemetry();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpLogging();

app.UseHttpsRedirection();

app.UseGlobalExceptionMiddleware();

app.UseAuthorization();

app.MapControllers();

app.Run();
