using UnityEngine;
using UnityEngine.Pool;
using Object = UnityEngine.Object;

namespace Spawners
{
    public abstract class Spawner<T> : MonoBehaviour where T : Object, ISpawnable<T>
    {
        [SerializeField] private T _prefab;
        [SerializeField] private int _poolSize;

        private ObjectPool<T> _pool;

        protected virtual void Awake()
        {
            _pool = new ObjectPool<T>(
                () => Instantiate(_prefab),
                OnGet,
                OnRelease,
                spawnable => spawnable.Destroy(),
                true,
                _poolSize,
                _poolSize
            );
        }

        protected bool IsOverflow() =>
            _pool.CountActive >= _poolSize;

        protected T InstantiateObject()
        {
            _pool.Get(out T o);

            return o;
        }

        protected virtual void OnRelease(T spawnable)
        {
            spawnable.Disable();
            spawnable.Dying -= _pool.Release;
        }

        private void OnGet(T spawnable)
        {
            spawnable.Enable();
            spawnable.Dying += _pool.Release;
        }
    }
}