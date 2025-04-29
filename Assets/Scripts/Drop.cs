using System;
using UnityEngine;

[RequireComponent(typeof(Rigidbody))]
public class Drop : MonoBehaviour
{
    public event Action PoolGet;
    public event Action PoolRelease;

    private Rigidbody _rigidbody;

    private void Awake()
    {
        _rigidbody = GetComponent<Rigidbody>();
    }

    public void OnPoolRelease()
    {
        PoolRelease?.Invoke();
    }

    public void OnPoolGet()
    {
        _rigidbody.linearVelocity = Vector3.zero;
        PoolGet?.Invoke();
    }

    public void Destroy()
    {
        Destroy(gameObject);
    }
}