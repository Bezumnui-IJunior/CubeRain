using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;

    private GameObject _objectHit;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_objectHit == other.gameObject)
            return;

        _objectHit = other.gameObject;
        _renderer.material.color = Random.ColorHSV();
    }
}