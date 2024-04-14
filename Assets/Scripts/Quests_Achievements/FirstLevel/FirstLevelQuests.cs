using Character;
using UnityEngine;

namespace QuestAchievements
{
    public class FirstLevelQuests : BaseTasks
    {
        private ToDoAction _keyAction;
        private ToDoAction _completeLevelAction;

        void Start()
        {
            GameManager.Instance.Player.SceneInteractionsController.OnItemCollected += OnPlayerCollectedItem;
            GameManager.Instance.OnLevelFinished += () => tasksContainer.MarkItemAsDone(_completeLevelAction.Id);
            AddActions();
        }

        protected override void AddActions()
        {
            _keyAction = new ToDoAction("Find the key.");
            AddAction(_keyAction);
            _completeLevelAction = new ToDoAction("Reach the elevator.");
            AddAction(_completeLevelAction);
        }

        private void OnPlayerCollectedItem(Collectable item)
        {
            if (item.Category == CollectableCategory.Key)
                tasksContainer.MarkItemAsDone(_keyAction.Id);
        }
    }
}
