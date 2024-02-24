using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorFunction : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;

    private void OnTriggerEnter(Collider collider)
    {
        if (collider.CompareTag("Player")){
            doorAnimator.SetBool("Open", true);
        }
    }

}
