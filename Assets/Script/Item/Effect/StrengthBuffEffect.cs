using Kryz.CharacterStats;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Strength Buff")]
public class StrengthBuffEffect : UsableItemEffect
{
    public int StrengthBuff;
    public float Duration;
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        StatsModifier statModifier = new StatsModifier(StrengthBuff, StatModType.Flat, parentItem);
        character.Strength.AddModifier(statModifier);
        character.StartCoroutine(RemoveBuff(character, statModifier, Duration));
        character.UpdateStatValues();
    }

    public override string Getdescription()
    {
        return "Grants " + StrengthBuff + " Strength for " + Duration + " Seconds.";
    }

    public static IEnumerator RemoveBuff(Character character, StatsModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.Strength.RemoveModifier(statModifier);
        character.UpdateStatValues();
    }
}
