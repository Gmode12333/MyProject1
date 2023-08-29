using CongTDev.ObjectPooling;
using System;
using UnityEngine;

public class EnemyAttack : MonoBehaviour, IPoolObject
{
    public LayerMask PlayerLayer;
    public GameObject Player;
    public Transform AttackPoint;
    public Animator anim;
    [SerializeField] int AttackDamage;
    [SerializeField] float AttackRange;
    [SerializeField] float AttackTime;
    [SerializeField]float attackDelay = 0f;
    float distance;
    [SerializeField]float range = 13f;
    private void Update()
    {
        distance = Vector2.Distance(transform.position, Player.transform.position);
        if (distance <= range && Time.time >= attackDelay)
        {
            Attack();
            NextAttack();
        }
    }
    void Attack()
    {
        anim.Play("Attack");
        Collider2D[] hitPlayer = Physics2D.OverlapCircleAll(AttackPoint.position, AttackRange, PlayerLayer);
        foreach (Collider2D hit in hitPlayer)
        {
            hit.GetComponent<Character>().TakeDamage(AttackDamage);
        }
    }
    private float NextAttack()
    {
        attackDelay = Time.time + 1f / AttackTime;
        return attackDelay;
    }
    void OnDrawGizmosSelected()
    {
        if (AttackPoint == null)
            return;

        Gizmos.DrawWireSphere(AttackPoint.position, AttackRange);
    }
    private Action<IPoolObject> _returnAction;

    public void Init(Action<IPoolObject> returnAction)
    {
        _returnAction = returnAction;
    }

    public void ReturnToPool()
    {
        if (_returnAction != null)
        {
            _returnAction.Invoke(this);
            _returnAction = null;
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
