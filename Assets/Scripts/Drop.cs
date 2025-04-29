using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
public class Drop : MonoBehaviour
{
    [SerializeField] private float _minLifetime = 2;
    [SerializeField] private float _maxLifetime = 5;

    private Rigidbody _rigidbody;
    private ColorChanger _colorChanger;
    public event Action<Drop> Dying;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
    }

    private void OnCollisionEnter(Collision other)
    {
        _colorChanger.RandomizeColor(other);
        StartCoroutine(ReleaseWithRandomDelay());
    }

    public void Reset()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _colorChanger.Reset();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator ReleaseWithRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minLifetime, _maxLifetime));

        Dying?.Invoke(this);
    }
}