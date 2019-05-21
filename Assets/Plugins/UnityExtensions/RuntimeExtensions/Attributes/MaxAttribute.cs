﻿using System;
using UnityEngine;

#if UNITY_EDITOR
using UnityEditor;
using UnityExtensions.Editor;
#endif

namespace UnityExtensions
{
    /// <summary>
    /// 让 int 或 float 字段的值限制在指定范围
    /// </summary>
    [AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
    public sealed class MaxAttribute : PropertyAttribute
    {
        float _max;

        /// <summary>
        /// 让 int 或 float 字段的值限制在指定范围
        /// </summary>
        public MaxAttribute(float max)
        {
            _max = max;
        }

#if UNITY_EDITOR

        [CustomPropertyDrawer(typeof(MaxAttribute))]
        class MaxDrawer : BasePropertyDrawer<MaxAttribute>
        {
            public override void OnGUI(Rect position, SerializedProperty property, GUIContent label)
            {
                switch (property.propertyType)
                {
                    case SerializedPropertyType.Float:
                        {
                            property.floatValue = Mathf.Min(
                                EditorGUI.FloatField(position, label, property.floatValue),
                                attribute._max);
                            break;
                        }
                    case SerializedPropertyType.Integer:
                        {
                            property.intValue = Mathf.Min(
                                EditorGUI.IntField(position, label, property.intValue),
                                (int)attribute._max);
                            break;
                        }
                    default:
                        {
                            EditorGUI.LabelField(position, label.text, "Use Max with float or int.");
                            break;
                        }
                }
            }

        } // class MaxDrawer

#endif

    } // class MaxAttribute

} // namespace UnityExtensions