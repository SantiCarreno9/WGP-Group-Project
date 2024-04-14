namespace QuestAchievements
{
    public class TutorialLevelAchievements : BaseTasks
    {        
        private ToDoAction _completeLevelAction;        

        void Start()
        {
            GameManager.Instance.OnLevelFinished += () => tasksContainer.MarkItemAsDone(_completeLevelAction.Id);
            AddActions();
        }

        protected override void AddActions()
        {
            _completeLevelAction = new ToDoAction("Complete the tutorial.");
            AddAction(_completeLevelAction);            
        }        
    }
}