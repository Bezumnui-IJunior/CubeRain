using System.Collections;
using UnityEngine;
using UnityEngine.Pool;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] private Drop _dropPrefab;
    [SerializeField] private int _poolSize;

    private ObjectPool<Drop> _pool;

    private void Awake()
    {
        _pool = new ObjectPool<Drop>(
            () => Instantiate(_dropPrefab),
            OnGet,
            OnRelease,
            drop => drop.Destroy(),
            true,
            _poolSize,
            _poolSize
        );
    }

    private void OnGet(Drop drop)
    {
        drop.gameObject.SetActive(true);
        drop.Rigidbody.linearVelocity = Vector3.zero;
    }

    private void OnRelease(Drop drop)
    {
        drop.gameObject.SetActive(false);
    }

    [ContextMenu("Spawn")]
    private void Spawn()
    {
        SpawnDrop(transform.position);
    }

    public IEnumerator ReleaseWithDelay(Drop drop, float seconds)
    {
        yield return new WaitForSeconds(seconds);

        _pool.Release(drop);
    }

    private void SpawnDrop(Vector3 position)
    {
        _pool.Get(out Drop drop);
        drop.transform.position = position;
    }
}