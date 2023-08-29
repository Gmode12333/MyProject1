using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] TextMeshProUGUI healthText;
    private void Start()
    {
        slider = GetComponent<Slider>();
    }
    public void SetMaxHealth(int maxhealth)
    {
        slider.maxValue = maxhealth;
        slider.value = maxhealth;
    }
    public void SetCurrentHealth(int currenthealth)
    {
        slider.value = currenthealth;
        healthText.text = currenthealth.ToString();
    }
}
