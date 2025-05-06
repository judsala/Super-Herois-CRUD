namespace SuperHeroesAPI.Models;

public class Hero
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeHeroi { get; set; }
    public string Superpoderes { get; set; }
    public string DataNascimento { get; set; }
    public string Altura { get; set; }
    public string Peso { get; set; }

    public Hero() { }

    public Hero(int id, string nome, string nomeHeroi, string superpoderes, string dataNascimento, string altura, string peso)
    {
      Id = id;
      Nome = nome;
      NomeHeroi = nomeHeroi;
      Superpoderes = superpoderes;
      DataNascimento = dataNascimento;
      Altura = altura;
      Peso = peso;
    }
}