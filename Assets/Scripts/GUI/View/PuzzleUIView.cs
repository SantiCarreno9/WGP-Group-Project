using UI;
using UnityEngine;
using UnityEngine.Events;

namespace Puzzle
{
    public class PuzzleUIView : MonoBehaviour
    {
        [SerializeField] private GameObject _doorKeyPuzzleCont;
        [SerializeField] private Dropzone _doorPuzzle;
        public UnityAction<PuzzleType> OnPuzzleSolved;

        private void OnEnable()
        {
            _doorPuzzle.OnItemDroppedOn += SolveDoorPuzzle;
        }

        private void OnDisable()
        {
            _doorPuzzle.OnItemDroppedOn -= SolveDoorPuzzle;
        }

        public void ShowDoorKeyPuzzle()
        {
            _doorKeyPuzzleCont.SetActive(true);
        }

        public void HideDoorKeyPuzzle()
        {
            _doorKeyPuzzleCont.SetActive(false);
        }

        private void SolveDoorPuzzle()
        {
            OnPuzzleSolved?.Invoke(PuzzleType.Door_Key);
        }

    }
}