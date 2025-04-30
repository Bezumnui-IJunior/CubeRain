using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Rigidbody))]
[RequireComponent(typeof(ColorChanger))]
public class Drop : MonoBehaviour
{
    public ColorChanger ColorChanger { get; private set; }

    [SerializeField] private float _minLifetime = 2;
    [SerializeField] private float _maxLifetime = 5;

    private Rigidbody _rigidbody;
    private bool _isDie;
    public event Action<Drop> Dying;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
        ColorChanger = GetComponent<ColorChanger>();
        _isDie = false;
    }

    public void Reset()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        ColorChanger.Reset();
        _isDie = false;
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }

    public IEnumerator DieWithRandomDelay()
    {
        if (_isDie)
            yield break;

        _isDie = true;

        yield return new WaitForSeconds(Random.Range(_minLifetime, _maxLifetime));

        Dying?.Invoke(this);
    }
}