using Mdb.Tatedrez.Data.Model;
using UnityEngine;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class UnplacedPieceHolderUiDisplay : MonoBehaviour
    {
        [SerializeField] private PieceUiDisplay _piece = default;

        public void Show()
        {
            gameObject.SetActive(true);
        }

        public void Hide()
        {
            gameObject.SetActive(false);
        }

        public void Populate(IPiece piece)
        {
            _piece.Populate(piece);
        }
    }
}