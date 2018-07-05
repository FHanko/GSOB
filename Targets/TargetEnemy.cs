using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB.Targets
{
    class TargetEnemy : ITarget
    {
        string EnemyName;
        Scr_Player Player;
        public TargetEnemy(string enemyName, Scr_Player player)
        {
            EnemyName = enemyName;
            Player = player;
        }

        public float DistanceTo()
        {
            return Vector3.Distance(Player.transform.position, Nearest().transform.position);
        }

        public void GoTo()
        {
            Player.UpdateAutomove(Nearest().transform.position);
        }

        public void Interact()
        {
            if (Player.anim.actionId == 0 && Player.actionId == 0 && Player.attacking == false)
            {
                Player.targetNPC = Nearest();
                Player.connecter.connectionHandler.rpcSender.selectNpcTarget(Player.playerName, Nearest().npcUniqueId);
                Player.useAbility(0);
            }
        }

        public void SetTagetName(string name)
        {
            EnemyName = name;
        }

        public string TargetName()
        {
            return EnemyName;
        }

        Scr_Npc Nearest()
        {
            return Utils.NearestNpcTo(Player, EnemyName);
        }
    }
}
