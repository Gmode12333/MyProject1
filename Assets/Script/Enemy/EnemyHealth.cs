using CongTDev.ObjectPooling;
using System;
using UnityEngine;

public class EnemyHealth : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] CharacterStats stats;
    [SerializeField] EnemyHealthBar healthBar;
    [SerializeField] Transform effectPosition;
    public int currentHealth;
    public int maxHealth;
    private bool isDead = false;
    private void Start()
    {
        maxHealth = stats.maxHeatlh;
        currentHealth = stats.maxHeatlh;
        healthBar.SetValue(maxHealth);
    }
    public void TakeDamage(int damage)
    {
        if (isDead) return;
        DamageFeedBack.Instance.DisplayDamage(effectPosition, damage);
        currentHealth -= damage;
        healthBar.SetValue((float)currentHealth/maxHealth);
        if(this.tag != "Lucid")
        {
            anim.Play("Hits");
        }

        if (currentHealth <= 0)
        {
            PlayerLevel.Instance.GainExperience(stats.ExpDrop);
            Die();
        }
    }
    public void TakeCritDamage(int damage)
    {
        if (isDead) return;

        DamageFeedBack.Instance.DisplayDamage(effectPosition, damage, true);
        currentHealth -= damage;
        healthBar.SetValue((float)currentHealth / maxHealth);

        anim.Play("Hits");

        if (currentHealth <= 0)
        {
            PlayerLevel.Instance.GainExperience(stats.ExpDrop);
            Die();
        }
    }
    public void Die()
    {
        isDead = true;
        if (this.tag != "Lucid")
        {
            anim.Play("Die");
        }
    }
}
