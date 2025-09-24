using Newtonsoft.Json;

namespace PetOperation
{
    public class Operation : EClass
    {
        [JsonProperty]
        public bool isVanguard;
    }
}
