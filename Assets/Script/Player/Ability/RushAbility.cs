using UnityEngine;

[CreateAssetMenu(menuName = ("Effect / Rush Ability"))]
public class RushAbility : Ability
{
    public float RushVelocity;
    public override void Active(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        rb.gravityScale = 5f;
        movement.moveSpeed += RushVelocity;
    }
    public override void BeginCoolDown(GameObject parent)
    {
        Movement movement = parent.GetComponent<Movement>();
        Rigidbody2D rb = parent.GetComponent<Rigidbody2D>();
        rb.gravityScale = 10f;
        movement.moveSpeed -= RushVelocity;
    }
}
