namespace SuperHeroesAPI.Models;

public class Superpoder
{
    public int Id { get; set; }
    public string Nome { get; set; }
    public string Descricao { get; set; }

    // Relacionamento com HeroisSuperpoderes
    public ICollection<HeroisSuperpoderes> HeroisSuperpoderes { get; set; }
}