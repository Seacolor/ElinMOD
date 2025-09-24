using System.Collections.Generic;
using System.Reflection.Emit;
using System.Linq;
using HarmonyLib;
using System.Reflection;

namespace DanalinOfWater
{
    [HarmonyPatch]
    internal class ConInsanePatch
    {
        [HarmonyPatch(typeof(ConInsane), nameof(ConInsane.Tick))]
        internal static IEnumerable<CodeInstruction> Transpiler(IEnumerable<CodeInstruction> instructions)
        {
            var map = new Dictionary<MethodInfo, MethodInfo>();

            MethodInfo addCondOpen = typeof(Chara)
                .GetMethods(BindingFlags.Instance | BindingFlags.Public | BindingFlags.NonPublic)
                .Single(m =>
                    m.Name == "AddCondition" &&
                    m.IsGenericMethodDefinition &&
                    m.GetParameters().Length == 2);

            MethodInfo callConfuse = addCondOpen.MakeGenericMethod(typeof(ConConfuse));
            MethodInfo callDim = addCondOpen.MakeGenericMethod(typeof(ConDim));
            MethodInfo callFear = addCondOpen.MakeGenericMethod(typeof(ConFear));

            map[callConfuse] = AccessTools.Method(
                typeof(ConInsanePatch),
                nameof(VariantAddConditionConConfuse)
            );
            map[callDim] = AccessTools.Method(
                typeof(ConInsanePatch),
                nameof(VariantAddConditionConDim)
            );
            map[callFear] = AccessTools.Method(
                typeof(ConInsanePatch),
                nameof(VariantAddConditionConFear)
            );

            var matcher = new CodeMatcher(instructions);

            while (true)
            {
                matcher = matcher.MatchForward(
                    false,
                    new CodeMatch(ci =>
                        ci.opcode == OpCodes.Callvirt &&
                        ci.operand is MethodInfo mi &&
                        map.ContainsKey(mi)
                    )
                );

                if (matcher.IsInvalid)
                    break;

                var original = matcher.Instruction;
                var origMI = (MethodInfo)original.operand;
                var replMI = map[origMI];

                var newInstr = new CodeInstruction(OpCodes.Call, replMI);
                newInstr.labels.AddRange(original.labels);
                newInstr.blocks.AddRange(original.blocks);

                matcher.SetInstruction(newInstr);

                matcher.Advance(1);
            }

            return matcher.InstructionEnumeration();
        }

        internal static Condition VariantAddConditionConConfuse(
            Chara __instance, int p = 100, bool force = false)
        {
            if (__instance.HasElement(293487))
            {
                if (__instance.SAN.GetValue() >= 40)
                {
                    return null;
                }
            }
            return __instance.AddCondition<ConConfuse>(p);
        }
        internal static Condition VariantAddConditionConDim(
            Chara __instance, int p = 100, bool force = false)
        {
            if (__instance.HasElement(293487))
            {
                if (__instance.SAN.GetValue() >= 20)
                {
                    return null;
                }
            }
            return __instance.AddCondition<ConDim>(p);
        }
        internal static Condition VariantAddConditionConFear(
            Chara __instance, int p = 100, bool force = false)
        {
            if (__instance.HasElement(293487))
            {
                if (__instance.SAN.GetValue() >= 50)
                {
                    return null;
                }
            }
            return __instance.AddCondition<ConFear>(p);
        }
    }
}
