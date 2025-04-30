using System;
using UnityEngine;

public class ColorChangeTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out ColorChanger colorChanger) == false)
            return;

        colorChanger.RandomizeColor(this);
    }
}