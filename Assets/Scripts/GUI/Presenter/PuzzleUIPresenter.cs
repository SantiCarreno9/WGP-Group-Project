using Character;
using UnityEngine;

namespace Puzzle
{
    public class PuzzleUIPresenter : MonoBehaviour
    {
        [SerializeField] private PuzzleUIView _view;
        [SerializeField] private SceneInteractionsController _interactionsController;

        private IPuzzle _currentPuzzle = null;

        private void OnEnable()
        {
            _interactionsController.OnPuzzleStarted += ShowPuzzle;
            _interactionsController.OnPuzzleExited += HidePuzzle;

            _view.OnPuzzleSolved += SolvePuzzle;
        }

        private void OnDisable()
        {
            _interactionsController.OnPuzzleStarted -= ShowPuzzle;
            _interactionsController.OnPuzzleExited -= HidePuzzle;

            _view.OnPuzzleSolved -= SolvePuzzle;
        }

        private void ShowPuzzle(IPuzzle puzzle)
        {
            switch (puzzle.Type)
            {
                case PuzzleType.Door_Key:
                    _view.ShowDoorKeyPuzzle();
                    break;
                default:
                    break;
            }
            _currentPuzzle = puzzle;
        }

        private void HidePuzzle(IPuzzle puzzle)
        {
            switch (puzzle.Type)
            {
                case PuzzleType.Door_Key:
                    _view.HideDoorKeyPuzzle();
                    break;
                default:
                    break;
            }
            _currentPuzzle = null;
        }

        private void SolvePuzzle(PuzzleType puzzleType)
        {
            if (_currentPuzzle == null)
                return;

            switch (puzzleType)
            {
                case PuzzleType.Door_Key:
                    _currentPuzzle.Solve();
                    _view.HideDoorKeyPuzzle();
                    break;
                default:
                    break;
            }
        }
    }
}