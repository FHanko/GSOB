using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB.QOL
{
    class AutoLoot : IUpdatable
    {
        public void Update(Scr_Player player)
        {
            foreach(Lootbag lb in player.connecter.lootHandler.lootList)
            {
                if (Vector3.Distance(player.transform.position, lb.transform.position) < 3)
                {
                    player.connecter.lootHandler.collectLoot(lb.id);
                }
            }
        }
    }
}
