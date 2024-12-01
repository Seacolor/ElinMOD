using Newtonsoft.Json;

namespace PetOperation
{
    [JsonObject(MemberSerialization.OptIn)]
    public class OperationManager
    {
        [JsonProperty]
        public static GlobalOperationList globalOperations = new GlobalOperationList();
    }
}
