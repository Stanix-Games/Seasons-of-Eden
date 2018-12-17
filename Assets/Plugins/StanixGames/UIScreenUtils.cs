#if UNITY_EDITOR

using Plugins.StanixGames.InternalUtils;
using Plugins.StanixGames.UI;
using UnityEditor;
using UnityEngine;

namespace Plugins.StanixGames
{
    public static class UIScreenUtils
    {
        [MenuItem("Assets/Create/Extended Screen", priority = 1)]
        public static void ExtendScreen()
        {
            var screenToExtend = Selection.activeObject as GameObject;
            if (screenToExtend == null)
            {
                return;
            }

            new EditorTextInputDialog(
                (newScreenName) => CreateScreenExtension(screenToExtend, newScreenName),
                "Type in screen extension name",
                defaultValue: "ScreenExtension"
            ).ShowUtility();
        }

        private static void CreateScreenExtension(GameObject screenToExtend, string newScreenName)
        {
            var scriptToExtend = MonoScript.FromMonoBehaviour(screenToExtend.GetComponent<UIScreen>());

            var path = AssetDatabase.GetAssetPath(screenToExtend);
            path = path.Substring(0, path.LastIndexOf('/'));
            var prefabPath = $"{path}/{newScreenName}.prefab";
            var scenePath = $"{path}/{newScreenName}.scene";

            // And who is going to fire me anyway ?..
            var variantMethod =
                typeof(PrefabUtility)
                    .GetMethod(
                        "CreateVariant",
                        System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic
                    );
            Debug.Assert(variantMethod != null, "variantMethod != null");

            var variant = variantMethod.Invoke(null, new object[] {screenToExtend, prefabPath}) as GameObject;
        }

        [MenuItem("Assets/Create/Extended Screen", validate = true)]
        public static bool ValidateExtendScreen()
        {
            var selected = Selection.activeObject;
            return (selected as GameObject)?.GetComponent<UIScreen>() != null;
        }

        [MenuItem("Assets/Create/UI Screen")]
        public static void CreateNewScreenItem()
        {
            new EditorTextInputDialog(
                (newScreenName) => CreateNewScreen(newScreenName),
                "Type in screen name",
                defaultValue: "NewScreen"
            ).ShowUtility();
        }

        private static void CreateNewScreen(string newScreenName)
        {
            var screenObject = new GameObject();
            screenObject.AddComponent<UIScreen>();
            PrefabUtility.SaveAsPrefabAsset(screenObject, $"Scenes/{newScreenName}.prefab");
            // EditorSceneManager.CreateScene(newScreenName)
        }
    }
}

#endif
