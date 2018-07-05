using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace GSOB
{
    public class UpdateHook
    {
        public static List<IUpdatable> updatables = new List<IUpdatable>();
        static bool active = false;

        public static void Update(Scr_Player player)
        {
            if (Input.GetKeyDown(KeyCode.U))
            {
                active = !active;
            }

            if (active)
            { 
                foreach (IUpdatable iup in updatables)
                {
                    iup.Update(player);
                }
            }
        }
    }
}
