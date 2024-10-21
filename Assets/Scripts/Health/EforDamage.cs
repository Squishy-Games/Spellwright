using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class EforDamage : MonoBehaviour
{
    [SerializeField] private UnityEvent takeDamage;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            takeDamage.Invoke();
        }
    }
}
