﻿#if UNITY_EDITOR

using UnityEditor;

namespace Plugins.StanixGames.UI.Components
{
    [CustomEditor(typeof(ProgressBarController))]
    public class ProgressBarControllerEditor : Editor
    {
        void OnEnable()
        {
            (target as ProgressBarController).Image.hideFlags = UnityEngine.HideFlags.HideInInspector;
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();
            CreateEditor((target as ProgressBarController).Image).OnInspectorGUI();
        }
    }
}

#endif
