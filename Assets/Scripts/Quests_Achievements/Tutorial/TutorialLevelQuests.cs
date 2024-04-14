using Puzzle;
using UnityEngine;

namespace QuestAchievements
{
    public class TutorialLevelQuests : BaseTasks
    {
        [SerializeField] private Enemy _enemy;
        [SerializeField] private DoorPuzzle _doorPuzzle;

        private ToDoAction _enemyAction;
        private ToDoAction _keyAction;
        private ToDoAction _puzzleAction;

        void Start()
        {
            GameManager.Instance.Player.SceneInteractionsController.OnItemCollected += OnPlayerCollectedItem;
            _doorPuzzle.OnSolved.AddListener(() => tasksContainer.MarkItemAsDone(_puzzleAction.Id));
            _enemy.OnDied += (enemy) => tasksContainer.MarkItemAsDone(_enemyAction.Id);
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