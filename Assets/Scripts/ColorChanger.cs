using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class ColorChanger : MonoBehaviour
{
    [SerializeField] private Color _initialColor = Color.white;

    private Renderer _renderer;
    private ColorChangeTrigger _objectHit;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    public void RandomizeColor(ColorChangeTrigger trigger)
    {
        if (_objectHit == trigger)
            return;

        _objectHit = trigger;
        _renderer.material.color = Random.ColorHSV();
    }

    public void Reset()
    {
        _objectHit = null;
        _renderer.material.color = _initialColor;
    }
}