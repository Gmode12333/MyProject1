using System;
using System.Collections;
using System.Collections.Generic;
using Random = UnityEngine.Random;
using UnityEngine;


public class BaseAttack : MonoBehaviour
{
    [SerializeField] private float attackDelay;
    [SerializeField] private float attackTime;
    [SerializeField] private int attackDamage;
    [SerializeField] private float attackRange;
    [SerializeField] private Transform attackPoint;
    [SerializeField] private LayerMask Target;
    [SerializeField] Character character;
    public List<AbilityInfo> abilities;
    [SerializeField] bool isCrit;
    [Header("Audio")]
    [SerializeField] private AudioClip _BAttack;
    [SerializeField] private AudioClip _slash;
    [SerializeField] private AudioClip _rush;
    [SerializeField] private AudioClip _combo;

    [SerializeField] private AudioClip _hits;
    [SerializeField] private AudioClip _crithits;
    private Animator anim;
    void Start()
    {
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.J) && Time.time >= attackDelay)
        {
            anim.Play("Attack");
            SoundManager.Instance.PlaySound(_BAttack);
            Attack(isCrit);
            NextAttack();
        }
        if (Input.GetKeyDown(KeyCode.K) && Time.time >= abilities[0].CoolDownTime && character.Mana >= abilities[0].ability.ManaCost)
        {
            anim.Play("Player_Slash");
            SoundManager.Instance.PlaySound(_slash);
            StartCoroutine(Slash());    
            abilities[0].CoolDownTime = Time.time + abilities[0].ability.cooldownTime + abilities[0].ability.activeTime;
        }
        if (Input.GetKeyDown(KeyCode.L) && Time.time >= abilities[1].CoolDownTime && character.Mana >= abilities[1].ability.ManaCost)
        {
            anim.Play("Player_Brandish");
            SoundManager.Instance.PlaySound(_combo);
            StartCoroutine(Brandish());
            abilities[1].CoolDownTime = Time.time + abilities[1].ability.cooldownTime + abilities[1].ability.activeTime;
        }
        if (Input.GetKeyDown(KeyCode.I) && Time.time >= abilities[2].CoolDownTime && character.Mana >= abilities[2].ability.ManaCost)
        {
            anim.Play("Player_Rush");
            SoundManager.Instance.PlaySound(_rush);
            StartCoroutine(Rush());
            abilities[2].CoolDownTime = Time.time + abilities[2].ability.cooldownTime + abilities[2].ability.activeTime;
        }
        if (Input.GetKeyDown(KeyCode.O) && Time.time >= abilities[3].CoolDownTime && character.Mana >= abilities[3].ability.ManaCost)
        {
            anim.Play("Player_Ultimate");
            SoundManager.Instance.PlaySound(_combo);
            StartCoroutine(Ultimate());
            abilities[3].CoolDownTime = Time.time + abilities[3].ability.cooldownTime + abilities[3].ability.activeTime;
        }
    }
    private void Attack(bool isCrit)
    {
        isCrit = Random.Range(0, 100f) < character.Dexterity.Value / 10;
        if (!isCrit)
        {
            NormalAttack();
        }
        else
        {
            CritAttack();
        }
    }
    private void CritAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Target);

        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<EnemyHealth>().TakeCritDamage((int)character.Strength.Value * (attackDamage * Mathf.Max(((int)character.Dexterity.Value / 50),2)));
            SoundManager.Instance.PlaySound(_crithits);
        }
    }
    private void NormalAttack()
    {
        Collider2D[] hits = Physics2D.OverlapCircleAll(attackPoint.position, attackRange, Target);

        foreach (Collider2D hit in hits)
        {
            hit.GetComponent<EnemyHealth>().TakeDamage((int)character.Strength.Value * attackDamage);
            SoundManager.Instance.PlaySound(_hits);
        }
        
    }
    private float NextAttack()
    {
        attackDelay = Time.time + 1f / attackTime;
        return attackDelay;
    }
    IEnumerator Slash()
    {
        int i = 0;
        while (i < 2)
        {
            i++;
            Attack(isCrit);
            attackRange = 5f;
            yield return new WaitForSeconds(0.3f);
        }
        attackRange = 3f;
    }
    IEnumerator Brandish()
    {
        int i = 0;
        while (i < 6)
        {
            i++;
            Attack(isCrit);
            attackRange = 5f;
            SoundManager.Instance.PlaySound(_combo);
            yield return new WaitForSeconds(0.25f);
        }
        attackRange = 3f;
    }
    IEnumerator Ultimate()
    {
        int i = 0;
        while (i < 20)
        {
            i++;
            Attack(isCrit);
            attackRange = 10f;
            SoundManager.Instance.PlaySound(_combo);
            yield return new WaitForSeconds(0.05f);
        }
        attackRange = 3f;
    }
    IEnumerator Rush()
    {
        int i = 0;
        while (i < 10)
        {
            i++;
            Attack(isCrit);
            attackRange = 10f;
            yield return new WaitForSeconds(0.01f);
        }
        attackRange = 3f;
    }
    [Serializable]
    public class AbilityInfo
    {
        public Ability ability;
        public float CoolDownTime;
    }
}
