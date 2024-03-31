using UnityEngine;

public class DoorPuzzle : MonoBehaviour, IPuzzle
{
    [SerializeField] private TriggerArea _puzzleObstacle;
    [SerializeField] private TriggerArea _doorArea;

    private void Start()
    {
        SetUp();
    }

    public void SetUp()
    {        
        _puzzleObstacle.gameObject.SetActive(true);
        _doorArea.enabled = false;
    }

    public void Solve()
    {
        _puzzleObstacle.gameObject.SetActive(false);
        _doorArea.enabled = true;
    }

}
