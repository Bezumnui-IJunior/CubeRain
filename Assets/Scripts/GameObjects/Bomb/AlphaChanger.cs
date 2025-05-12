using System.Collections;
using Misc.interfaces;
using UnityEngine;

namespace GameObjects.Bomb
{
    public class AlphaChanger
    {
        private readonly float _destructTime;
        private readonly ICoroutineExecutor _executor;
        private readonly Material _material;

        public AlphaChanger(ICoroutineExecutor executor, float destructTime, Material material)
        {
            _destructTime = destructTime;
            _material = material;
            _executor = executor;
        }

        public void Start()
        {
            _executor.StartCoroutine(ChangingAlpha());
        }

        private IEnumerator ChangingAlpha()
        {
            float alpha = 1;

            while (alpha > 0)
            {
                alpha -= Time.deltaTime / _destructTime;
                ChangeAlpha(alpha);

                yield return null;
            }
        }

        private void ChangeAlpha(float alpha)
        {
            Color color = _material.color;
            color.a = alpha;
            _material.color = color;
        }
    }
}