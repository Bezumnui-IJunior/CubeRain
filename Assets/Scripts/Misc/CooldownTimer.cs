using System;
using System.Collections;
using Misc.interfaces;
using UnityEngine;

namespace Misc
{
    public class CooldownTimer
    {
        private readonly WaitForSeconds _delay;
        private readonly ICoroutineExecutor _user;

        private Coroutine _coroutine;

        public CooldownTimer(ICoroutineExecutor user, float delaySeconds)
        {
            _delay = new WaitForSeconds(delaySeconds);
            IsFree = true;
            _user = user;
        }

        public bool IsFree { get; private set; }
        public event Action Freed;

        public void Restart()
        {
            Stop();
            Start();
        }

        public void Start()
        {
            if (IsFree == false)
                return;

            _coroutine = _user.StartCoroutine(WaitingDelay());
        }

        public void Stop()
        {
            if (IsFree)
                return;

            IsFree = true;

            if (_coroutine != null)
                _user.StopCoroutine(_coroutine);
        }

        private IEnumerator WaitingDelay()
        {
            IsFree = false;

            yield return _delay;

            IsFree = true;
            Freed?.Invoke();
        }
    }
}