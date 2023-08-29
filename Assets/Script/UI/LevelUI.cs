using TMPro;
using UnityEngine;

public class LevelUI : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI levelText;
    public void SetCurrentlevel(int currentlevel)
    {
        
        levelText.text = currentlevel.ToString();
    }
}
