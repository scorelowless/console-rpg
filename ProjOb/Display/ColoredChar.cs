namespace ProjOb;

public struct ColoredChar
{
    public char Character { get; set; }
    public ConsoleColor Color { get; set; }

    public ColoredChar(char character, ConsoleColor color = ConsoleColor.White)
    {
        Character = character;
        Color = color;
    }

    public ColoredChar()
    {
    }
}