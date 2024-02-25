using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ElevatorController : MonoBehaviour
{
    [SerializeField] Animator doorAnimator;    

    public void OpenDoor() => doorAnimator.SetBool("Open", true);

    public void CloseDoor() => doorAnimator.SetBool("Open", false);    
}
