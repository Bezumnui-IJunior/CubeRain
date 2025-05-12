using GameObjects;
using UnityEngine;

public class InfectionTrigger : MonoBehaviour
{
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.TryGetComponent(out Drop drop) == false)
            return;

        drop.ColorChanger.RandomizeColor(this);
        StartCoroutine(drop.DieWithRandomDelay());
    }
}