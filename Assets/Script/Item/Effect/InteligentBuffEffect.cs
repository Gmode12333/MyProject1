using Kryz.CharacterStats;
using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/Inteligence Buff")]
public class InteligentBuffEffect : UsableItemEffect
{
    public int InteligentBuff;
    public float Duration;
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        StatsModifier statModifier = new StatsModifier(InteligentBuff, StatModType.Flat, parentItem);
        character.Intelligence.AddModifier(statModifier);
        character.StartCoroutine(RemoveBuff(character, statModifier, Duration));
        character.UpdateStatValues();
    }

    public override string Getdescription()
    {
        return "Grants " + InteligentBuff + " Inteligence for " + Duration + " Seconds.";
    }

    public static IEnumerator RemoveBuff(Character character, StatsModifier statModifier, float duration)
    {
        yield return new WaitForSeconds(duration);
        character.Intelligence.RemoveModifier(statModifier);
        character.UpdateStatValues();
    }
}
