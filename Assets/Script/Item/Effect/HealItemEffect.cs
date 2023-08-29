using UnityEngine;
[CreateAssetMenu(menuName ="Item Effects/ Heal")]
public class HealItemEffect : UsableItemEffect
{
    public int HealthAmount;
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        if (character.Health + HealthAmount > character.MaxHealth)
        {
            if (character.Health < character.MaxHealth)
            {
                character.Health += character.MaxHealth - character.Health;
            }
            else
            {
                Debug.Log("Player Health is Full!");
            }
        }
        else
        {
            character.Health += HealthAmount;
        }
    }

    public override string Getdescription()
    {
        return "Heal for " + HealthAmount + " health.";
    }
}
