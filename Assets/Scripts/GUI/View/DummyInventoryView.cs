using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class DummyInventoryView : MonoBehaviour
{
    [SerializeField] private GameObject _keyIcon;
    public UnityEvent OnKeyInserted;

    public void AddKey()
    {
        _keyIcon.SetActive(true);
    }

    public void InsertKey()
    {
        _keyIcon.SetActive(false);
        OnKeyInserted?.Invoke();
    }
}
