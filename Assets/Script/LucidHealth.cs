using UnityEngine;

public class LucidHealth : MonoBehaviour
{
    [SerializeField] CharacterStats stats;
    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] Transform effectPosition;
    [SerializeField] int currentHealth;
    [SerializeField] int maxHealth;
    private void Start()
    {
        maxHealth = stats.maxHeatlh;
        currentHealth = stats.maxHeatlh;
        healthBar.SetValue(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        DamageFeedBack.Instance.DisplayDamage(effectPosition, damage);
        currentHealth -= damage;
        healthBar.SetValue((float)currentHealth / maxHealth);
    }
    public void TakeCritDamage(int damage)
    {
        DamageFeedBack.Instance.DisplayDamage(effectPosition, damage, true);
        currentHealth -= damage;
        healthBar.SetValue((float)currentHealth / maxHealth);
    }
}

