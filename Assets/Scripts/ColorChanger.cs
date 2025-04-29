using System;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Renderer))]
[RequireComponent(typeof(Collider))]
[RequireComponent(typeof(Drop))]
public class ColorChanger : MonoBehaviour
{
    private Renderer _renderer;
    private GameObject _objectHit;
    private Drop _drop;

    private void Awake()
    {
        _renderer = GetComponent<Renderer>();
        _drop = GetComponent<Drop>();
    }

    public void OnEnable()
    {
        _drop.PoolRelease += OnPoolRelease;
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_objectHit == other.gameObject)
            return;

        _objectHit = other.gameObject;
        _renderer.material.color = Random.ColorHSV();
    }

    private void OnDisable()
    {
        _drop.PoolRelease -= OnPoolRelease;
    }

    private void OnPoolRelease()
    {
        _objectHit = null;
    }
}