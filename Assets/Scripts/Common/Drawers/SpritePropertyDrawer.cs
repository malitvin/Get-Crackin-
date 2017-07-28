#if UNITY_EDITOR

//Unity
using UnityEngine;
using UnityEditor;

//C#
using Common.UI;

[CustomPropertyDrawer(typeof(Sprite))]
public class SpritePropertyDrawer : PropertyDrawer
{
    public override float GetPropertyHeight(SerializedProperty prop, GUIContent label)
    {
        if (prop.objectReferenceValue != null)
        {
            return maxTextureSizeY;
        }
        else
        {
            return base.GetPropertyHeight(prop, label);
        }
    }

    private const float maxTextureSizeX = 50;
    private const float maxTextureSizeY = 50;


    public override void OnGUI(Rect position, SerializedProperty prop, GUIContent label)
    {
        EditorGUI.BeginProperty(position, label, prop);

        if (prop.objectReferenceValue != null)
        {
            position.width = EditorGUIUtility.labelWidth;
            GUI.Label(position, prop.displayName);

            Vector2 spriteAspect = new Vector2(maxTextureSizeX,maxTextureSizeY);
            position.x += position.width;
            position.width =spriteAspect.x;
            position.height = spriteAspect.y;

            prop.objectReferenceValue = EditorGUI.ObjectField(position, prop.objectReferenceValue, typeof(Sprite), false);
        }
        else
        {
            EditorGUI.PropertyField(position, prop, true);
        }

        EditorGUI.EndProperty();
    }
}

#endif
