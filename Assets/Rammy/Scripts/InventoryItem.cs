using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class InventoryItem : MonoBehaviour
{
    [SerializeField] GameObject itemDisplay;
    [SerializeField] TextMeshProUGUI count;
    public int Count { get; private set; }

    public void Increment()
    {
        itemDisplay.SetActive(true);
        Count++;
        count.text = Count.ToString();
    }
}
