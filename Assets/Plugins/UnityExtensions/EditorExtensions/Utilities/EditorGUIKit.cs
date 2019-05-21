﻿#if UNITY_EDITOR

using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

namespace UnityExtensions.Editor
{
    /// <summary>
    /// 菜单项状态
    /// </summary>
    public enum MenuItemState
    {
        Normal,
        Selected,
        Disabled,
    }


    /// <summary>
    /// 编辑器 UI 工具箱
    /// </summary>
    public partial struct EditorGUIKit
    {
        static GUIContent _tempContent = new GUIContent();

        static GUIStyle _buttonStyle;
        static GUIStyle _buttonLeftStyle;
        static GUIStyle _buttonMiddleStyle;
        static GUIStyle _buttonRightStyle;
        static GUIStyle _middleCenterLabelStyle;
        static GUIStyle _middleLeftLabelStyle;

        static Texture2D _paneOptionsIconDark;
        static Texture2D _paneOptionsIconLight;


        public static GUIStyle buttonStyle
        {
            get
            {
                if (_buttonStyle == null) _buttonStyle = "Button";
                return _buttonStyle;
            }
        }


        /// <summary>
        /// 左侧按钮 GUIStyle
        /// </summary>
        public static GUIStyle buttonLeftStyle
        {
            get
            {
                if (_buttonLeftStyle == null) _buttonLeftStyle = "ButtonLeft";
                return _buttonLeftStyle;
            }
        }


        /// <summary>
        /// 中部按钮 GUIStyle
        /// </summary>
        public static GUIStyle buttonMiddleStyle
        {
            get
            {
                if (_buttonMiddleStyle == null) _buttonMiddleStyle = "ButtonMid";
                return _buttonMiddleStyle;
            }
        }


        /// <summary>
        /// 右侧按钮 GUIStyle
        /// </summary>
        public static GUIStyle buttonRightStyle
        {
            get
            {
                if (_buttonRightStyle == null) _buttonRightStyle = "ButtonRight";
                return _buttonRightStyle;
            }
        }


        public static GUIStyle middleCenterLabelStyle
        {
            get
            {
                if (_middleCenterLabelStyle == null)
                {
                    _middleCenterLabelStyle = new GUIStyle(EditorStyles.label);
                    _middleCenterLabelStyle.alignment = TextAnchor.MiddleCenter;
                }
                return _middleCenterLabelStyle;
            }
        }


        public static GUIStyle middleLeftLabelStyle
        {
            get
            {
                if (_middleLeftLabelStyle == null)
                {
                    _middleLeftLabelStyle = new GUIStyle(EditorStyles.label);
                    _middleLeftLabelStyle.alignment = TextAnchor.MiddleLeft;
                }
                return _middleLeftLabelStyle;
            }
        }


        public static Texture2D paneOptionsIcon
        {
            get
            {
                return EditorGUIUtility.isProSkin ? paneOptionsIconDark : paneOptionsIconLight;
            }
        }


        public static Texture2D paneOptionsIconDark
        {
            get
            {
                if (_paneOptionsIconDark == null)
                {
                    _paneOptionsIconDark = (Texture2D)EditorGUIUtility.Load("Builtin Skins/DarkSkin/Images/pane options.png");
                }
                return _paneOptionsIconDark;
            }
        }


        public static Texture2D paneOptionsIconLight
        {
            get
            {
                if (_paneOptionsIconLight == null)
                {
                    _paneOptionsIconLight = (Texture2D)EditorGUIUtility.Load("Builtin Skins/LightSkin/Images/pane options.png");
                }
                return _paneOptionsIconLight;
            }
        }


        /// <summary>
        /// 编辑器默认文本颜色
        /// </summary>
        public static Color defaultTextColor
        {
            get { return EditorStyles.label.normal.textColor; }
        }


