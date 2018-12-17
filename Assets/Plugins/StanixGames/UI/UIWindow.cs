using System;
using UnityEngine;

namespace Plugins.StanixGames.UI
{
    [BaseScreen("Window")]
    public class UIWindow : UIScreen
    {
        [Flags]
        public enum WindowState
        {
            None           = 0,
            Movable        = 1 << 0,
            Closable       = 1 << 1,
            Resizable      = 1 << 2
        }
        
        public Rect PreferedSize { protected set; get; }
        public WindowState State { protected set; get; }
    }
}