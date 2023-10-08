namespace Mdb.Tatedrez.Data.Model
{
    public class Piece : IPiece
    {
        public PieceType PieceType { get; set; }

        public Player Player { get; set; }

        public Piece(PieceType type, Player player)
        {
            PieceType = type;
            Player = player;
        }
    }
}
