using System;
using EloBuddy;
using EloBuddy.SDK;
using EloBuddy.SDK.Menu;
using EloBuddy.SDK.Menu.Values;
using System.Linq;

namespace ThyLastHit
{
    class LastHit
    {
        static AttackableUnit lasthit;
        static Menu Menu;
        static float LastRitt, Move, Delay, Deley, Minion;

        internal void Initialize()
        {
            Game.OnUpdate += Game_OnTick;
            Obj_AI_Base.OnBasicAttack += Obj_AI_Base_OnBasicAttack;
        }

        private void Obj_AI_Base_OnBasicAttack(Obj_AI_Base sender, GameObjectProcessSpellCastEventArgs args)
        {
            if (sender.IsMe)
            {
                Delay = sender.AttackCastDelay;
                Deley = sender.AttackDelay;
                LastRitt = Game.Time;
            }
        
        }

        private void Game_OnTick(EventArgs args)
        {
            if (Menu["LastHit"].Cast<KeyBind>().CurrentValue)
            {
                Hit();
            }
        }

        static void Hit()
        {
            if (Game.Time < Minion + 0.5f && Game.Time + 0.2f > LastRitt + Deley)
            {
                Player.IssueOrder(GameObjectOrder.AttackUnit, lasthit);
                return;
            }

            if (Game.Time > LastRitt + Delay + 0.05f && Game.Time > Move + 0.2f)
            {
                Player.IssueOrder(GameObjectOrder.MoveTo, Game.CursorPos);
                Move = Game.Time;
                Minion = 0;
            }

            if (Game.Time + 0.2f < LastRitt + Deley)
                return;
            foreach (var Minion in EntityManager.MinionsAndMonsters.Minions
                        .Where(m => m.IsValidTarget(Player.Instance.AttackRange + Player.Instance.BoundingRadius + m.BoundingRadius, true))
                        .OrderBy(m => m.CharData.BaseSkinName.Contains("TrueMinion"))
                        .ThenBy(m => m.CharData.BaseSkinName.Contains("SuperMinion"))
                        .ThenBy(m => m.Health)
                        .ThenByDescending(m => m.MaxHealth))
            {
                var healthPred = Prediction.Health.GetPrediction(Minion, (int)(Player.Instance.AttackCastDelay * 1000) + 1000 * (int)(Math.Max(0, Player.Instance.Distance(Minion) - Minion.BoundingRadius) / (int)Player.Instance.BasicAttack.MissileSpeed));
                if (healthPred <= Player.Instance.GetAutoAttackDamage(Minion))
                {
                    lasthit = Minion;
                    LastHit.Minion = Game.Time;
                    Player.IssueOrder(GameObjectOrder.AttackUnit, lasthit);
                    return;
                }
            }
        }
    }
}

