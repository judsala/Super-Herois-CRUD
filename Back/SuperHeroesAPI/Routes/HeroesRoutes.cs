using Microsoft.EntityFrameworkCore;
using SuperHeroesAPI.Data;
using SuperHeroesAPI.Models;
namespace SuperHeroesAPI.Routes;

public static class HeroesRoutes
{
  public static void MapHeroesRoutes(this WebApplication app)
    {
        //[GET]Lista todos os heróis
        app.MapGet("/heroes", async (AppDbContext db) => await db.Heroes.ToListAsync());

        //[GET]Retorna herói por id
        app.MapGet("/heroes/{id}", async (AppDbContext db, int id) =>
            await db.Heroes.FindAsync(id) is Hero hero ? Results.Ok(hero) : Results.NotFound());

        //[POST]Cria novo herói  
        app.MapPost("/heroes", async (AppDbContext db, Hero hero) =>
        {
            db.Heroes.Add(hero);
            await db.SaveChangesAsync();
            return Results.Created($"/heroes/{hero.Id}", hero);
        });

        //[PUT]Atualiza herói existente
        app.MapPut("/heroes/{id}", async (AppDbContext db, int id, Hero updatedHero) =>
        {
            var hero = await db.Heroes.FindAsync(id);
            if (hero is null) return Results.NotFound();

            hero.Nome = updatedHero.Nome;
            hero.NomeHeroi = updatedHero.NomeHeroi;
            hero.Superpoderes = updatedHero.Superpoderes;
            hero.DataNascimento = updatedHero.DataNascimento;
            hero.Altura = updatedHero.Altura;
            hero.Peso = updatedHero.Peso;

            await db.SaveChangesAsync();
            return Results.Ok(hero);
        });

        //[DELETE]Deleta herói
        app.MapDelete("/heroes/{id}", async (AppDbContext db, int id) =>
        {
            var hero = await db.Heroes.FindAsync(id);
            if (hero is null) return Results.NotFound();

            db.Heroes.Remove(hero);
            await db.SaveChangesAsync();
            return Results.Ok(hero);
        });
    }
}