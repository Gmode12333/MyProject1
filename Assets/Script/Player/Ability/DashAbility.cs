using System.Collections;
using UnityEngine;

[CreateAssetMenu(menuName =("Effect / Dash Ability"))]
public class DashAbility : Ability
{
    public float DashVelocity;
    [SerializeField] AudioClip _dash;
    public override void Active(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        rb.gravityScale = 2f;
        movement.moveSpeed += DashVelocity;
        SoundManager.Instance.PlaySound(_dash);
    }
    public override void BeginCoolDown(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        rb.gravityScale = 10f;
        movement.moveSpeed -= DashVelocity;
    }
}
