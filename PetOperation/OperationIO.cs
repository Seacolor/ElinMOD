using Newtonsoft.Json;

using System.IO;

namespace PetOperation
{
    public class OperationIO : EClass
    {
        public static GlobalOperationList LoadOperation(string root) {
            string path = root + "/operation.txt";
            return JsonConvert.DeserializeObject<GlobalOperationList>(IO.IsCompressed(path) ? IO.Decompress(path) : File.ReadAllText(path), GameIO.jsReadGame);
        }
    }
}
