using System;

namespace Spawners
{
    public interface ISpawnable<out T>
    {
        event Action<T> Dying;

        void Enable();
        void Disable();
        void Destroy();
    }
}