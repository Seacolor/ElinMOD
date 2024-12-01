namespace PetOperation
{
    public class ActOrderVanguard : Act
    {
        public override CursorInfo CursorIcon {
            get {
                return CursorSystem.Move;
            }
        }

        public override int PerformDistance
        {
            get
            {
                return 3;
            }
        }

        public override bool CanPerform() {
            if (Act.TC == null || !Act.TC.IsAliveInCurrentZone || !Act.TC.isChara || !Act.TC.IsPCParty)
            {
                return false;
            }
            if (Act.CC.Dist(Act.TC) > this.PerformDistance)
            {
                return false;
            }
            if (this.PerformDistance == 1)
            {
                if (!Act.CC.CanInteractTo(Act.TC))
                {
                    return false;
                }
            }
            else if (!Act.CC.CanSeeLos(Act.TC, -1, false))
            {
                return false;
            }
            return base.CanPerform();
        }

        public override bool Perform() {
            Chara tc = Act.TC.Chara;
            Operation o = OperationManager.globalOperations.Find(tc.uid);
            if (o == null) {
                o = new Operation();
                OperationManager.globalOperations.Add(tc.uid, o);
            }
            o.isVanguard = true;
            return true;
        }

        public override string GetText(string str = "") {
            return Lang.isJP ? "前衛にする" : "Order the vanguard";
        }
    }
}
