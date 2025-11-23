using HarmonyLib;
using System.Collections.Generic;
using UnityEngine;

namespace CatWithMillionLives
{
    [HarmonyPatch(typeof(Feat), nameof(Feat.Apply))]
    internal class FeatApplyPatch
    {
        internal static void Postfix(Feat __instance, ref List<string> __result, int a, ElementContainer owner, bool hint = false)
        {
            switch (__instance.id)
            {
                case 689054:
                    ModBase(61, a * 5, hide: false);
                    ModBase(79, a * 10, hide: false);
                    ModBase(950, a * 3, hide: false);
                    break;
            }
            __result = Feat.hints;
            void ModBase(int ele, int _v, bool hide)
            {
                if (!hint)
                {
                    owner.ModBase(ele, _v);
                }
                if (!hide && _v != 0)
                {
                    NoteElement(ele, _v);
                }
            }
            void Note(string s)
            {
                if (hint)
                {
                    Feat.hints.Add(s);
                }
            }
            void NoteElement(int ele, int b)
            {
                SourceElement.Row row = EClass.sources.elements.map[ele];
                if (row.category == "ability")
                {
                    Note("hintLearnAbility".lang(row.GetName().ToTitleCase()));
                }
                else if (row.tag.Contains("flag"))
                {
                    Note(row.GetName());
                }
                else
                {
                    string @ref = ((b < 0) ? "" : "+") + b;
                    if (row.category == "resist")
                    {
                        int num3 = 0;
                        @ref = ((b > 0) ? "+" : "-").Repeat(Mathf.Clamp(Mathf.Abs(b) / 5 + num3, 1, 5));
                        Note("modValueRes".lang(row.GetName(), @ref));
                    }
                    else
                    {
                        Note("modValue".lang(row.GetName(), @ref));
                    }
                }
            }
        }
    }
}
