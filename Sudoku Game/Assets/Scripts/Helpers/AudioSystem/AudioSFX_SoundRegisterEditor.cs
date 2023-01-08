using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

namespace Audio.AudioSFX
{
    [CustomPropertyDrawer(typeof(AudioSFX_SFX))]
    public class AudioSFX_SoundRegisterEditor : PropertyDrawer
    {
        private float _height = 100;
        private float _indent = 50;

        public override float GetPropertyHeight(SerializedProperty property, GUIContent label)
        {
            int totalLine = 1;

            if (property.isExpanded)
                totalLine = 7;

            return EditorGUIUtility.singleLineHeight * totalLine + EditorGUIUtility.standardVerticalSpacing * (totalLine - 1);
        }

        public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
        {
            EditorGUI.BeginProperty(position, label, property);

            // Draw the default property editor
            EditorGUI.PropertyField(position, property, label, true);

            //Check if some values are empty
            if (property.FindPropertyRelative("ID").stringValue == "")
            {
                if (property.FindPropertyRelative("clip").objectReferenceValue != null)
                {
                    property.FindPropertyRelative("ID").stringValue = property.FindPropertyRelative("clip").objectReferenceValue.name;
                }
            }

            if (property.FindPropertyRelative("minRandomPitch").floatValue == 0)
            {
                property.FindPropertyRelative("minRandomPitch").floatValue = 0.7f;
            }

            if (property.FindPropertyRelative("maxRandomPitch").floatValue == 0)
            {
                property.FindPropertyRelative("maxRandomPitch").floatValue = 1.3f;
            }

            if (property.FindPropertyRelative("intensityFactor").floatValue == 0)
            {
                property.FindPropertyRelative("intensityFactor").floatValue = 1;
            }

            if (property.isExpanded)
            {
                position.height = EditorGUIUtility.singleLineHeight;
                position.xMin += EditorGUIUtility.labelWidth;
            }
            else
            {
                Rect rect = position;
                rect.x += 200;
                rect.width -= 200;

                MessageType MT = MessageType.Info;

                string displayMessage = "NULL";

                if (property.FindPropertyRelative("clip") != null)
                {
                    if (property.FindPropertyRelative("clip").objectReferenceValue != null)
                    {
                        displayMessage = property.FindPropertyRelative("clip").objectReferenceValue.name;
                    }

                    else
                    {
                        displayMessage = "No AudioClip assigned";
                        MT = MessageType.Warning;
                    }
                }

                EditorGUI.HelpBox(rect, displayMessage, MT);
            }

            EditorGUI.EndProperty();
        }
    }
}
