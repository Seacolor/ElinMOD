using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using YKF;

namespace PetOperation
{
    public class ActReviseOperation : Act
    {
        public override CursorInfo CursorIcon
        {
            get
            {
                return CursorSystem.Move;
            }
        }

        public override int PerformDistance
        {
            get
            {
                return 2;
            }
        }

        public override bool CanPerform()
        {
            if (Act.TC == null || !Act.TC.IsAliveInCurrentZone || !Act.TC.isChara || !Act.TC.IsPCParty)
            {
                return false;
            }
            if (Act.CC.Dist(Act.TC) > this.PerformDistance)
            {
                return false;
            }
            return base.CanPerform();
        }

        public override bool Perform()
        {
            Chara chara = Act.TC.Chara;
            YK.CreateLayer<OperationLayer, Chara>(chara);
            return false;
        }

        public override string GetText(string str = "")
        {
            return Lang.Get("actReviseOperation");
        }
    }
}
