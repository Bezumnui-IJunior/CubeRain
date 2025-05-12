using System.Collections;
using UnityEngine;

namespace Misc.interfaces
{
    public interface ICoroutineExecutor
    {
        public Coroutine StartCoroutine(IEnumerator enumerator);
        public void StopCoroutine(Coroutine coroutine);
    }
}