namespace CatWithMillionLives
{
    internal class TraitCatsBell : Trait
    {
        public override void WriteNote(UINote n, bool identified)
        {
            if (owner != null && !owner.c_idDeity.IsEmpty() && owner.c_idDeity == EClass.pc.idFaith)
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
