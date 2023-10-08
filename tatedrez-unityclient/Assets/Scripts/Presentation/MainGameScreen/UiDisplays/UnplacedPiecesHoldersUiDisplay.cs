using Mdb.Tatedrez.Data.Model;
using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class UnplacedPiecesHoldersUiDisplay : MonoBehaviour
    {
        [SerializeField] private UnplacedPieceHolderUiDisplay[] _unplacedPiecesHolders;

        private Action<IPiece> _onPieceSelected;
        private Action _onPieceDeselected;

        public void Initialize(Action<IPiece> onPieceSelected, Action onPieceDeselect)
        {
            _onPieceSelected = onPieceSelected;
            _onPieceDeselected = onPieceDeselect;

            foreach (UnplacedPieceHolderUiDisplay pieceHolder in _unplacedPiecesHolders)
            {
                pieceHolder.Initialize(OnPieceSelect, OnPieceDeselect);
            }
        }

        private void OnPieceSelect(UnplacedPieceHolderUiDisplay selectedPieceHolder)
        {
            foreach (UnplacedPieceHolderUiDisplay pieceHolder in _unplacedPiecesHolders)
            {
                pieceHolder.SetState(pieceHolder == selectedPieceHolder
                    ? PieceHolderState.Selected : PieceHolderState.Selectable);
            }

            _onPieceSelected.Invoke(selectedPieceHolder.Piece);
        }

        private void OnPieceDeselect()
        {
            _onPieceDeselected.Invoke();
        }

        public void Populate(IList<IPiece> unplacedPieces)
        {
            for (int i = 0; i < _unplacedPiecesHolders.Length; i++)
            {
                UnplacedPieceHolderUiDisplay unplacedPieceHolder = _unplacedPiecesHolders[i];

                if (unplacedPieces.Count <= i) unplacedPieceHolder.Hide();
                else
                {
                    unplacedPieceHolder.Populate(unplacedPieces[i]);
                    unplacedPieceHolder.SetState(PieceHolderState.Selectable);
                    unplacedPieceHolder.Show();
                }
            }
        }
    }
}