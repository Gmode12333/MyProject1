using Kryz.CharacterStats;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Dexterity Buff")]
public class DexterityBuffEffect : UsableItemEffect
{
    public int DexterityBuff;
    public float Duration;
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        StatsModifier statModifier = new StatsModifier(DexterityBuff, StatModType.Flat, parentItem);
        character.Dexterity.AddModifier(statModifier);
        character.StartCoroutine(RemoveBuff(character, statModifier, Duration));
        character.UpdateStatValues();
    }

    public override string Getdescription()
    {
        return "Grants " + DexterityBuff + " Dexterity for " + Duration + " Seconds.";
    }

    public static IEnumerator RemoveBuff(Character character, StatsModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.Dexterity.RemoveModifier(statModifier);
        character.UpdateStatValues();
    }
}
