using UnityEngine;
using UI;
using TMPro;

namespace Inventory
{
    public class InventoryView : MonoBehaviour
    {
        [SerializeField] private DraggableItem _keyItem;
        [SerializeField] private TMP_Text _message;

        [SerializeField] private float _messageDuration = 2.0f;
        [SerializeField] InventoryItem syringeSlot;
        [SerializeField] InventoryItem bottleSlot;

        public void AddKey()
        {
            
        }
        public void AddCollectible(CollectableCategory collectableCategory)
        {
            switch (collectableCategory)
            {
                case CollectableCategory.Key:
                    _keyItem.gameObject.SetActive(true);                    
                    break;
                case CollectableCategory.Syringe:
                    syringeSlot.Increment();
                    break;
                case CollectableCategory.Bottle:
                    bottleSlot.Increment();
                    break;
                default:
                    break;
            }
            ShowHideMessage("Item collected");            
        }

        private void ShowMessage(string text)
        {
            _message.gameObject.SetActive(true);
            _message.text = text;
        }

        private void HideMessage()
        {
            _message.gameObject.SetActive(false);
        }

        private void ShowHideMessage(string text)
        {
            ShowMessage(text);
            Invoke(nameof(HideMessage), _messageDuration);
        }

    }
}