using System.Collections;
using UnityEngine;

public class Lucid1 : MonoBehaviour
{
    Animator anim;
    public Transform[] SpawnPoint;
    public GameObject EnemyPrefab;
    public GameObject LucidgameObject;
    private void Start()
    {
        anim = GetComponent<Animator>();
        StartCoroutine(State1());
    }
    IEnumerator State1()
    {
        yield return new WaitForSeconds(5f);
        int spawnTime = 0;
        while(spawnTime <= 10)
        {
            yield return new WaitForSeconds(2f);
            SpawnUnit();
            spawnTime++;
        }
        anim.SetBool("State 1 Die", true);
    }
    public void SpawnUnit()
    {
        int randomSpawnPoint = Random.Range(0, SpawnPoint.Length);
        Instantiate(EnemyPrefab, SpawnPoint[randomSpawnPoint].position, transform.rotation);
    }
    private void Off()
    {
        LucidgameObject.SetActive(false);
    }
} 
