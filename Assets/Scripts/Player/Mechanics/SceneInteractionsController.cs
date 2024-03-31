using UnityEngine;
using UnityEngine.Events;

namespace Character
{
    public class SceneInteractionsController : MonoBehaviour
    {
        private IPuzzle _currentPuzzle;
        public UnityAction<Collectable> OnItemCollected;

        public UnityAction<IPuzzle> OnPuzzleStarted;
        public UnityAction<IPuzzle> OnPuzzleExited;

        private void OnTriggerEnter(Collider other)
        {
            switch (other.tag)
            {
                case "Collectable":
                    if (other.TryGetComponent(out Collectable collectable))
                    {
                        OnItemCollected?.Invoke(collectable);
                        other.gameObject.SetActive(false);
                    }
                    break;
                case "Puzzle":
                    if (other.TryGetComponent(out IPuzzle puzzle))
                    {
                        OnPuzzleStarted?.Invoke(puzzle);
                        _currentPuzzle = puzzle;
                    }
                    break;
                default:
                    break;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.CompareTag("Puzzle"))
            {
                if (other.TryGetComponent(out IPuzzle puzzle))
                {
                    if (puzzle == _currentPuzzle)
                        OnPuzzleExited?.Invoke(puzzle);
                }
            }
        }
    }
}