        /// <summary>
        /// 获取临时的 GUIContent（避免 GC，仅用于临时绘制）
        /// </summary>
        public static GUIContent TempContent(string text = null, Texture image = null, string tooltip = null)
        {
            _tempContent.text = text;
            _tempContent.image = image;
            _tempContent.tooltip = tooltip;

            return _tempContent;
        }


        /// <summary>
        /// 绘制矩形的边框
        /// </summary>
        /// <param name="rect"> 矩形 </param>
        /// <param name="color"> 边框颜色 </param>
        /// <param name="size"> 边框大小 </param>
        public static void DrawWireRect(Rect rect, Color color, float borderSize = 1f)
        {
            Rect draw = new Rect(rect.x, rect.y, rect.width, borderSize);
            EditorGUI.DrawRect(draw, color);
            draw.y = rect.yMax - borderSize;
            EditorGUI.DrawRect(draw, color);
            draw.yMax = draw.yMin;
            draw.yMin = rect.yMin + borderSize;
            draw.width = borderSize;
            EditorGUI.DrawRect(draw, color);
            draw.x = rect.xMax - borderSize;
            EditorGUI.DrawRect(draw, color);
        }


        /// <summary>
        /// 绘制一个单行高且缩进的按钮
        /// </summary>
        public static bool IndentedButton(string text)
        {
            var rect = EditorGUILayout.GetControlRect(true);
            rect.xMin += EditorGUIUtility.labelWidth;
            return GUI.Button(rect, text, EditorStyles.miniButton);
        }


        /// <summary>
        /// 绘制一个单行高且缩进的开关按钮
        /// </summary>
        public static bool IndentedToggleButton(string text, bool value)
        {
            var rect = EditorGUILayout.GetControlRect(true);
            rect.xMin += EditorGUIUtility.labelWidth;
            return GUI.Toggle(rect, value, text, EditorStyles.miniButton);
        }


        public static Vector2 SingleLineVector2Field(Rect rect, Vector2 value, GUIContent label)
        {
            rect = EditorGUI.PrefixLabel(rect, label);
            using (new LabelWidthScope(14))
            {
                using (new IndentScope(0, false))
                {
                    rect.width = (rect.width - 4) * 0.5f;
                    value.x = EditorGUI.FloatField(rect, "X", value.x);
                    rect.x = rect.xMax + 4;
                    value.y = EditorGUI.FloatField(rect, "Y", value.y);
                }
            }
            return value;
        }


        public static Vector2 SingleLineVector2Field(Rect rect, Vector2 value, GUIContent label, float aspectRatio)
        {
            rect = EditorGUI.PrefixLabel(rect, label);
            using (new LabelWidthScope(14))
            {
                using (new IndentScope(0, false))
                {
                    rect.width = (rect.width - 4) * 0.5f;
                    using (var scope = new ChangeCheckScope(null))
                    {
                        value.x = EditorGUI.FloatField(rect, "X", value.x);
                        if (scope.changed) value.y = value.x / aspectRatio;
                    }

                    rect.x = rect.xMax + 4;
                    using (var scope = new ChangeCheckScope(null))
                    {
                        value.y = EditorGUI.FloatField(rect, "Y", value.y);
                        if (scope.changed) value.x = value.y * aspectRatio;
                    }
                }
            }
            return value;
        }


        public static void SingleLineVector2Field(Rect rect, SerializedProperty property, GUIContent label)
        {
            property.vector2Value = SingleLineVector2Field(rect, property.vector2Value, label);
        }


        public static void SingleLineVector2Field(Rect rect, SerializedProperty property, GUIContent label, float aspectRatio)
        {
            property.vector2Value = SingleLineVector2Field(rect, property.vector2Value, label, aspectRatio);
        }


        public static Vector2 SingleLineVector2FieldLayout(Vector2 value, GUIContent label)
        {
            return SingleLineVector2Field(EditorGUILayout.GetControlRect(true, EditorGUIUtility.singleLineHeight), value, label);
        }


        public static void SingleLineVector2FieldLayout(SerializedProperty property, GUIContent label)
        {
            property.vector2Value = SingleLineVector2FieldLayout(property.vector2Value, label);
        }


