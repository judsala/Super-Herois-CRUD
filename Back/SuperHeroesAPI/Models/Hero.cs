namespace SuperHeroesAPI.Models;

public class Hero
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string NomeHeroi { get; set; }
    public string Superpoderes { get; set; }
    public string DataNascimento { get; set; }
    public float Altura { get; set; }
    public float Peso { get; set; }

    public ICollection<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }

    public Hero() { }

    public Hero(int id, string nome, string nomeHeroi, string superpoderes, string dataNascimento, float altura, float peso)
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