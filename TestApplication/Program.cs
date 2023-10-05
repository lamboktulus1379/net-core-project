using Confluent.Kafka;
using Microsoft.EntityFrameworkCore;
using Microsoft.OpenApi.Models;
using TestApplication.Infrastructure.Persistence;
using Microsoft.Extensions.Options;
using TestApplication.Core.Interfaces;
using TestApplication.Core.Models.Mail;
using TestApplication.Infrastructure.ExternalHttpClient;
using TestApplication.Infrastructure.ExternalHttpClient.Tulusfun;
using TestApplication.Infrastructure.Mail;
using TestApplication.Usecase;

var builder = WebApplication.CreateBuilder(args);
    

// TODO: Setup Kafka Schemas and the schemas registry
// Setup Kafka
builder.Services.Configure<ProducerConfig>(builder.Configuration.GetSection("Kafka"));
builder.Services.AddSingleton<IProducer<String, String>>(sp =>
{
    var config = sp.GetRequiredService<IOptions<ProducerConfig>>();

    return new ProducerBuilder<String, String>(config.Value)
        .Build();
});
// End of Setup Kafka

// Setup Mail
var emailConfig = builder.Configuration
    .GetSection("EmailConfiguration")
    .Get<EmailConfiguration>();
builder.Services.AddSingleton(emailConfig);
builder.Services.AddTransient<IEmailSender, EmailSender>();
// End of Setup Mail

// Setup HttpClientService
builder.Services.AddHttpClient<IHostClient, HostClient>();
builder.Services.AddScoped<ITulusFunClient, TulusFunClient>(sp =>
{
    var hostClient = sp.GetRequiredService<IHostClient>();
    string baseUrl = "https://tulus.fun/api/typings";

    return new TulusFunClient(hostClient, baseUrl);
});
// End Setup HttpClientService

// Setup Usecase
builder.Services.AddTransient<ITestUsecase, TestUsecase>();
// End of Setup Usecase

builder.Services.AddDbContext<RepositoryContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("connection")));
builder.Services.AddControllers();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "My Test Api", Version = "v1" });
});

var app = builder.Build();
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<RepositoryContext>();
    db.Database.Migrate();
}

app.MapGet("/", () => "Hello World!");

app.UseSwagger();
app.UseSwaggerUI(c =>
{
    c.SwaggerEndpoint("/swagger/v1/swagger.json", "This is my api");
});
app.MapControllers();
app.Run();

