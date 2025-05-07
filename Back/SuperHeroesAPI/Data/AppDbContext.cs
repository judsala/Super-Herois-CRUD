using Microsoft.EntityFrameworkCore;
using SuperHeroesAPI.Models;

namespace SuperHeroesAPI.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

    public DbSet<Hero> Heroes { get; set; }
    public DbSet<Superpoder> Superpoderes { get; set; }
    public DbSet<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configura a chave composta para HeroisSuperpoderes
        modelBuilder.Entity<HeroisSuperpoderes>()
            .HasKey(hs => new { hs.HeroId, hs.SuperpoderId });

        // Configura os relacionamentos
        modelBuilder.Entity<HeroisSuperpoderes>()
            .HasOne(hs => hs.Hero)
            .WithMany(h => h.HeroisSuperpoderes)
            .HasForeignKey(hs => hs.HeroId);

        modelBuilder.Entity<HeroisSuperpoderes>()
            .HasOne(hs => hs.Superpoder)
            .WithMany(s => s.HeroisSuperpoderes)
            .HasForeignKey(hs => hs.SuperpoderId);

        // Dados iniciais
        // modelBuilder.Entity<Hero>().HasData(
        //     new Hero
        //     {
        //         Id = 1,
        //         Nome = "Clark Kent",
        //         NomeHeroi = "Superman",
        //         Superpoderes = "Força, Voo, Visão de Raio-X",
        //         DataNascimento = "18/04/1938",
        //         Altura = "1,85m",
        //         Peso = "90kg"
        //     },
        //     new Hero
        //     {
        //         Id = 2,
        //         Nome = "Bruce Wayne",
        //         NomeHeroi = "Batman",
        //         Superpoderes = "Inteligência, Habilidade em Luta, Tecnologia Avançada",
        //         DataNascimento = "19/03/1939",
        //         Altura = "1,85m",
        //         Peso = "95kg"
        //     },
        //     new Hero
        //     {
        //         Id = 3,
        //         Nome = "Diana Prince",
        //         NomeHeroi = "Mulher Maravilha",
        //         Superpoderes = "Força, Voo, Luta com Espada",
        //         DataNascimento = "21/03/1939",
        //         Altura = "1,83m",
        //         Peso = "74kg"
        //     }
        // );
    }
}