using Kryz.CharacterStats;
using UnityEngine;

public enum EquipmentType
{
    Ring,
    Amulet,
}


[CreateAssetMenu(menuName = "Items / Equippable Item")]
public class EquipableItem : Item
{
    public int StrengthBonus;
    public int IntelligentBonus;
    public int AgilityBonus;
    public int DexterityBonus;
    [Space]
    public float StrengthPercentBonus;
    public float IntelligentPercentBonus;
    public float AgilityPercentBonus;
    public float DexterityPercentBonus;
    [Space]
    public EquipmentType EquipmentType;

    public override Item GetCopy()
    {
        return Instantiate(this);
    }
    public override void Destroy()
    {
        Destroy(this);
    }

    public void Equip(Character c)
    {
        if (StrengthBonus != 0)
            c.Strength.AddModifier(new StatsModifier(StrengthBonus, StatModType.Flat, this));
        if (IntelligentBonus != 0)
            c.Intelligence.AddModifier(new StatsModifier(IntelligentBonus, StatModType.Flat, this));
        if (AgilityBonus != 0)
            c.Agility.AddModifier(new StatsModifier(AgilityBonus, StatModType.Flat, this));
        if (DexterityBonus != 0)
            c.Dexterity.AddModifier(new StatsModifier(DexterityBonus, StatModType.Flat, this));


        if (StrengthPercentBonus != 0)
            c.Strength.AddModifier(new StatsModifier(StrengthPercentBonus, StatModType.PercentMult, this));
        if (IntelligentPercentBonus != 0)
            c.Intelligence.AddModifier(new StatsModifier(IntelligentPercentBonus, StatModType.PercentMult, this));
        if (AgilityPercentBonus != 0)
            c.Agility.AddModifier(new StatsModifier(AgilityPercentBonus, StatModType.PercentMult, this));
        if (DexterityPercentBonus != 0)
            c.Dexterity.AddModifier(new StatsModifier(DexterityPercentBonus, StatModType.PercentMult, this));
    }
    public void UnEquip(Character c)
    {
        c.Strength.RemoveAllModifierFromSource(this);
        c.Intelligence.RemoveAllModifierFromSource(this);
        c.Dexterity.RemoveAllModifierFromSource(this);
        c.Agility.RemoveAllModifierFromSource(this);
    }
    public override string GetDescription()
    {
        sb.Length = 0;
        AddStats(StrengthBonus, "Strength");
        AddStats(IntelligentBonus, "Intelligence");
        AddStats(AgilityBonus, "Agility");
        AddStats(DexterityBonus, "Dexterity");

        AddStats(StrengthPercentBonus, "Strength", isPercent: true);
        AddStats(IntelligentPercentBonus, "Intelligence", isPercent: true);
        AddStats(AgilityPercentBonus, "Agility", isPercent: true);
        AddStats(DexterityPercentBonus, "Dexterity", isPercent: true);
        return sb.ToString();
    }
    public override string GetItemType()
    {
        return EquipmentType.ToString();
    }
    private void AddStats(float value, string statName, bool isPercent = false)
    {
        if (value != 0)
        {
            if (sb.Length > 0)
                sb.AppendLine();

            if (value > 0)
                sb.Append(" + ");

            if (isPercent)
            {
                sb.Append(value * 100);
                sb.Append("% ");
            }
            else
            {
                sb.Append(value);
                sb.Append(" ");
            }

            sb.Append(statName);

        }
    }
}


