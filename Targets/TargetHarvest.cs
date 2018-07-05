using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB.Targets
{
    class TargetHarvest : ITarget
    {
        protected string HarvestName;
        protected Scr_Player Player;
        public TargetHarvest(string harvestName, Scr_Player player)
        {
            HarvestName = harvestName;
            Player = player;
        }

        public virtual float DistanceTo()
        {
            return Vector3.Distance(Player.transform.position, Nearest().transform.position);
        }

        public virtual void GoTo()
        {
            Player.UpdateAutomove(Nearest().transform.position);
        }
        
        public virtual void Interact()
        {
            if (Player.anim.actionId == 0 && Player.actionId == 0)
            {
                Player.connecter.harvestableHandler.attemptHarvest(Nearest().uniqueId);
            }
        }

        public void SetTagetName(string name)
        {
            HarvestName = name;
        }

        public string TargetName()
        {
            return HarvestName;
        }

        protected Scr_Harvestable Nearest()
        {
            return Utils.NearestHarvestTo(Player, HarvestName);
        }
    }
}
