using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HazardDamage : MonoBehaviour
{
    private IHealth playerHealth;

    private void OntriggerEnter(Collider other)
    {
        if (other.CompareTag("Player")) { playerHealth = other.gameObject.GetComponentInChildren<IHealth>();
            playerHealth.Damage(50);
        }
        
    }
}
