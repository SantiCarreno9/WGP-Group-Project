using UnityEngine;

public enum CollectableCategory
{
    Key
}

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableCategory _category;
    public CollectableCategory Category => _category;
}
