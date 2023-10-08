using System.Collections.Generic;
using System.Linq;

namespace Mdb.Tatedrez.Data.Model
{
    public class GameModel : IGameDataReader
    {
        public Piece[][] BoardImplementation { get; set; }
        public IPiece[][] Board => BoardImplementation;

        public List<Piece> UnplacedPiecesImplementation { get; set; }
        public IList<IPiece> UnplacedPieces => UnplacedPiecesImplementation.Cast<IPiece>().ToList();

        public Player CurrentPlayer { get; set; }

        public bool CanMovePieceAt(int row, int column)
        {
            Piece piece = BoardImplementation[row][column];
            if (piece == null) return false;

            if (piece.Player != CurrentPlayer) return false;

            if (UnplacedPiecesImplementation.Any(x => x.Player == piece.Player))
                return false;

            if (GetPossibleMovesForPieceAt(row, column).Count == 0) return false;

            return true;
        }

        public bool CanPlacePieceAt(int row, int column)
        {
            return BoardImplementation[row][column] == null;
        }

        public IList<(int, int)> GetPossibleMovesForPieceAt(int row, int column)
        {
            Piece piece = BoardImplementation[row][column];

            List < (int, int) > result = new List<(int, int)>();

            if (piece == null) return result;
            if (piece.Player != CurrentPlayer) return result;

            switch (piece.PieceType)
            {
                case PieceType.Rook:
                    // left
                    int leftRow = row - 1;
                    while (leftRow >= 0)
                    {
                        if (BoardImplementation[leftRow][column] != null) break;

                        result.Add((leftRow, column));

                        leftRow--;
                    }

                    // right
                    int rightRow = row + 1;
                    while (rightRow < 3)
                    {
                        if (BoardImplementation[rightRow][column] != null) break;

                        result.Add((rightRow, column));

                        rightRow++;
                    }

                    // up
                    int topColumn = column - 1;
                    while (topColumn >= 0)
                    {
                        if (BoardImplementation[row][topColumn] != null) break;

                        result.Add((row, topColumn));

                        topColumn--;
                    }

                    // down
                    int bottomColumn = column + 1;
                    while (bottomColumn < 3)
                    {
                        if (BoardImplementation[row][bottomColumn] != null) break;

                        result.Add((row, bottomColumn));

                        bottomColumn++;
                    }

                    break;
                case PieceType.Bishop:
                    // top left
                    int newRow = row - 1;
                    int newColumn = column - 1;

                    while (newRow >= 0 && newColumn >= 0)
                    {
                        if (BoardImplementation[newRow][newColumn] != null) break;

                        result.Add((newRow, newColumn));

                        newRow--;
                        newColumn--;
                    }

                    // top right
                    newRow = row + 1;
                    newColumn = column - 1;

                    while (newRow < 3 && newColumn >= 0)
                    {
                        if (BoardImplementation[newRow][newColumn] != null) break;

                        result.Add((newRow, newColumn));

                        newRow++;
                        newColumn--;
                    }

                    // bottom left
                    newRow = row - 1;
                    newColumn = column + 1;

                    while (newRow >= 0 && newColumn < 3)
                    {
                        if (BoardImplementation[newRow][newColumn] != null) break;

                        result.Add((newRow, newColumn));

                        newRow--;
                        newColumn++;
                    }

                    // bottom right
                    newRow = row + 1;
                    newColumn = column + 1;

                    while (newRow < 3 && newColumn < 3)
                    {
                        if (BoardImplementation[newRow][newColumn] != null) break;

                        result.Add((newRow, newColumn));

                        newRow++;
                        newColumn++;
                    }

                    break;
                case PieceType.Knight:
                    if (row - 1 >= 0 && column - 2 >= 0 && BoardImplementation[row - 1][column - 2] == null)
                        result.Add((row - 1, column - 2));
                    if (row - 2 >= 0 && column - 1 >= 0 && BoardImplementation[row - 2][column - 1] == null)
                        result.Add((row - 2, column - 1));
                    if (row + 2 < 3 && column - 1 >= 0 && BoardImplementation[row + 2][column - 1] == null)
                        result.Add((row + 2, column - 1));
                    if (row + 1 < 3 && column - 2 >= 0 && BoardImplementation[row + 1][column - 2] == null)
                        result.Add((row + 1, column - 2));
                    if (row + 1 < 3 && column + 2 < 3 && BoardImplementation[row + 1][column + 2] == null)
                        result.Add((row + 1, column + 2));
                    if (row + 2 < 3 && column + 1 < 3 && BoardImplementation[row + 2][column + 1] == null)
                        result.Add((row + 2, column + 1));
                    if (row - 2 >= 0 && column + 1 < 3 && BoardImplementation[row - 2][column + 1] == null)
                        result.Add((row - 2, column + 1));
                    if (row - 1 >= 0 && column + 2 < 3 && BoardImplementation[row - 1][column + 2] == null)
                        result.Add((row - 1, column + 2));

                    break;
            }

            return result;
        }

        public bool CanCurrentPlayerMove()
        {
            if (UnplacedPiecesImplementation.Any(x => x.Player == CurrentPlayer)) return true;

            for (int row = 0; row < 3; row++)
                for (int column = 0; column < 3; column++)
                    if (CanMovePieceAt(row, column)) return true;

            return false;
        }
    }
}
