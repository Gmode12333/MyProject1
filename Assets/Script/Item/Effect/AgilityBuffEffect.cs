using Kryz.CharacterStats;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Agility Buff")]
public class AgilityBuffEffect : UsableItemEffect
{
    public int AgilityBuff;
    public float Duration;
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        StatsModifier statModifier = new StatsModifier(AgilityBuff, StatModType.Flat, parentItem);
        character.Agility.AddModifier(statModifier);
        character.StartCoroutine(RemoveBuff(character, statModifier, Duration));
        character.UpdateStatValues();
    }

    public override string Getdescription()
    {
        return "Grants " + AgilityBuff + " Agility for " + Duration + " Seconds.";
    }

    public static IEnumerator RemoveBuff(Character character, StatsModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.Agility.RemoveModifier(statModifier);
        character.UpdateStatValues();
    }
}
