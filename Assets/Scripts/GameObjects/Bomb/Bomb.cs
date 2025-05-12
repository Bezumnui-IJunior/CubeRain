using System;
using Misc;
using Misc.interfaces;
using Spawners;
using UnityEngine;
using Random = UnityEngine.Random;

namespace GameObjects.Bomb
{
    public class Bomb : MonoBehaviour, ICoroutineExecutor, ISpawnable<Bomb>
    {
        [SerializeField] private float _minTime;
        [SerializeField] private float _maxTime;
        [SerializeField] private float _force;
        [SerializeField] private float _radius;
        [SerializeField] private int _maxAffected = 200;

        private AlphaChanger _alphaChanger;
        private CooldownTimer _destructTimer;
        private MeshRenderer _meshRenderer;
        private OverlapSphere<Rigidbody> _overlapSphere;

        public event Action<Bomb> Dying;

        private void Awake()
        {
            float destructTime = Random.Range(_minTime, _maxTime);
            _meshRenderer = GetComponent<MeshRenderer>();

            _overlapSphere = new OverlapSphere<Rigidbody>(_radius, transform, _maxAffected);
            _alphaChanger = new AlphaChanger(this, destructTime, _meshRenderer.material);
            _destructTimer = new CooldownTimer(this, destructTime);
        }

        private void OnEnable()
        {
            _destructTimer.Freed += Explode;
        }

        private void OnDisable()
        {
            _destructTimer.Freed -= Explode;
        }

        public void Enable()
        {
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

        [ContextMenu("Start timer")]
        public void StartExplodeTimer()
        {
            _alphaChanger.Start();
            _destructTimer.Start();
        }

        private void Explode()
        {
            foreach (Rigidbody nearby in _overlapSphere.GetNearbyComponents())
                nearby.AddExplosionForce(_force, transform.position, _radius, 0.5f, ForceMode.Impulse);

            Die();
        }

        private void Die()
        {
            Dying?.Invoke(this);
        }
    }
}