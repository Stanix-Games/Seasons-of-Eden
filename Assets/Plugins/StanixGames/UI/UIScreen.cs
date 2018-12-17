using System;
using NUnit.Framework.Internal;
using UnityEngine;

namespace Plugins.StanixGames.UI
{
    [BaseScreen("Screen")]
    public abstract class UIScreen : MonoBehaviour
    {
        /// <summary>
        /// Declares attributed class as base screen class and adds it as menu item.
        /// </summary>
        /// <remarks>
        /// In most cases menu's item text would say <see cref="Name"/>,
        /// or if its empty then class name would be used instead. <br/>
        /// <br/>
        /// But in case of multiple classes using same Name or replacing same type name would be different. <br/>
        /// This is called Collision Avoidance,
        /// and if it happens then next names would be tried for all of collided items: <br/>
        /// 
        /// <list type="number">
        ///   <item><description><see cref="Name"/> or class name</description></item>
        ///   <item><description>
        ///     Any other string from this list prefixed with "{Replaces.BaseScreen.Name} - " ( Only when <see cref="Replaces"/> isn't null )
        ///   </description></item> 
        ///   <item><description>
        ///     "{BaseScreen.Name} [{AttributedClass.Name}]"
        ///   </description></item>
        ///   <item><description>
        ///     "{BaseScreen.Name} [{AttributedClass.Namespace.FirstDifferentFrom(CollisionClass.Namespace)}.{AttributedClass.Name}]
        ///   </description></item>
        /// </list>
        ///
        /// <example>
        /// If we have given code:
        /// <code>
        /// namespace StanixGames.GameA.Screens {
        ///     using BaseUIScreen = Plugins.StanixGames.UI.UIScreen;
        ///     [BaseScreen(replaces: typeof(BaseUIScreen))]
        ///     public abstract class UIScreen : BaseUIScreen {}
        ///     [BaseScreen(replaces: typeof(UIWindow))]
        ///     public abstract class PixelArtWindow : UIWindow {}
        ///     [BaseScreen(name: "Angry Popup", replaces: typeof(UIPopupWindow))]
        ///     public AngryPopupWindow : UIPopupWindow {}
        /// }
        /// namespace StanixGames.GameB.Screens {
        ///     using BaseUIScreen = Plugins.StanixGames.UI.UIScreen;
        ///     [BaseScreen(replaces: typeof(BaseUIScreen))]
        ///     public abstract class UIScreen : BaseUIScreen {}
        ///     [BaseScreen(replaces: typeof(UIWindow))]
        ///     public abstract class ShadyArtWindow : UIWindow {}
        ///     [BaseScreen(replaces: typeof(UIPopupWindow))]
        ///     public GoodPopupWindow : UIPopupWindow {}
        /// }
        /// </code>
        /// Resulting menu would contain at least those items:<br />
        /// <list type="bullet">
        ///   <item><description>Screen - [StanixGames.GameA.UIScreen]</description></item>
        ///   <item><description>Screen - [StanixGames.GameB.UIScreen]</description></item>
        ///   <item><description>Window - [PixelArtWindow]</description></item>
        ///   <item><description>Window - [ShadyArtWindow]</description></item>
        ///   <item><description>Popup window - Angry Popup</description></item>
        ///   <item><description>Popup window - GoodPopupWindow</description></item>
        /// </list>
        /// Last item might be strange for some peps,
        /// but it utilizes fact that if name is empty ( and default name is empty ) then class name would be used. 
        /// </example>
        /// </remarks>
        public class BaseScreen : Attribute
        {
            /// <summary>
            /// User friendly name to display. Defaults to "".
            /// </summary>
            public string Name { get; }

            /// <summary>
            /// Class that annotated class should replace. Default to null.
            /// If field isn't null then class represented by type Replaces would be hidden in creation menu.
            /// </summary>
            /// <remarks>
            /// In case of multiple classes replacing same target collision avoidance would be used.
            /// <see cref="BaseScreen" /> for more info about collision avoidance
            ///
            /// Annotated class doesn't need to extend class that it replaces,
            /// but for compatibility reasons it is recommended to extend it.
            /// </remarks>
            public Type Replaces { get; }
            
            public BaseScreen(string name = "", Type replaces = null)
            {
                Name = name;
                Replaces = replaces;
            }
        }
        internal virtual void OnLoad() {}
    }
}
