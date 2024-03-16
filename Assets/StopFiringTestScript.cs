using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Character
{
    public class StopFiringTestScript : MonoBehaviour
    {
        [SerializeField] private AttackController _attackController;
        public void StopFiring()
        {
            _attackController.StopAttacking();
        }

    }
}

