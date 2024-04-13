using Character;
using UnityEngine;

namespace QuestAchievements
{
    public class SecondLevelQuests : BaseTasks
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private TriggerArea _doorTrigger;
        [SerializeField] private TriggerArea _elevatorTrigger;

        private ToDoAction _firstKeyAction;
        private ToDoAction _secondKeyAction;
        private ToDoAction _completeLevelAction;

        private byte _keysCollected = 0;

        protected virtual void OnEnable()
        {
            _playerController.SceneInteractionsController.OnItemCollected += OnPlayerCollectedItem;
            _elevatorTrigger.OnAreaEnter.AddListener(() => tasksContainer.MarkItemAsDone(_completeLevelAction.Id));
        }

        protected virtual void OnDisable()
        {
            _playerController.SceneInteractionsController.OnItemCollected -= OnPlayerCollectedItem;
        }


        void Start()
        {
            AddActions();
        }

        protected override void AddActions()
        {
            _firstKeyAction = new ToDoAction("Find the key to open the door");
            AddAction(_firstKeyAction);
            _secondKeyAction = new ToDoAction("Find the key for the exit");
            AddAction(_secondKeyAction);
            _completeLevelAction = new ToDoAction("Reach the elevator.");
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