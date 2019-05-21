﻿using UnityEngine;

namespace UnityExtensions
{
    /// <summary>
    /// ShaderIDs
    /// </summary>
    public struct ShaderIDs
    {
        public static readonly int mainTex = Shader.PropertyToID("_MainTex");
        public static readonly int color = Shader.PropertyToID("_Color");

    } // struct ShaderIDs

} // namespace UnityExtensions