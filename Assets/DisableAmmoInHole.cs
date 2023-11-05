using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisableAmmoInHole : MonoBehaviour
{
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("ammo"))
        {
            //Debug.Log("Amooooo");
            other.gameObject.SetActive(false);
        }
    }
}
