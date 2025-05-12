using System;
using UnityEngine;

namespace Attributes
{
    public class Restrict : PropertyAttribute
    {
        public Restrict(Type type)
        {
            Type = type;
        }

        public Type Type { get; private set; }
    }
}