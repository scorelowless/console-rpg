namespace ProjOb;

public struct ColoredChar
{
    public char Character { get; }
    public ConsoleColor Color { get; }

    public ColoredChar(char character, ConsoleColor color = ConsoleColor.White)
    {
        Character = character;
        Color = color;
    }
}