        public static float DragValue(Rect rect, float value, float step)
        {
            return DragValue(rect, GUIContent.none, value, step, GUIStyle.none);
        }


        /// <summary>
        /// 拖动鼠标以修改数值
        /// </summary>
        public static float DragValue(Rect rect, GUIContent content, float value, float step, GUIStyle style, bool horizontal = true)
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);

            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.Repaint:
                    {
                        GUI.Label(rect, content, style);
                        break;
                    }

                case EventType.MouseDown:
                    {
                        if (Event.current.button == 0 && rect.Contains(Event.current.mousePosition))
                        {
                            GUIUtility.hotControl = controlID;
                        }
                        break;
                    }

                case EventType.MouseUp:
                    {
                        if (GUIUtility.hotControl == controlID)
                        {
                            GUIUtility.hotControl = 0;
                            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                        }
                        break;
                    }
            }

            if (GUIUtility.hotControl == controlID)
            {
                if (Event.current.isMouse)
                {
                    if (Event.current.type == EventType.MouseDrag)
                    {
                        if (horizontal) value += Event.current.delta.x * step;
                        else value -= Event.current.delta.y * step;
                        value = MathKit.RoundToSignificantDigitsFloat(value);

                        GUI.changed = true;
                    }

                    Event.current.Use();
                }

                rect.size = new Vector2(1000, 1000);
                rect.center = Event.current.mousePosition;
            }

            EditorGUIUtility.AddCursorRect(rect, horizontal ? MouseCursor.ResizeHorizontal : MouseCursor.ResizeVertical);

            return value;
        }


        /// <summary>
        /// 绘制可调节的进度条控件
        /// </summary>
        /// <param name="rect"> 绘制的矩形范围 </param>
        /// <param name="value"> [0, 1] 范围的进度 </param>
        /// <param name="backgroundColor"> 背景色 </param>
        /// <param name="foregroundColor"> 进度填充色 </param>
        /// <returns> 用户修改后的进度 </returns>
        public static float ProgressBar(
            Rect rect,
            float value,
            Color backgroundColor,
            Color foregroundColor)
        {
            int controlID = GUIUtility.GetControlID(FocusType.Passive);

            switch (Event.current.GetTypeForControl(controlID))
            {
                case EventType.Repaint:
                    {
                        using (new GUIColorScope(backgroundColor))
                        {
                            GUI.DrawTexture(rect, EditorGUIUtility.whiteTexture);

                            var progressRect = rect;
                            progressRect.width = Mathf.Round(progressRect.width * value);
                            GUI.color = foregroundColor;
                            GUI.DrawTexture(progressRect, EditorGUIUtility.whiteTexture);
                        }
                        break;
                    }

                case EventType.MouseDown:
                    {
                        if (Event.current.button == 0 && rect.Contains(Event.current.mousePosition))
                        {
                            GUIUtility.hotControl = controlID;
                        }
                        break;
                    }

                case EventType.MouseUp:
                    {
                        if (GUIUtility.hotControl == controlID)
                        {
                            GUIUtility.hotControl = 0;
                            UnityEditorInternal.InternalEditorUtility.RepaintAllViews();
                        }
                        break;
                    }
            }

            if (GUIUtility.hotControl == controlID)
            {
                if (Event.current.isMouse)
                {
                    float offset = Event.current.mousePosition.x - rect.x + 1f;
                    value = Mathf.Clamp01(offset / rect.width);

                    GUI.changed = true;
                    Event.current.Use();
                }

                rect.size = new Vector2(1000, 1000);
                rect.center = Event.current.mousePosition;
            }

            EditorGUIUtility.AddCursorRect(rect, MouseCursor.SlideArrow);

            return value;
        }


        /// <summary>
        /// 绘制可调节的进度条控件
        /// </summary>
        /// <param name="rect"> 绘制的矩形范围 </param>
        /// <param name="value"> [0, 1] 范围的进度 </param>
        /// <param name="backgroundColor"> 背景色 </param>
        /// <param name="foregroundColor"> 进度填充色 </param>
        /// <param name="borderColor"> 绘制的边界框颜色 </param>
        /// <param name="drawForegroundBorder"> 是否绘制进度条右侧的边界线 </param>
        /// <returns> 用户修改后的进度 </returns>
        public static float ProgressBar(
            Rect rect,
            float value,
            Color backgroundColor,
            Color foregroundColor,
            Color borderColor,
            bool drawForegroundBorder = false)
        {
            float result = ProgressBar(rect, value, backgroundColor, foregroundColor);

            if (Event.current.type == EventType.Repaint)
            {
                DrawWireRect(rect, borderColor);

                if (drawForegroundBorder)
                {
                    rect.width = Mathf.Round(rect.width * value);
                    if (rect.width > 0f)
                    {
                        rect.xMin = rect.xMax - 1f;
                        EditorGUI.DrawRect(rect, borderColor);
                    }
                }
            }

            return result;
        }


        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="itemCount"> 菜单项总数, 包括所有级别的菜单项和分隔符 </param>
        /// <param name="getItemContent"> 获取菜单项内容, 分割符必须以 "/" 结尾 </param>
        /// <param name="getItemState"> 获取菜单项状态, 不会对分隔符获取状态 </param>
        /// <param name="onSelect"> 菜单项被选中的回调 </param>
        /// <returns> 创建好的菜单, 接下来可以通过调用 DropDown 或 ShowAsContext 来显示菜单 </returns>
        public static GenericMenu CreateMenu(
            int itemCount,
            Func<int, GUIContent> getItemContent,
            Func<int, MenuItemState> getItemState,
            Action<int> onSelect)
        {
            GenericMenu menu = new GenericMenu();
            GUIContent content;
            MenuItemState state;

            for(int i=0; i<itemCount; i++)
            {
                content = getItemContent(i);
                if(content.text.EndsWith("/"))
                {
                    menu.AddSeparator(content.text.Substring(0, content.text.Length - 1));
                }
                else
                {
                    state = getItemState(i);
                    if(state == MenuItemState.Disabled)
                    {
                        menu.AddDisabledItem(content);
                    }
                    else
                    {
                        int index = i;
                        menu.AddItem(content, state == MenuItemState.Selected, () => onSelect(index));
                    }
                }
            }

            return menu;
        }


        /// <summary>
        /// 创建菜单
        /// </summary>
        /// <param name="items"> 元素集合 </param>
        /// <param name="getItemContent"> 获取菜单项内容, 分割符必须以 "/" 结尾 </param>
        /// <param name="getItemState"> 获取菜单项状态, 不会对分隔符获取状态 </param>
        /// <param name="onSelect"> 菜单项被选中的回调 </param>
        /// <returns> 创建好的菜单, 接下来可以通过调用 DropDown 或 ShowAsContext 来显示菜单 </returns>
        public static GenericMenu CreateMenu<T>(
            IEnumerable<T> items,
            Func<T, GUIContent> getItemContent,
            Func<T, MenuItemState> getItemState,
            Action<T> onSelect)
        {
            GenericMenu menu = new GenericMenu();
            GUIContent content;
            MenuItemState state;

            foreach (var i in items)
            {
                content = getItemContent(i);
                if (content.text.EndsWith("/"))
                {
                    menu.AddSeparator(content.text.Substring(0, content.text.Length - 1));
                }
                else
                {
                    state = getItemState(i);
                    if (state == MenuItemState.Disabled)
                    {
                        menu.AddDisabledItem(content);
                    }
                    else
                    {
                        T current = i;
                        menu.AddItem(content, state == MenuItemState.Selected, () => onSelect(current));
                    }
                }
            }

            return menu;
        }

    } // struct EditorGUIKit

} // namespace UnityExtensions.Editor

#endif // UNITY_EDITOR