namespace TestNLogLayoutsForELK;

public class Car
{
    public int Id { get; set; }
    public string Color { get; set; } = "";
    public decimal Length { get; set; }

    public override string ToString()
    {
        return $"{Color} car {Length}m with id={Id}";
    }
}