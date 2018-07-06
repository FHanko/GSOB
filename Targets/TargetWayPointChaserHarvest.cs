using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB.Targets
{
    class TargetWayPointChaserHarvest : TargetHarvest
    {
        Stack<Waypoint> Forward = new Stack<Waypoint>();
        Stack<Waypoint> Back = new Stack<Waypoint>();
        Waypoint current;
        public TargetWayPointChaserHarvest(string harvestname, Scr_Player player, Stack<Waypoint> stack) : base(harvestname, player)
        {
            Forward = new Stack<Waypoint>(stack.Reverse());
            current = Forward.Pop();
        }

        public override float DistanceTo()
        {
            return Vector3.Distance(Player.transform.position, current.Goal);
        }

        public override void GoTo()
        {
            Player.UpdateAutomove(current.Goal);
        }

        public override void Interact()
        {
            float dist;
            if (Nearest() == null) { dist = float.MaxValue; }
            else {
                dist = Vector3.Distance(Player.transform.position, Nearest().transform.position);
            }
            if (dist < 4.3)
            {
                if (Player.anim.actionId == 0 && Player.actionId == 0)
                {
                    Player.connecter.harvestableHandler.attemptHarvest(Nearest().uniqueId);
                }
            }
            else
            {
                if (Forward.Count == 0)
                {
                    Forward = new Stack<Waypoint>(Back.Reverse());
                }
                if (current.TickTime < 1)
                {
                    Back.Push(current);
                    current = Forward.Pop();
                    Back.Peek().TickTime = Back.Peek().MaxTickTime;
                }
                else
                {
                    current.TickTime--;
                }
            }
        }
    }
}
