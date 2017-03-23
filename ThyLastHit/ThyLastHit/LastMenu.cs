using System;
using EloBuddy;
using EloBuddy.SDK.Events;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;


namespace ThyLastHit
{
    class LastMenu
    {
        private static Menu Menu;
        private static AttackableUnit lasthit;

        internal static void Initialize()
        {
            Loading.OnLoadingComplete += Game_OnStart;
        }

        private static void Game_OnStart(EventArgs args)
        {
            Chat.Print("I made my first BETA 1v addon");
            Menu = MainMenu.AddMenu("LastHit", "LastHit");
            Menu.AddSeparator();
            Menu.AddLabel("Creator of this Addon ThytYN");
            Menu.Add("lasthit", new KeyBind("LastHit", false, KeyBind.BindTypes.HoldActive, 'X'));

        }
    }
}
