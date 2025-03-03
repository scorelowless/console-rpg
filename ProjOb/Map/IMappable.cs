using System.Drawing;

namespace ProjOb;

public interface IMappable
{
    Point  Position { get; set; }
    char Display { get; set; }
}