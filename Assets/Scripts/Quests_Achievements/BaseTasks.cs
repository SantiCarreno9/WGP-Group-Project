using QuestAchievements.UI;
using UnityEngine;

namespace QuestAchievements
{
    public class BaseTasks : MonoBehaviour
    {
        [SerializeField] protected ToDoList tasksContainer;
        protected int currentActionsCount = 0;

        void Start()
        {
            AddActions();
        }

        protected virtual void AddActions()
        {
        }

        protected virtual void AddAction(ToDoAction action)
        {
            action.Id = currentActionsCount++;
            tasksContainer.AddItem(action);
        }

    }
}