using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutoTrigger : MonoBehaviour
{
    [SerializeField] private ElementToHighlight tutoPart;
    [SerializeField] private UIHighlighter uIhiglighter;
    private void OnTriggerEnter(Collider collision)
    {
        if (collision != null && collision.gameObject.CompareTag("Player")) {
            uIhiglighter.HighlightTheThing(tutoPart);
            gameObject.SetActive(false);
        }
    }
}
