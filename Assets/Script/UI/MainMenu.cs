using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    public GameObject settingMenu;
    public GameObject loadMenu;
    public void PlayGame()
    {
        SceneManager.LoadScene("Base");
    }
    public void SettingGame()
    {
        this.gameObject.SetActive(false);
        settingMenu.SetActive(true);
    }
    public void LoadGame()
    {

    }
    public void LoadMenu()
    {
        this.gameObject.SetActive(false);
        loadMenu.SetActive(true);
    }
    public void LoadClickBack()
    {
        this.gameObject.SetActive(true);
        loadMenu.SetActive(false);
    }
    public void ClickBack()
    {
        this.gameObject.SetActive(true);
        settingMenu.SetActive(false);
    }
}
