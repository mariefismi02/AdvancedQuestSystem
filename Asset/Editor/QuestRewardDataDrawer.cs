#if UNITY_EDITOR
using System;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace mariefismi02.Quest.Asset.Editor
{

    [CustomPropertyDrawer(typeof(QuestRewardData<>), true)]
    public class QuestRewardDataDrawer : PropertyDrawer
    {
        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            var fieldType = fieldInfo.FieldType;
            Type innerType = fieldType.IsGenericType
                ? fieldType.GetGenericArguments()[0]
                : fieldType;

            // kalau innerType-nya sendiri generic (QuestRewardData<Player>), ambil parameternya
            Type genericParam = innerType.IsGenericType
                ? innerType.GetGenericArguments()[0]
                : innerType;

            // get all reward types with correct generic parameter
            var types = QuestEditorCache.RewardTypesByOwner[genericParam];

            Type currentType = property.managedReferenceValue?.GetType();
            string[] typeNames = types.Select(t => t.Name).ToArray();
            int index = currentType != null ? types.IndexOf(currentType) : -1;

            Rect dropdownRect = new Rect(position.x, position.y, position.width, EditorGUIUtility.singleLineHeight);

            int newIndex = EditorGUI.Popup(dropdownRect, "Reward Type", index, typeNames);

            if (newIndex != index)
            {
                property.managedReferenceValue = Activator.CreateInstance(types[newIndex]);
            }

            if (property.managedReferenceValue != null)
            {
                Rect fieldRect = new Rect(position.x, position.y + EditorGUIUtility.singleLineHeight + 2,
                    position.width, EditorGUI.GetPropertyHeight(property, true));
                EditorGUI.PropertyField(fieldRect, property, GUIContent.none, true);
            }

            EditorGUI.EndProperty();
        }

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            float height = EditorGUIUtility.singleLineHeight + 4;
            if (property.managedReferenceValue != null)
                height += EditorGUI.GetPropertyHeight(property, true);
            return height;
        }
    }
}
#endif