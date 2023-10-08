namespace Mdb.Tatedrez.Data.Model
{
    public interface IPiece
    {
        PieceType PieceType { get; }
        Player Player { get; }
    }
}
