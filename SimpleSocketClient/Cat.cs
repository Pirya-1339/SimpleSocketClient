namespace SimpleSocketClient;

public class Cat
{
    public string Name { get; set; }
    public int Age { get; set; }
    public int Sex { get; set; }
    public string Owner { get; set; }
    public override string ToString()
    {
        return $"Name = {Name}, Age = {Age}, Sex = {Sex}, Owner = {Owner}";
    }
}
