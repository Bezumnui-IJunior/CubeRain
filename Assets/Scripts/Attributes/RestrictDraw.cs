using System;
using Unity.VisualScripting;
using UnityEditor;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Attributes
{
    [CustomPropertyDrawer(typeof(Restrict))]
    public class RequireInterfaceDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (attribute is Restrict propertyAttribute == false)
                return;

            EditorGUI.BeginProperty(position, label, property);

            string textLabel = label.ToString();

            if (position.Contains(Event.current.mousePosition) == false)
                textLabel = $"{propertyAttribute.Type} {label}";

            property.objectReferenceValue = EditorGUI.ObjectField(
                position,
                textLabel,
                property.objectReferenceValue,
                typeof(MonoBehaviour),
                true
            );

            EditorGUI.EndProperty();

            if (property.objectReferenceValue == null)
                return;

            if (TryGetComponent(property.objectReferenceValue, propertyAttribute.Type, out MonoBehaviour component) ==
                false)
            {
                Debug.LogWarning(
                    $"Object {property.objectReferenceValue} does not contain interface {propertyAttribute.Type}.");
                property.objectReferenceValue = null;

                return;
            }

            property.objectReferenceValue = component;
        }

        private bool TryGetComponent(Object gameObject, Type targetInterface, out MonoBehaviour result)
        {
            result = null;

            if (gameObject.IsComponentHolder() == false)
                return false;

            foreach (MonoBehaviour component in gameObject.GetComponents<MonoBehaviour>())
            {
                if (targetInterface.IsAssignableFrom(component) == false)
                    continue;

                result = component;

                return true;
            }

            return false;
        }
    }
}