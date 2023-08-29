using UnityEngine;

public class SetPlayerPosition : MonoBehaviour
{
    private void Awake()
    {
        GameObject.FindGameObjectWithTag("Player").gameObject.transform.position = transform.position;
    }
}
