using System.Collections;
using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class DropSpawner : MonoBehaviour
{
    [SerializeField] private Drop _dropPrefab;
    [SerializeField] private Vector3 _maxSpawnOffset;
    [SerializeField] private int _poolSize;
    [SerializeField] private float _spawnDelaySeconds = 0.01f;

    private ObjectPool<Drop> _pool;
    private WaitForSecondsRealtime _spawnDelay;
    private Coroutine _coroutine;

    private void Awake()
    {
        _pool = new ObjectPool<Drop>(
            CreateDrop,
            OnGet,
            OnRelease,
            drop => drop.Destroy(),
            true,
            _poolSize,
            _poolSize
        );

        _spawnDelay = new WaitForSecondsRealtime(_spawnDelaySeconds);
    }

    private void OnEnable()
    {
        _coroutine = StartCoroutine(Spawning());
    }

    private void OnDisable()
    {
        if (_coroutine == null)
            return;

        StopCoroutine(_coroutine);
        _coroutine = null;
    }

    public void Release(Drop drop)
    {
        _pool.Release(drop);
    }

    private Drop CreateDrop()
    {
        Drop drop = Instantiate(_dropPrefab);

        return drop;
    }

    private void OnRelease(Drop drop)
    {
        drop.gameObject.SetActive(false);
        drop.Dying -= Release;
    }

    private void OnGet(Drop drop)
    {
        drop.gameObject.SetActive(true);
        drop.Reset();
        drop.Dying += Release;
    }

    [ContextMenu("Spawn")]
    private void Spawn()
    {
        SpawnDrop();
    }

    private IEnumerator Spawning()
    {
        while (enabled)
        {
            yield return _spawnDelay;

            if (_pool.CountActive >= _poolSize)
                continue;

            SpawnDrop();
        }
    }

    private void SpawnDrop()
    {
        _pool.Get(out Drop drop);
        float xOffset = Random.Range(-_maxSpawnOffset.x, _maxSpawnOffset.x);
        float yOffset = Random.Range(-_maxSpawnOffset.y, _maxSpawnOffset.y);
        float zOffset = Random.Range(-_maxSpawnOffset.z, _maxSpawnOffset.z);

        drop.transform.position = transform.position + new Vector3(xOffset, yOffset, zOffset);
    }
}