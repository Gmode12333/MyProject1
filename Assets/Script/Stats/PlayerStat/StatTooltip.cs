using UnityEngine;
using UnityEngine.UI;
using System.Text;
using TMPro;
using Kryz.CharacterStats;

public class StatTooltip : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI StatNameText;
    [SerializeField] TextMeshProUGUI StatModifiersLabelText;
    [SerializeField] TextMeshProUGUI StatModifiersText;
    private StringBuilder sb = new StringBuilder();

    public void ShowTooltip(CharacterStat stat, string statname)
    {
        StatNameText.text = GetStatTopText(stat , statname);

        StatModifiersText.text = GetStatModifiersText(stat);

        gameObject.SetActive(true);
    }
    public void HideTooltip()
    {
        gameObject.SetActive(false);
    }
    private string GetStatTopText(CharacterStat stat, string statname)
    {
        sb.Length = 0;
        sb.Append(statname);
        sb.Append(" ");
        sb.Append(stat.Value);
        sb.Append(" (");
        sb.Append(stat.BaseValue);

        if (stat.Value > stat.BaseValue)
        {
            sb.Append(" + ");
        }
        sb.Append(stat.Value - stat.BaseValue);
        sb.Append(")");

        return sb.ToString();
    }
    private string GetStatModifiersText(CharacterStat stat)
    {
        sb.Length = 0;

        foreach (StatsModifier mod in stat.StatModifiers)
        {
            if (sb.Length > 0)
            {
                sb.AppendLine();
            }

            if (mod.Value > 0)
            {
                sb.Append(" + ");
            }

            sb.Append(mod.Value);

            EquipableItem item = mod.Source as EquipableItem;

            if (item != null)
            {
                sb.Append(" ");
                sb.Append(item.ItemName);
            }
            else
            {
                Debug.LogError("Modifiers is not a EquipableItem!");
            }
        }
        return sb.ToString();
    }
}
