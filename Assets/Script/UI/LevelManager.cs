using UnityEngine.SceneManagement;
using UnityEngine;

public class LevelManager : Interactable
{
    
    public override void Interact()
    {
        SceneManager.LoadSceneAsync("Map1");
    }
}
