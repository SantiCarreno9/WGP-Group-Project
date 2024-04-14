using UnityEngine;
using UnityEngine.Events;

namespace Puzzle
{
    public class DoorPuzzle : MonoBehaviour, IPuzzle
    {
        [SerializeField] private GameObject _puzzleObstacle;
        [SerializeField] private TriggerArea _doorArea;

        public UnityEvent OnSolved;

        public PuzzleType Type => PuzzleType.Door_Key;

        private void Start()
        {
            SetUp();
        }

        public void SetUp()
        {
            _puzzleObstacle.SetActive(true);
            _doorArea.enabled = false;
        }

        public void Solve()
        {
            OnSolved?.Invoke();
            _doorArea.enabled = true;
            _puzzleObstacle.SetActive(false);            
        }

    }
}