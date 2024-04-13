using Character;
using System.Collections.Generic;
using UnityEngine;

namespace QuestAchievements
{
    public class SecondLevelAchievements : BaseTasks
    {
        [SerializeField] private PlayerController _playerController;
        [SerializeField] private Enemy[] _enemies;

        private byte _deadEnemiesCount = 0;
        private byte _collectedItemsCount = 0;

        private ToDoAction _enemiesAction;
        private ToDoAction _damageAction;
        private ToDoAction _itemsAction;
        private ToDoAction _locksmithAction;


        void Start()
        {

            GameManager.Instance.Player.SceneInteractionsController.OnItemCollected += OnPlayerCollectedItem;
            for (int i = 0; i < _enemies.Length; i++)
                _enemies[i].OnDied += OnEnemyDied;
            GameManager.Instance.OnLevelFinished += () =>
            {
                if (GameManager.Instance.Player.HealthModule.HasMaxHealth())
                    tasksContainer.MarkItemAsDone(_damageAction.Id);
            };
            AddActions();
        }

        protected override void AddActions()
        {
            _enemiesAction = new ToDoAction("Kill all the enemies.");
            AddAction(_enemiesAction);
            _itemsAction = new ToDoAction("Collect the five syringes.");
            AddAction(_itemsAction);
            _damageAction = new ToDoAction("Finish the level with no damage.");
            AddAction(_damageAction);
            _locksmithAction = new ToDoAction("Locksmith");
            AddAction(_locksmithAction);
        }

        private void OnPlayerCollectedItem(Collectable item)
        {
            //if (item.Category == CollectableCategory.)
            //    tasksContainer.MarkItemAsDone(_itemsAction.Id);
            _collectedItemsCount++;
            if (_collectedItemsCount == 5)
                tasksContainer.MarkItemAsDone(_itemsAction.Id);
        }

        private void OnEnemyDied(Enemy enemy)
        {
            _deadEnemiesCount++;
            enemy.OnDied -= OnEnemyDied;
            if (_deadEnemiesCount == _enemies.Length)
                tasksContainer.MarkItemAsDone(_enemiesAction.Id);
        }
    }
}