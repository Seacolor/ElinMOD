using Newtonsoft.Json;

namespace PetOperation
{
    public class Operation : EClass
    {
        [JsonProperty]
        public bool isVanguard;
        [JsonProperty]
        public int targetEnemy = 0;
    }
}
