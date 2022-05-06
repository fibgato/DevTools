using NetDevTools.Web.MVC.Configurations;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityConfigurations();

builder.Services.AddMvcConfiguration();

builder.Services.RegisterServices();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseMvcConfiguration(WebApplication.CreateBuilder(args).Environment);

app.Run();
