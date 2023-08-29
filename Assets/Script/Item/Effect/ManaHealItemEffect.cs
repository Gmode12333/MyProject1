using UnityEngine;

[CreateAssetMenu(menuName = "Item Effects/ Mana Heal")]
public class ManaHealItemEffect : UsableItemEffect
{
    public int ManaAmount;
    public override void ExecuteEffect(UsableItem parentItem, Character character)
    {
        if (character.Mana + ManaAmount > character.MaxMana)
        {
            if (character.Mana < character.MaxMana)
            {
                character.Mana += character.MaxMana - character.Mana;
            }
            else
            {
                Debug.Log("Player mana is Full!");
            }
        }
        else
        {
            character.Mana += ManaAmount;
        }
    }

    public override string Getdescription()
    {
        return "Heal for " + ManaAmount + " mana.";
    }
}
