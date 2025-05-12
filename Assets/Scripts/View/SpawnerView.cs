using Attributes;
using Spawners;
using TMPro;
using UnityEngine;

namespace View
{
    public class SpawnerView : MonoBehaviour
    {
        [SerializeField] [Restrict(typeof(ISpawner))]
        private MonoBehaviour _spawner;

        [SerializeField] private TextMeshProUGUI _textMesh;
        [SerializeField] private string _name;

        private ISpawner Spawner => _spawner as ISpawner;

        private void Update()
        {
            _textMesh.text =
                $"{_name}:\nTotal:{Spawner.TotalCount}\nAlive:{Spawner.AliveCount}\nCreated:{Spawner.SpawnedCount}";
        }
    }
}