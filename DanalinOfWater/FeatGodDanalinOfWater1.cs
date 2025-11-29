namespace DanalinOfWater
{
    internal class FeatGodDanalinOfWater1 : Feat
    {
        internal void _OnApply(int add, ElementContainer eleOwner, bool hint)
        {
            // Bonus for Insanity
            int bonusSAN = 30 * (eleOwner.Chara.GetPietyValue() * (120 + eleOwner.Chara.Evalue(1407) * 15) / 100) / 50;
            if (bonusSAN >= 50)
            {
                bonusSAN = 50;
            }
            if (bonusSAN != 0)
            {
                Note("modValue".lang(eleOwner.Chara.SAN.name, "+" + bonusSAN));
            }
            int SAN = eleOwner.Chara.SAN.GetValue();
            if (SAN >= 20)
            {
                Note(Element.GetName("negateDimByInsanity"));
                Note(Element.GetName("negateConfusionByInsanity"));
                Note(Element.GetName("negateFearByInsanity"));
            }
            void Note(string s)
            {
                if (hint)
                {
                    hints.Add(s);
                }
            }
        }
    }
}
