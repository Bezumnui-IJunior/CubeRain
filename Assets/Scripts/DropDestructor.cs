using UnityEngine;

[RequireComponent(typeof(Drop))]
[RequireComponent(typeof(Collider))]
public class DropDestructor : MonoBehaviour
{
    [SerializeField] private DropSpawner _dropSpawner;
    [SerializeField] private float _lifeTime = 2f;

    private Drop _drop;
    private Coroutine _coroutine;

    private void Awake()
    {
        _drop = GetComponent<Drop>();
    }

    private void OnCollisionEnter(Collision _)
    {
        if (_coroutine != null)
            StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(_dropSpawner.ReleaseWithDelay(_drop, _lifeTime));
    }
}