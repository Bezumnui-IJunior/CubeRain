using System;
using System.Collections;
using Spawners;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameObjects
{
    [RequireComponent(typeof(Rigidbody))]
    [RequireComponent(typeof(ColorChanger))]
    public class Drop : MonoBehaviour, ISpawnable<Drop>
    {
        [SerializeField] private float _minLifetime = 2;
        [SerializeField] private float _maxLifetime = 5;
        private bool _isDie;

        private Rigidbody _rigidbody;
        public ColorChanger ColorChanger { get; private set; }

        public event Action<Drop> Dying;

        private void Awake()
        {
            _rigidbody = GetComponent<Rigidbody>();
            ColorChanger = GetComponent<ColorChanger>();
        }

        public void Enable()
        {
            _rigidbody.linearVelocity = Vector3.zero;
            ColorChanger.Reset();
            _isDie = false;

            gameObject.SetActive(true);
        }

        public void Disable()
        {
            gameObject.SetActive(false);
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
}