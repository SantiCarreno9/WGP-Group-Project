using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorTransit : MonoBehaviour
{
    [SerializeField] Transform placeToSend;
    private void OnCollisionEnter(Collision collision)
    {
        Debug.Log(collision.gameObject.CompareTag("Player"));
    }
}
