using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GSOB.Targets
{
    public interface ITarget
    {
        void GoTo();
        void Interact();
        float DistanceTo();
        string TargetName();
        void SetTagetName(string name);
    }
}
