using System.Collections.Generic;
using YKF;

namespace PetOperation
{
    public class OperationTab : YKLayout<Chara>
    {
        public override void OnLayout()
        {
            Chara chara = Layer.Data;
            Operation o = OperationManager.globalOperations.Find(chara.uid);
            if (o == null)
            {
                o = new Operation();
                OperationManager.globalOperations.Add(chara.uid, o);
            }
            Header(Lang.Get("po_attack_target"));
            var targetList = new List<string>() { Lang.Get("po_at_free"), Lang.Get("po_at_ally"), Lang.Get("po_at_master") };
            Dropdown(targetList, (idx) => { o.targetEnemy = idx; }, o.targetEnemy);
            Header(Lang.Get("po_formation"));
            var formationList = new List<string>() { Lang.Get("po_f_rear_guard"), Lang.Get("po_f_vanguard") };
            Dropdown(formationList, (idx) => {
                switch (idx)
                {
                    case 1:
                        o.isVanguard = true;
                        break;
                    default:
                        o.isVanguard = false;
                        break;
                }
            }, o.isVanguard ? 1 : 0);
        }
    }
}
