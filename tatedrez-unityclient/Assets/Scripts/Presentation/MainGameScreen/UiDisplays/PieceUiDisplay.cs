using Mdb.Tatedrez.Data.Model;
using TMPro;
using UnityEngine;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class PieceUiDisplay : MonoBehaviour
    {
        [SerializeField] private TMP_Text pieceText;

        public void Populate(IPiece piece)
        {
            pieceText.text = piece.PieceType.ToString();
            pieceText.color = piece.Player == Player.White ? Color.white : Color.black;
        }
    }
}
