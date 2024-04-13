using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestAchievements
{
    public class TutorialLevelQuests : BaseTasks
    {
        [SerializeField] private TriggerArea _elevatorTrigger;
        
        private ToDoAction _enemyAction;
        private ToDoAction _keyAction;        
        private ToDoAction _puzzleAction;

        void Start()
        {
            GameManager.Instance.Player.SceneInteractionsController.OnItemCollected += OnPlayerCollectedItem;                        
            AddActions();
        }

        protected override void AddActions()
        {
            _enemyAction = new ToDoAction("Kill the enemy.");
            AddAction(_enemyAction);
            _keyAction = new ToDoAction("Pick up the key to open the elevator.");
            AddAction(_keyAction);
            _puzzleAction = new ToDoAction("Solve the door puzzle");
            AddAction(_puzzleAction);
        }

        private void OnPlayerCollectedItem(Collectable item)
        {
            if (item.Category == CollectableCategory.Key)
                tasksContainer.MarkItemAsDone(_keyAction.Id);
        }
    }
}