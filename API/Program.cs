using API.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddDbContext<AppDataContext>();

var app = builder.Build();

app.MapGet("/", () => "Chamados");

//ENDPOINTS DE TAREFA
//GET: http://localhost:5000/api/chamado/listar
app.MapGet("/api/chamado/listar", ([FromServices] AppDataContext ctx) =>
{
    if (ctx.Chamados.Any())
    {
        return Results.Ok(ctx.Chamados.ToList());
    }
    return Results.NotFound("Nenhum chamado encontrada");
});

//POST: http://localhost:5000/api/chamado/cadastrar
app.MapPost("/api/chamado/cadastrar", ([FromServices] AppDataContext ctx, [FromBody] Chamado chamado) =>
{
    ctx.Chamados.Add(chamado);
    ctx.SaveChanges();
    return Results.Created("", chamado);
});

//PATCH: http://localhost:5000/chamado/alterar/{id}
app.MapPatch("/api/chamado/alterar/{id}", 
([FromServices] AppDataContext ctx, [FromRoute] string id, [FromBody] Chamado alterarChamado) =>
{
    Chamado? Resultado = ctx.Chamados.Find(id);

    if (Resultado == null)
    {
        return Results.NotFound("Chamado não encontrado!");
    }

    Resultado.Status = alterarChamado.Status;
    ctx.Chamados.Update(Resultado);
    ctx.SaveChanges();
    return Results.Ok(Resultado);
});

//GET: http://localhost:5000/chamado/naoconcluidas
app.MapGet("/api/chamado/naoresolvidos", ([FromServices] AppDataContext ctx) =>
{
    
});

//GET: http://localhost:5000/chamado/concluidas
app.MapGet("/api/chamado/resolvidos", ([FromServices] AppDataContext ctx) =>
{

});


app.Run();
