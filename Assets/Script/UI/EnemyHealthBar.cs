using UnityEngine;
using UnityEngine.UI;

public class EnemyHealthBar : MonoBehaviour
{
    public Image Image;

    public void SetValue(float maxhealth)
    {
        Image.fillAmount = maxhealth;
    }

}
