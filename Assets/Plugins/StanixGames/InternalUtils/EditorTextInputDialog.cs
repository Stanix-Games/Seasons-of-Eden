#if UNITY_EDITOR

using UnityEditor;
using UnityEngine;

namespace Plugins.StanixGames.InternalUtils
{
    internal class EditorTextInputDialog : EditorWindow
    {
        public delegate void Callback(string userInput);

        private readonly Callback callback;
        private readonly string fieldLabel;
        private readonly string okButtonLabel;

        private string inputText;

        public EditorTextInputDialog(Callback callback, string fieldLabel, string okButtonLabel = "OK", string defaultValue = "")
        {
            this.callback = callback;
            this.fieldLabel = fieldLabel;
            this.okButtonLabel = okButtonLabel;
            this.inputText = defaultValue;
        }

        void OnGUI()
        {
            inputText = EditorGUILayout.TextField(fieldLabel, inputText);

            if (GUILayout.Button(okButtonLabel))
            {
                callback(inputText);
                Close();
            }

            if (GUILayout.Button("Abort"))
                Close();
        }
    }
}

#endif
