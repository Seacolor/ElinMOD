using System.Collections.Generic;

namespace PetOperation
{
    public class GlobalOperationList : Dictionary<int, Operation>
    {
        public void Add(Chara c, Operation o) {
            base[c.uid] = o;
        }
        public void Remove(Chara c) {
            base.Remove(c.uid);
        }
        public Operation Find(int uid) {
            if (base.ContainsKey(uid)) {
                return base[uid];
            }
            return null;
        }
    }
}
