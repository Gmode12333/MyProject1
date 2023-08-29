using Kryz.CharacterStats;
using UnityEngine;

public class StatPanel : MonoBehaviour
{
    [SerializeField] StatDisplay[] statDisplays;

    private CharacterStat[] stats;

    private void OnValidate()
    {
        statDisplays = GetComponentsInChildren<StatDisplay>();
    }

    public void SetStats(params CharacterStat[] charStats)
    {
        stats = charStats;

        if (stats.Length > statDisplays.Length)
        {
            Debug.LogError("Not Enough Stat Displays!");
            return;
        }

        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].gameObject.SetActive(i < statDisplays.Length);

        }
    }

    public void UpdateStatValues()
    {
        for (int i = 0; i < stats.Length; i++)
        {
            statDisplays[i].valueText.text = stats[i].Value.ToString();
        }
    }
}
