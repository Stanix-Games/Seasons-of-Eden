using UnityEngine;

namespace Plugins.StanixGames.UI
{
    public class UIManager
    {
        public static T LoadUIScreen<T>(GameObject screen)
            where T : UIScreen
        {
            var newScreen = Object.Instantiate(screen);
            var uiScreen = newScreen.GetComponent<T>();
            uiScreen.OnLoad();
            return uiScreen;
        }
    }
}
