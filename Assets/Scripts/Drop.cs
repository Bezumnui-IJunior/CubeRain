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
    private bool _isDie;
    public event Action<Drop> Dying;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        _colorChanger = GetComponent<ColorChanger>();
        _isDie = false;
    }

    private void OnCollisionEnter(Collision _)
    {
        if (_isDie)
            return;

        StartCoroutine(DieWithRandomDelay());
    }

    public void Reset()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        _colorChanger.Reset();
        _isDie = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    private IEnumerator DieWithRandomDelay()
    {
        _isDie = true;

        yield return new WaitForSeconds(Random.Range(_minLifetime, _maxLifetime));

        Dying?.Invoke(this);
    }
}