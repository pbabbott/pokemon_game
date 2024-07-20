public class SpriteCoordinate {
    public int Row { get; set; }
    public int Column { get; set; }

    public SpriteCoordinate(int row, int column)
    {
        Row = row;
        Column = column;
    }

    public override string ToString()
    {
        return $"({Row}, {Column})";
    }
    public void Deconstruct(out int row, out int column)
    {
        row = Row;
        column = Column;
    }
}