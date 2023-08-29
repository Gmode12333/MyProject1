using CongTDev.ObjectPooling;
using System;
using UnityEngine;


public class OncolliderAttack : MonoBehaviour
{
    [SerializeField] int damage;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.TryGetComponent<Character>(out Character health))
        {
            health.TakeDamage(damage);
        }
    }
}
