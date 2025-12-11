namespace CatChingOfTraps.Ability
{
    internal class ActCatChingDropTrap : Act
    {
        public override bool Perform()
        {
            if (CC.pos.Installed != null || EClass._zone.IsPCFaction)
            {
                return true;
            }
            Thing thing = ThingGen.CreateFromCategory("trap", EClass._zone.DangerLv);
            Zone.ignoreSpawnAnime = true;
            EClass._zone.AddCard(thing, CC.pos).Install();
            return true;
        }
    }
}
