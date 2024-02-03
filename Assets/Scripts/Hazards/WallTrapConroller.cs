using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallTrapConroller : MonoBehaviour
{
    [SerializeField] private float _timeDelay;
    private Transform[] _children;

    private bool isEnabling = false;

    private void Start()
    {
        // TimeDelay
        _timeDelay = 1.0f;
        // Get all child objects
        _children = new Transform[transform.childCount];
        for (int i = 0; i < transform.childCount; i++)
        {
            _children[i] = transform.GetChild(i);
            _children[i].gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        //Debug.Log($"collider {other} detected");
        if (other.CompareTag("Player") && !isEnabling)
        {
            isEnabling = true;
            StartCoroutine(EnableChildren());
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player") && isEnabling)
        {
            //Debug.Log("playerDetection OUT");
            StopAllCoroutines();
            StartCoroutine(DisableAllChildren());
        }
    }

    private IEnumerator EnableChildren()
    {
        for (int i = 0; i < _children.Length; i++)
        {
            _children[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(_timeDelay);
        }

        yield return new WaitForSeconds(_timeDelay + 4);
        DisableAllChildren();
    }

    private IEnumerator DisableAllChildren()
    {
        for (int i = 0; i < _children.Length; i++)
        {
            _children[i].gameObject.SetActive(false);
            yield return new WaitForSeconds(_timeDelay);
        }

        isEnabling = false;
    }
}
