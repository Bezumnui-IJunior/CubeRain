using System.Collections;
using GameObjects;
using UnityEngine;
using Random = UnityEngine.Random;

namespace Spawners
{
    public class DropSpawner : Spawner<Drop>
    {
        [SerializeField] private BombSpawner _bombSpawner;
        [SerializeField] private Vector3 _maxSpawnOffset;
        [SerializeField] private float _spawnDelaySeconds = 0.01f;
        private Coroutine _coroutine;

        private WaitForSecondsRealtime _spawnDelay;

        protected override void Awake()
        {
            base.Awake();
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

        protected override void OnRelease(Drop spawnable)
        {
            base.OnRelease(spawnable);
            _bombSpawner.SpawnWithExplodeTimer(spawnable.transform.position);
        }

        private IEnumerator Spawning()
        {
            while (enabled)
            {
                yield return _spawnDelay;

                if (IsOverflow() == false)
                    SpawnDrop();
            }
        }

        private void SpawnDrop()
        {
            Drop drop = InstantiateObject();
            float xOffset = Random.Range(-_maxSpawnOffset.x, _maxSpawnOffset.x);
            float yOffset = Random.Range(-_maxSpawnOffset.y, _maxSpawnOffset.y);
            float zOffset = Random.Range(-_maxSpawnOffset.z, _maxSpawnOffset.z);

            drop.transform.position = transform.position + new Vector3(xOffset, yOffset, zOffset);
        }
    }
}