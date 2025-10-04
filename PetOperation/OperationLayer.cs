using YKF;

namespace PetOperation
{
    public class OperationLayer : YKLayer<Chara>
    {
        public override void OnLayout()
        {
            CreateTab<OperationTab>(Lang.Get("po_operation"), "operation");
        }
    }
}
