using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class LayerManager : MonoBehaviour
{
    [SerializeField] private string[] layers = { "Default", "noColl" };

    public event EventHandler<GameObject> onObjectSwallowed;

    private void OnTriggerEnter(Collider other)
    {
        onObjectSwallowed?.Invoke(this, other.gameObject);
        ChangeLayer(other, 1); // no coll layer
    }

    private void OnTriggerExit(Collider other)
    {
        ChangeLayer(other, 0); // Default layer
    }

    private void ChangeLayer(Collider other, int index)
    {
        other.gameObject.layer = LayerMask.NameToLayer(layers[index]);
    }
}

