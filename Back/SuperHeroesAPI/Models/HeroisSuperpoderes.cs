namespace SuperHeroesAPI.Models;

public class HeroisSuperpoderes
{
    public int HeroId { get; set; }
    public Hero Hero { get; set; }

    public int SuperpoderId { get; set; }
    public Superpoder Superpoder { get; set; }
}