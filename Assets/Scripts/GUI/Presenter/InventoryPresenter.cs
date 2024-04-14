using UnityEngine;
using Character;

namespace Inventory
{
    public class InventoryPresenter : MonoBehaviour
    {
        [SerializeField] private InventoryView _inventoryView;
        [SerializeField] private SceneInteractionsController interactionsController;

        private void OnEnable()
        {
            interactionsController.OnItemCollected += OnCollectablePickedUp;
        }

        private void OnDisable()
        {
            interactionsController.OnItemCollected -= OnCollectablePickedUp;
        }

        private void OnCollectablePickedUp(Collectable item)
        {
            _inventoryView.AddCollectible(item.Category);
        }
    }
}