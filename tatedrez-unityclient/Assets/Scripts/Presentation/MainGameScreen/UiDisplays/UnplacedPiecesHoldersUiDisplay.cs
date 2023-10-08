using Mdb.Tatedrez.Data.Model;
using System.Collections.Generic;
using UnityEngine;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class UnplacedPiecesHoldersUiDisplay : MonoBehaviour
    {
        [SerializeField] private UnplacedPieceHolderUiDisplay[] _unplacedPiecesHolders;

        public void Populate(IList<IPiece> unplacedPieces)
        {
            for (int i = 0; i < _unplacedPiecesHolders.Length; i++)
            {
                UnplacedPieceHolderUiDisplay unplacedPieceHolder = _unplacedPiecesHolders[i];

                if (unplacedPieces.Count < i) unplacedPieceHolder.Hide();
                else
                {
                    unplacedPieceHolder.Populate(unplacedPieces[i]);
                    unplacedPieceHolder.Show();
                }
            }
        }
    }
}