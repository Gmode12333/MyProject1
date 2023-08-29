using UnityEngine;

[CreateAssetMenu(menuName =("Effect / Skill"))]
public abstract class Ability : ScriptableObject
{
    public new string name;

    public float cooldownTime;
    public float activeTime;
    public int ManaCost;

    public KeyCode Key;

    public virtual void Active(GameObject gameObject)
    {

    }
    public virtual void BeginCoolDown(GameObject gameObject)
    {

    }
}
