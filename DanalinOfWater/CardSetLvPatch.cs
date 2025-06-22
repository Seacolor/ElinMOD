using System.Collections.Generic;
using System.Reflection.Emit;
using HarmonyLib;

namespace DanalinOfWater
{
    [HarmonyPatch(typeof(Card), nameof(Card.SetLv))]
    internal class CardSetLvPatch
    {
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            instructions = new CodeMatcher(instructions)
                .MatchEndForward(
                    new CodeMatch(
                        OpCodes.Callvirt,
                        AccessTools.PropertyGetter(typeof(Dictionary<int, Element>), "Values")
                    )
                )
                .ThrowIfInvalid("[DanalinOfWater] get_Values not found.")
               .Set(
                    OpCodes.Call,
                    AccessTools.Method(typeof(CardSetLvPatch), nameof(VariantGetValues))
                )
                .InstructionEnumeration();
            return instructions;
        }
        internal static Dictionary<int, Element>.ValueCollection VariantGetValues(Dictionary<int, Element> dict)
        {
            Dictionary<int, Element> copyDict = new Dictionary<int, Element>(dict);
            return copyDict.Values;
        }
    }
}
