using JetBrains.Annotations;
using System;

namespace Mdb.Tatedrez.Presentation.MainGameScreen.UiDisplays
{
    public class BoardCellUiDisplay : PieceHolderUiDisplay
    {
        public int Row { get; private set; }
        public int Column { get; private set; }

        private Action<BoardCellUiDisplay> _onSelect;
        private Action _onDeselect;

        public void Initialize(int row, int column, Action<BoardCellUiDisplay> onSelect, Action onDeselect)
        {
            Row = row;
            Column = column;

            _onSelect = onSelect;
            _onDeselect = onDeselect;
        }

        [UsedImplicitly]
        public void Select()
        {
            switch (_state)
            {
                case PieceHolderState.Selectable:
                    _onSelect.Invoke(this);
                    break;
                case PieceHolderState.Selected:
                    _onDeselect.Invoke();
                    SetState(PieceHolderState.Selectable);
                    break;
            }
        }
    }
}
