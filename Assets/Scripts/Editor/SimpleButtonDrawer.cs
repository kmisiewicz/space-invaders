using System.Reflection;
using UnityEngine;
using UnityEditor;

namespace KM.Utility
{
    [CustomPropertyDrawer(typeof(SimpleButtonAttribute))]
    public class SimpleButtonDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            if (property.propertyType == SerializedPropertyType.Boolean)
            {
                string methodName = (attribute as SimpleButtonAttribute).MethodName;
                Object target = property.serializedObject.targetObject;
                System.Type type = target.GetType();
                MethodInfo method = type.GetMethod(methodName);
                if (method == null)
                {
                    EditorGUI.LabelField(position, label.text, "Method not found.");
                    return;
                }
                if (method.GetParameters().Length > 0)
                {
                    EditorGUI.LabelField(position, label.text, "Method can't have parameters.");
                    return;
                }
                if (GUI.Button(position, method.Name))
                {
                    method.Invoke(target, null);
                }
            }
            else
                EditorGUI.LabelField(position, label.text, "Use SimpleButton property on a boolean.");
        }
    }
}