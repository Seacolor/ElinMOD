namespace CatWithMillionLives
{
    internal class TraitCatsBell : Trait
    {
        public override void WriteNote(UINote n, bool identified)
        {
            if (EClass.pc.idFaith == "cwl_cat_with_millionlives")
            {
                base.WriteNote(n, identified);
                if (identified)
                {
                    n.AddText("isEnsurePreventDeathPanalty".lang() + "_factionWide".lang(), FontColor.Myth);
                }
            }
        }
    }
}
