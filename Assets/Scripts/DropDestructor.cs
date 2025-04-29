using System;
using System.Collections;
using UnityEngine;
using Random = UnityEngine.Random;

[RequireComponent(typeof(Drop))]
[RequireComponent(typeof(Collider))]
public class DropDestructor : MonoBehaviour
{
    [SerializeField] private float _minDamagedLifetime = 2f;
    [SerializeField] private float _maxDamagedLifetime = 5f;
    [SerializeField] private float _maxLifetime = 20f;

    private WaitForSeconds _lifetimeDelay;
    private IReleasable _releasable;
    private Drop _drop;
    private Coroutine _coroutine;
    private bool _isInit;

    private void Awake()
    {
        _drop = GetComponent<Drop>();
        _lifetimeDelay = new WaitForSeconds(_maxLifetime);
    }

    private void OnEnable()
    {
        _drop.PoolGet += OnPoolGet;
        _drop.PoolRelease += OnPoolRelease;
    }

    private void OnCollisionEnter(Collision _)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(ReleaseWithRandomDelay());
    }

    private void OnDisable()
    {
        _drop.PoolGet -= OnPoolGet;
        _drop.PoolRelease -= OnPoolRelease;
    }

    public void Init(IReleasable releasable)
    {
        if (_isInit)
            return;

        _isInit = true;
        _releasable = releasable;
    }

    private void OnPoolRelease()
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
    }

    private void OnPoolGet()
    {
        _coroutine = StartCoroutine(ReleaseOvertime());
    }

    private IEnumerator ReleaseWithRandomDelay()
    {
        yield return new WaitForSeconds(Random.Range(_minDamagedLifetime, _maxDamagedLifetime));

        _releasable.Release(_drop);
    }

    private IEnumerator ReleaseOvertime()
    {
        yield return _lifetimeDelay;

        _releasable.Release(_drop);
    }
}