using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB.QOL
{
    class Scanner : IUpdatable
    {
        public void Update(Scr_Player player)
        {
            if (Input.GetKeyDown(KeyCode.U) && Input.GetKeyDown(KeyCode.LeftControl))
            {
                player.connecter.harvestableHandler.harvestablesList.ForEach(h =>
                {
                    h.gameObject.AddComponent<TextMesh>();
                    h.GetComponent<TextMesh>().text = h.harvestableName;
                    h.gameObject.AddComponent<MeshRenderer>();
                });
                player.connecter.npcHandler.npcList.ForEach(n =>
                {
                    n.gameObject.AddComponent<TextMesh>();
                    n.GetComponent<TextMesh>().text = n.npcName;
                });
            }
        }
    }
}
