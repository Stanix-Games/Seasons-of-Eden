#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Plugins.StanixGames.UI.Components
{
    public static class CustomUIComponents
    {
        [MenuItem("GameObject/UI/Progress Bar")]
        public static void CreateProgressBar(MenuCommand menuCommand)
        {
            LinkGameObject(menuCommand, new GameObject("ProgressBar")).AddComponent<ProgressBarController>();
        }

        private static GameObject LinkGameObject(MenuCommand menuCommand, GameObject obj)
        {
            GameObjectUtility.SetParentAndAlign(obj, menuCommand.context as GameObject);
            Undo.RegisterCreatedObjectUndo(obj, "Create " + obj.name);
            Selection.activeObject = obj;
            return obj;
        }
    }
}

#endif