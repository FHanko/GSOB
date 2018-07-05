using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSOB.Targets
{
    public class TargetHandler:IUpdatable
    {
        ITarget target;

        public TargetHandler(ITarget startTarget)
        {
            target = startTarget;
        }

        public void SetTarget(ITarget target)
        {
            this.target = target;
        }

        public ITarget GetTarget()
        {
            return target;
        }

        public static int InteractTickDelayMax = 45;
        public int InteractTickDelay = 15;
        public static float InteractChaserRadius = 0.3f;
        public void Update(Scr_Player player)
        {
            if (target.DistanceTo() < (target is TargetWayPointChaserHarvest ? InteractChaserRadius : 4.3f))
            {
                
                player.autoMoving = false;
                if (InteractTickDelay < 1)
                {
                    target.Interact();
                    InteractTickDelay = InteractTickDelayMax;
                }
                if(InteractTickDelay>0)
                InteractTickDelay--;
            }
            else {
                target.GoTo();
            }
        }
    }
}
