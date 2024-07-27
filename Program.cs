

using TEST_DEV_EPA_26072024.context;
using TEST_DEV_EPA_26072024.contracts;
using TEST_DEV_EPA_26072024.repository;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddSingleton<DapperContext>();
builder.Services.AddScoped<IPersonaFisicaRepository, PersonaFisicaRepository>();
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
    app.UseDeveloperExceptionPage();
} else {

    app.UseHttpsRedirection();
} 


app.UseAuthorization();

app.MapControllers();

app.Run();