using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.UI;

public class LevelTransitionController : MonoBehaviour
{
    [SerializeField] private string _nextLevelName;
    [SerializeField] private GameObject _transitionScreen;
    [SerializeField] private Image _blackScreen;

    public void GoToNextLevel()
    {
        _transitionScreen.SetActive(true);
        _blackScreen.DOFade(1, 2).onComplete += () =>
        {
            GameManager.Instance.GoToLevel(_nextLevelName);
        };
    }
}
