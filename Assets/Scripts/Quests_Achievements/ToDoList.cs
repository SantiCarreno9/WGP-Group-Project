using System.Collections.Generic;
using UnityEngine;

namespace QuestAchievements.UI
{
    public class ToDoList : MonoBehaviour
    {
        [SerializeField] private Transform _container;
        [SerializeField] private ToDoItem _itemPrefab;
        private Dictionary<int, ToDoItem> _items = new Dictionary<int, ToDoItem>();

        public void AddItem(ToDoAction action)
        {
            ToDoItem instance = Instantiate(_itemPrefab, _container);
            instance.SetUp(action.Description);
            _items.Add(action.Id, instance);
        }

        public void MarkItemAsDone(int id) => _items[id].SetState(true);
    }
}