using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HeathBar : MonoBehaviour
{
    public int currentHeatlh;
    private Animator anim;
    public Slider slider;
    public CharacterStats characterStats;
    private void Start()
    {
        currentHeatlh = characterStats.maxHeatlh;
        anim = GetComponent<Animator>();
    }
    public void SetHealth()
    {
        slider.value = (float)currentHeatlh / characterStats.maxHeatlh;
    }
    public void TakeDamage(int damage)
    {
        Debug.Log("Player taken damage");
        anim.SetTrigger("Hits");
        currentHeatlh -= damage;
        SetHealth();
        if (currentHeatlh <= 0)
        {
            Die();
        }
    }
    private void Die()
    {
        anim.SetBool("Die", true);
        Destroy(gameObject, 0.5f);
    }
}
