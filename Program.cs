using GateWay;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddScoped<IClientContract, ClientService>();
builder.Services.AddScoped<IClientManagerContract, ClientManagerService>();

builder.Services.AddScoped<IContactContract, ContactService>();
builder.Services.AddScoped<IContactManagerContract, ContactManagerService>();

builder.WebHost.UseUrls("http://localhost:5083");
builder.Services.AddControllers();

builder.Services.AddDistributedMemoryCache();
builder.Services.AddLogging();

builder.Services.AddHttpClient("Client", client =>
{
    client.BaseAddress = new Uri("http://localhost:5227");
});

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyHeader()
                        .AllowAnyMethod());
});


builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();
app.MapSwagger();
app.UseCors("AllowAll");

app.Run();

