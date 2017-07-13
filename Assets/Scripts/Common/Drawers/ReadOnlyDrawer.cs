//UNity

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

using Common.Attributes;

namespace Common.Drawers
{
    [CustomPropertyDrawer(typeof(ReadOnlyCustomAttribute))]
    public class ReadOnlyDrawer : PropertyDrawer
    {
        public override float GetPropertyHeight(SerializedProperty property,
                                                GUIContent label)
        {
            return EditorGUI.GetPropertyHeight(property, label, true);
        }

        public override void OnGUI(Rect position,
                                   SerializedProperty property,
                                   GUIContent label)
        {
            GUI.enabled = false;
            EditorGUI.PropertyField(position, property, label, true);
            GUI.enabled = true;
        }
    }
}

#endif
