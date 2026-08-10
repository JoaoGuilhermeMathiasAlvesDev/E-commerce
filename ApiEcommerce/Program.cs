using Infrectetura.Ioc;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.ConexecaoBanco(builder.Configuration);
builder.Services.IdependenciaRepositorios();
builder.Services.IdependenciaServicos();
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();

    // Habilita a interface visual do ReDoc na rota /redoc
    app.UseReDoc(options =>
    {
        options.SpecUrl("/swagger/v1/swagger.json");
        options.RoutePrefix = "redoc";
    });
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
