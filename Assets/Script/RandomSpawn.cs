using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class RandomSpawn : MonoBehaviour
{
    public Transform[] SpawnPoint;
    public GameObject[] EnemyPrefab;
    public GameObject[] Stage;
    private void Awake()
    {
        StartStage1();
    }
    IEnumerator SpawnMush1(enemyPrefab type)
    {
        yield return new WaitForSeconds(3f);
        Stage[0].SetActive(true);
        yield return new WaitForSeconds(2f);
        Stage[0].SetActive(false);
        int x = 0;
        while(x < 35)
        {
            yield return new WaitForSeconds(1f);
            SpawnUnit(type);
            x++;
        }
        yield return new WaitForSeconds(10f);
        StartStage2();
    }
    IEnumerator SpawnMush2(enemyPrefab type)
    {
        yield return new WaitForSeconds(3f);
        Stage[1].SetActive(true);
        yield return new WaitForSeconds(2f);
        Stage[1].SetActive(false);
        int x = 0;
        while (x < 35)
        {
            yield return new WaitForSeconds(1f);
            SpawnUnit(type);
            x++;
        }
        yield return new WaitForSeconds(10f);
        StartStage3();
    }
    IEnumerator SpawnMush3(enemyPrefab type)
    {
        yield return new WaitForSeconds(3f);
        Stage[2].SetActive(true);
        yield return new WaitForSeconds(2f);
        Stage[2].SetActive(false);
        int x = 0;
        while (x < 35)
        {
            yield return new WaitForSeconds(1f);
            SpawnUnit(type);
            x++;
        }
        yield return new WaitForSeconds(10f);
        StartStage4();
    }
    IEnumerator SpawnMush4(enemyPrefab type)
    {
        yield return new WaitForSeconds(3f);
        Stage[3].SetActive(true);
        yield return new WaitForSeconds(2f);
        Stage[3].SetActive(false);
        int x = 0;
        while (x < 35)
        {
            yield return new WaitForSeconds(1f);
            SpawnUnit(type);
            x++;
        }
        Stage[4].SetActive(true);
        yield return new WaitForSeconds(2f);
        Stage[3].SetActive(false);
        yield return new WaitForSeconds(2f);
        SceneManager.LoadScene("Final Boss");
    }
    private void StartStage1()
    {
        StartCoroutine(SpawnMush1(enemyPrefab.MUS1));
    }
    private void StartStage2()
    {
        StartCoroutine(SpawnMush2(enemyPrefab.MUS2));
    }
    private void StartStage3()
    {
        StartCoroutine(SpawnMush3(enemyPrefab.MUS3));
    }
    private void StartStage4()
    {
        StartCoroutine(SpawnMush4(enemyPrefab.MUS4));
    }
    public void SpawnUnit(enemyPrefab type)
    {
        int randomSpawnPoint = Random.Range(0,SpawnPoint.Length);
        Instantiate(EnemyPrefab[(int)type], SpawnPoint[randomSpawnPoint].position, transform.rotation);
    }
}
public enum enemyPrefab
{
    MUS1,
    MUS2,
    MUS3,
    MUS4,
}
