using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSOB.QOL
{
    class HealBelowHealth : IUpdatable
    {
        int Trigger;
        int Slot;
        public HealBelowHealth(int trigger, int slot)
        {
            Trigger = trigger;
            Slot = slot;
        }

        public void Update(Scr_Player player)
        {

            if (player.currentHealth < Trigger)
            {
                if (SkillsAndAbilities.instance.getAbilitySlot(Slot).cooldownCurrent <= 0)
                {
                    player.useAbility(Slot);
                }
            }
        }
    }
}
