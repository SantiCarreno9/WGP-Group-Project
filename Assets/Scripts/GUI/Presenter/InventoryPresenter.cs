using UnityEngine;

namespace Character.UI
{
    public class InventoryPresenter : MonoBehaviour
    {
        [SerializeField] private DummyInventoryView _inventoryView;
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
            switch (item.Category)
            {
                case CollectableCategory.Key:
                    _inventoryView.AddKey();
                    break;
                default:
                    break;
            }
        }
    }
}