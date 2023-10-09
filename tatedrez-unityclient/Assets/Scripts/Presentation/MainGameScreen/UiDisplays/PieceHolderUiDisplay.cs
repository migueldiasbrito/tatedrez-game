using Mdb.Tatedrez.Data.Model;
using UnityEngine;
using UnityEngine.UI;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class PieceHolderUiDisplay : MonoBehaviour
    {
        [SerializeField] private PieceUiDisplay _pieceDisplay = default;
        [SerializeField] private Image _image = default;

        protected IPiece _piece;
        public IPiece Piece => _piece;

        protected PieceHolderState _state;

        public void Populate(IPiece piece)
        {
            _piece = piece;
            _pieceDisplay.Populate(piece);
        }

        public void SetState(PieceHolderState state)
        {
            _state = state;
            switch (state)
            {
                case PieceHolderState.Unselectable:
                    _image.color = new Color(0.6078431f, 0.6078431f, 0.6078431f, 0.5019608f);
                    break;
                case PieceHolderState.Selectable:
                    _image.color = new Color(0.7843137f, 0.7843137f, 0.7843137f);
                    break;
                case PieceHolderState.Selected:
                    _image.color = new Color(0.6078431f, 0.6078431f, 0.6078431f);
                    break;
            }
        }
    }
}
