using Microsoft.EntityFrameworkCore;
using SuperHeroesAPI.Data;
using SuperHeroesAPI.Models;
namespace SuperHeroesAPI.Routes;

public static class HeroesRoutes
{
  public static void MapHeroesRoutes(this WebApplication app)
    {
        app.MapGet("/heroes", async (AppDbContext db) => await db.Heroes.ToListAsync());

        app.MapGet("/heroes/{id}", async (AppDbContext db, int id) =>
            await db.Heroes.FindAsync(id) is Hero hero ? Results.Ok(hero) : Results.NotFound());

        app.MapPost("/heroes", async (AppDbContext db, Hero hero) =>
        {
            db.Heroes.Add(hero);
            await db.SaveChangesAsync();
            return Results.Created($"/heroes/{hero.Id}", hero);
        });

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

        app.MapDelete("/heroes/{id}", async (AppDbContext db, int id) =>
        {
            var hero = await db.Heroes.FindAsync(id);
            if (hero is null) return Results.NotFound();

            db.Heroes.Remove(hero);
            await db.SaveChangesAsync();
            return Results.Ok(hero);
        });
    }
  // public static List<Hero> Heroes = new List<Hero>() {
  //   new Hero(Guid.NewGuid(), "Clark Kent", "Superman", "Força, Voo, Visão de Raio-X", "18/04/1938", "1,85m", "90kg"),
  //   new Hero(Guid.NewGuid(), "Bruce Wayne", "Batman", "Inteligência, Habilidade em Luta, Tecnologia Avançada", "19/03/1939", "1,85m", "95kg"),
  //   new Hero(Guid.NewGuid(), "Diana Prince", "Mulher Maravilha", "Força, Voo, Luta com Espada", "21/03/1939", "1,83m", "74kg")
  // };

  // public static void MapHeroesRoutes(this WebApplication app)
  // {
  //   app.MapGet("/heroes", () => Heroes);

  //   app.MapGet("/heroes/{id}", (Guid id) => Heroes.Find(h => h.Id == id));

  //   app.MapPost("/heroes", (Hero hero) => {
  //     Heroes.Add(hero);
  //     return hero;    
  //   });

  //   app.MapPut("/heroes/{id}", (Guid id, Hero hero) => {
  //     var index = Heroes.Find(h => h.Id == id);
  //     if (index == null) return Results.NotFound();
  //     index.Nome = hero.Nome;
  //     return Results.Ok(index);
  //   });

  //   app.MapDelete("/heroes/{id}", (Guid id) => {
  //     var index = Heroes.Find(h => h.Id == id);
  //     if (index == null) return Results.NotFound();
  //     Heroes.Remove(index);
  //     return Results.Ok(index);
  //   });
  // }
}