using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ManaBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI manaText;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxMana(int maxmana)
    {
        slider.maxValue = maxmana;
        slider.value = maxmana;
    }
    public void SetCurrentMana(int currentmana)
    {
        slider.value = currentmana;
        manaText.text = currentmana.ToString();
    }

}
