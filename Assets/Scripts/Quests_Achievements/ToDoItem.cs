using TMPro;
using UnityEngine;

namespace QuestAchievements.UI
{
    public class ToDoItem : MonoBehaviour
    {
        [SerializeField] private TMP_Text _text;
        [SerializeField] private GameObject _check;       

        public void SetUp(string text, bool status = false)
        {
            _text.text = text;
            _check.SetActive(status);            
        }

        public void SetState(bool done) => _check.SetActive(done);
    }
}