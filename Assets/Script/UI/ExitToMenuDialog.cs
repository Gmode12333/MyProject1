using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitToMenuDialog : MonoBehaviour
{
    [SerializeField] GameObject ExitDialog;
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            ExitDialog.SetActive(true);
        }
    }
    public void ExitToMenu()
    {
        SceneManager.LoadScene("MenuScene");
    }
}
