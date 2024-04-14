namespace QuestAchievements
{
    public class SecondLevelQuests : BaseTasks
    {                
        private ToDoAction _firstKeyAction;
        private ToDoAction _secondKeyAction;
        private ToDoAction _completeLevelAction;

        private byte _keysCollected = 0;       

        void Start()
        {
            GameManager.Instance.Player.SceneInteractionsController.OnItemCollected += OnPlayerCollectedItem;
            GameManager.Instance.OnLevelFinished += () => tasksContainer.MarkItemAsDone(_completeLevelAction.Id);
            
            AddActions();
        }

        protected override void AddActions()
        {
            _firstKeyAction = new ToDoAction("Find the key to open the door");
            AddAction(_firstKeyAction);
            _secondKeyAction = new ToDoAction("Find the key for the exit");
            AddAction(_secondKeyAction);
            _completeLevelAction = new ToDoAction("Escape from the facility.");
            AddAction(_completeLevelAction);
        }

        private void OnPlayerCollectedItem(Collectable item)
        {
            if (item.Category == CollectableCategory.Key)
            {
                if (_keysCollected == 0)
                    tasksContainer.MarkItemAsDone(_firstKeyAction.Id);
                else tasksContainer.MarkItemAsDone(_secondKeyAction.Id);
                _keysCollected++;
            }

        }
    }
}