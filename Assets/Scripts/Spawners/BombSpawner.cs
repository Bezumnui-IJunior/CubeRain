using GameObjects.Bomb;
using UnityEngine;

namespace Spawners
{
    public class BombSpawner : Spawner<Bomb>
    {
        public void SpawnWithExplodeTimer(Vector3 position)
        {
            if (IsOverflow())
                return;

            Bomb bomb = InstantiateObject();
            bomb.transform.position = position;
            bomb.StartExplodeTimer();
        }
    }
}