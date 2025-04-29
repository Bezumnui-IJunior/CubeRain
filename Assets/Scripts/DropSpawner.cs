using UnityEngine;
using UnityEngine.Pool;
using Random = UnityEngine.Random;

public class DropSpawner : MonoBehaviour, IReleasable
{
    [SerializeField] private Drop _dropPrefab;
    [SerializeField] private Vector3 _maxSpawnOffset;
    [SerializeField] private int _poolSize;
    [SerializeField] private float _spawnChance = 0.01f;
    [SerializeField] private bool _isEnable;

    private ObjectPool<Drop> _pool;

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
    }

    private void FixedUpdate()
    {
        if (_isEnable)
            SpawnWithChance();
    }

    private Drop CreateDrop()
    {
        Drop drop = Instantiate(_dropPrefab);

        if (drop.TryGetComponent(out DropDestructor destructor))
            destructor.Init(this);

        return drop;
    }

    private void OnGet(Drop drop)
    {
        drop.OnPoolGet();
        drop.gameObject.SetActive(true);
    }

    private void OnRelease(Drop drop)
    {
        drop.OnPoolRelease();
        drop.gameObject.SetActive(false);
    }

    [ContextMenu("Spawn")]
    private void Spawn()
    {
        SpawnDrop(transform.position);
    }

    private void SpawnWithChance()
    {
        if (_pool.CountActive >= _poolSize)
            return;

        if (_spawnChance > Random.value)
            SpawnDrop(transform.position);
    }

    private void SpawnDrop(Vector3 position)
    {
        _pool.Get(out Drop drop);
        float xOffset = Random.Range(-_maxSpawnOffset.x, _maxSpawnOffset.x);
        float yOffset = Random.Range(-_maxSpawnOffset.y, _maxSpawnOffset.y);
        float zOffset = Random.Range(-_maxSpawnOffset.z, _maxSpawnOffset.z);

        drop.transform.position = position + new Vector3(xOffset, yOffset, zOffset);
    }

    public void Release(Drop drop)
    {
        _pool.Release(drop);
    }
}