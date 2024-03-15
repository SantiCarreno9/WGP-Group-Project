using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;

namespace Character
{
    public class PlayerAndroidController : MonoBehaviour
    {
        [Header("movement componets - Essentials")]
        [SerializeField] private MovementController _movementController;
        [SerializeField] private AttackController _attackController;

        [Header("Android Stuff")]
        [SerializeField] private Joystick _joystick;
        [SerializeField] private Joystick _joystick1;

        private void FixedUpdate()
        {
            // using the script already done
            _movementController.SetUserMovementInput(_joystick.Direction + _joystick1.Direction);
        }

    }

}