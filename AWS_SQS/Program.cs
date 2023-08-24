using AWS_SQS.Factories;
using AWS_SQS.Interfaces;
using AWS_SQS.Models;
using AWS_SQS.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAWSLambdaHosting(LambdaEventSource.RestApi);
builder.Services.AddTransient<ISqsService, SqsService>();
builder.Services.AddSingleton<ISqsClientFactory, SqsClientFactory>();
builder.Services.Configure<SqsOptions>(builder.Configuration.GetSection("SqsOptions"));

builder.Services.AddControllers();
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

app.UseAuthorization();

app.MapControllers();

app.Run();
