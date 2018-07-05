using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB
{
    public class Utils
    {
        public static Scr_Harvestable NearestHarvestTo(Scr_Player from, string to)
        {
            Scr_Harvestable nearest = null;
            foreach (Scr_Harvestable harvest in from.connecter.harvestableHandler.harvestablesList)
            {
                if (harvest != null && harvest.currentHealth > 0 && (harvest.harvestableName.Equals(to) || to.Equals("all")))
                {
                    if (nearest == null) nearest = harvest;
                    if (Vector3.Distance(from.transform.position, harvest.transform.position) < Vector3.Distance(from.transform.position, nearest.transform.position))
                    {
                        nearest = harvest;
                    }
                }
            }
            return nearest;
        }

        public static Scr_Npc NearestNpcTo(Scr_Player from, string to)
        {
            Scr_Npc nearest = null;
            foreach (Scr_Npc npc in from.connecter.npcHandler.npcList)
            {
                if (npc != null && npc.currentHealth > 0 && npc.npcName.Equals(to))
                {
                    if (nearest == null) nearest = npc;
                    if (Vector3.Distance(from.transform.position, npc.transform.position) < Vector3.Distance(from.transform.position, nearest.transform.position))
                    {
                        nearest = npc;
                    }
                }
            }
            return nearest;
        }
    }
}
