using TMPro;
using UnityEngine;

public class StatDisplay : MonoBehaviour
{

    public TextMeshProUGUI valueText;

    [SerializeField] StatTooltip tooltip;
    private void OnValidate()
    {
        TextMeshProUGUI[] text = GetComponentsInChildren<TextMeshProUGUI>();
        valueText = text[0];

        if (tooltip == null)
        {
            tooltip = FindObjectOfType<StatTooltip>();
        }
    }
}