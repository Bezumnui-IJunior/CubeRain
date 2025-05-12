using System.Collections.Generic;
using UnityEngine;

namespace Misc
{
    public class OverlapSphere<T> where T : Component
    {
        private readonly Collider[] _nearColliders;
        private readonly List<T> _nearComponents;
        private readonly float _radius;
        private readonly Transform _transform;

        public OverlapSphere(float radius, Transform transform, int maxCount)
        {
            _radius = radius;
            _transform = transform;
            _nearColliders = new Collider[maxCount];
            _nearComponents = new List<T>(maxCount);
        }

        public IEnumerable<T> GetNearbyComponents()
        {
            _nearComponents.Clear();
            int size = Physics.OverlapSphereNonAlloc(_transform.position, _radius, _nearColliders);

            for (int i = 0; i < size; i++)
            {
                if (_nearColliders[i].TryGetComponent(out T component))
                    _nearComponents.Add(component);
            }

            return _nearComponents;
        }
    }
}