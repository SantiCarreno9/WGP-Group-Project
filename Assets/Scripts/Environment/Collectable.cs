using UnityEngine;

public enum CollectableCategory
{
    Key,
    Syringe,
    Bottle
}

public class Collectable : MonoBehaviour
{
    [SerializeField] private CollectableCategory _category;
    public CollectableCategory Category => _category;
}
