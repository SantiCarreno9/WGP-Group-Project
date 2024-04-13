using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace QuestAchievements
{
    public class TutorialLevelAchievements : BaseTasks
    {
        [SerializeField] private TriggerArea _elevatorTrigger;

        private ToDoAction _completeLevelAction;        

        void Start()
        {            
            AddActions();
        }

        protected override void AddActions()
        {
            _completeLevelAction = new ToDoAction("Complete the tutorial.");
            AddAction(_completeLevelAction);            
        }        
    }
}