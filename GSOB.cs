using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB
{
    public class GSOB
    {
        public static Dictionary<string, Action<string[], Menucontroller>> Commands = new Dictionary<string, Action<string[], Menucontroller>>();
        static Scr_Player Player;
        public static void Start(Scr_Player player)
        {
            Player = player;
            Targets.TargetHarvest OakHarvest = new Targets.TargetHarvest("Oak", player);
            Targets.TargetHandler targets = new Targets.TargetHandler(OakHarvest);
            UpdateHook.updatables.Add(targets);
            UpdateHook.updatables.Add(new QOL.Scanner());

            Commands.Add("@target", SetTargetName);
            Commands.Add("@maxdelay", SetMaxDelay);
            Commands.Add("@loot", ToggleLoot);
            Commands.Add("@trigger", AddTrigger);
            Commands.Add("@xp", AddGhostXP);
            Commands.Add("@addwp", AddWP);
            Commands.Add("@clearwp", ClearWP);
            Commands.Add("@startwp", StartWP);
            Commands.Add("@setmc", SetMc);
            Commands.Add("@scr", SetChaserRadius);
        }

        static void SetTargetName(string[] param, Menucontroller mc)
        {
            Targets.ITarget setTo = null;
            param[1] = param[1].Replace('_', ' ');

            if (Utils.NearestHarvestTo(Player, param[1]) != null)
            {
                setTo = new Targets.TargetHarvest(param[1], Player);
            }
            else if (Utils.NearestNpcTo(Player, param[1]) != null)
            {
                setTo = new Targets.TargetEnemy(param[1], Player);
            }

            foreach (IUpdatable iup in UpdateHook.updatables)
            {
                if (iup is Targets.TargetHandler)
                {
                    ((Targets.TargetHandler)iup).SetTarget(setTo);
                }
            }
            mc.sendChatMessage("Target is now " + param[1]);
        }

        static void SetMaxDelay(string[] param, Menucontroller mc)
        {
            int amount = 0;
            if (int.TryParse(param[1], out amount)) {
                Targets.TargetHandler.InteractTickDelayMax = amount;
            }
            mc.sendChatMessage("Max delay is now " + param[1]);
        }

        static void ToggleLoot(string[] param, Menucontroller mc)
        {
            IEnumerable<IUpdatable> lootin = UpdateHook.updatables.Where(iup => iup is QOL.AutoLoot);
            if (lootin.Count() > 0)
            {
                UpdateHook.updatables.RemoveAll(iup => iup is QOL.AutoLoot);
                mc.sendChatMessage("Loot is now off");
            }
            else
            {
                UpdateHook.updatables.Add(new QOL.AutoLoot());
                mc.sendChatMessage("Loot is now on");
            }
        }

        static void AddTrigger(string[] param, Menucontroller mc)
        {
            int trigger = 0;
            int slot = 0;
            if (int.TryParse(param[1], out trigger) && int.TryParse(param[2], out slot))
            {
                UpdateHook.updatables.Add(new QOL.HealBelowHealth(trigger, slot));
                mc.sendChatMessage("Added " + param[2] + " to trigger when below " + param[1] + " Health");
            }
            else {
                mc.sendChatMessage("Command error");
            }
        }

        static void AddGhostXP(string[] param, Menucontroller mc)
        {
            int id = 0;
            int amount = 0;

            if (int.TryParse(param[1], out id) && int.TryParse(param[2], out amount))
            {
                Player.connecter.skillsAndAbilities.addXp(amount, id);
            }
        }

        static Stack<Targets.Waypoint> waypoints = new Stack<Targets.Waypoint>();
        static void AddWP(string[] param, Menucontroller mc)
        {
            Vector3 wp = new Vector3(
                Player.transform.position.x,
                Player.transform.position.y,
                Player.transform.position.z
                );
            Targets.Waypoint waypoint = new Targets.Waypoint(wp);
            waypoint.MaxTickTime = 0f;
            if (param.Length > 2)
            {
                float amount = 0f;
                if (float.TryParse(param[1], out amount))
                {
                    waypoint.MaxTickTime = amount;
                }
            }
            waypoints.Push(new Targets.Waypoint(wp));
            mc.sendChatMessage("Waypoint added");
        }

        static void ClearWP(string[] param, Menucontroller mc)
        {
            waypoints.Clear();
            mc.sendChatMessage("Waypoints cleared");
        }

        static void StartWP(string[] param, Menucontroller mc)
        {
            param[1] = param[1].Replace('_', ' ');
            Targets.ITarget setTo = new Targets.TargetWayPointChaserHarvest(param[1], Player, waypoints);
            foreach (IUpdatable iup in UpdateHook.updatables)
            {
                if (iup is Targets.TargetHandler)
                {
                    ((Targets.TargetHandler)iup).SetTarget(setTo);
                }
            }
            mc.sendChatMessage("Started waypoint stack for " + param[1]);
        }

        public static Menucontroller staticMC = null;
        static void SetMc(string[] param, Menucontroller mc)
        {
            staticMC = mc;
        }

        public static void TrySendDebug(string message)
        {
            if (staticMC != null)
            {
                staticMC.sendChatMessage(message);
            }
        }

        static void SetChaserRadius(string[] param, Menucontroller mc)
        {
            float amount = 0.3f;
            if (float.TryParse(param[1], out amount))
            {
                Targets.TargetHandler.InteractChaserRadius = amount;
                mc.sendChatMessage("Waypoint radius: " + param[1]);
            }
        }
    }
}
