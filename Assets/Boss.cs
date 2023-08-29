using CongTDev.ObjectPooling;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Boss : MonoBehaviour
{
    public Animator anim;
    public Prefab[] Butterfly;
    public Prefab[] LaserEffect;
    public Transform[] SpawnLaser;
    public Prefab[] Bush;
    public Transform LaserSpawn;
    [SerializeField] EnemyHealth lucidHealth;
    [SerializeField] GameObject colider1;
    [SerializeField] GameObject colider2;
    [SerializeField] GameObject WinTitle;
    void Start()
    {
        StartState1();
    }
    private void StartState1()
    {
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Lucid");
        StartCoroutine(State1());
    }
    private void StartState2()
    {
        anim.runtimeAnimatorController = Resources.Load<RuntimeAnimatorController>("Lucid 2");
        StartCoroutine(State2());
        
    }
    private void SpawnUnit(int i)
    {
        if (lucidHealth.currentHealth < 0) return;
        if (PoolManager.Get<PoolObject>(Butterfly[i], out var instance))
        {
            instance.transform.position = transform.position;
        }
    }
    private void BushEffect(int i)
    {
        if (lucidHealth.currentHealth < 0) return;
        anim.Play("State 2 Attack 3");
        StartCoroutine(CastSkill());
        lucidHealth.currentHealth += 1000;
        if (PoolManager.Get<PoolObject>(Bush[i], out var instance))
        {
            instance.transform.position = Bush[i].transform.position;
        }
    }
    private void SpawnEffect(int i)
    {
        if (lucidHealth.currentHealth < 0) return;
        anim.Play("State 2 Attack 2");
        StartCoroutine(CastSkill());
        if (PoolManager.Get<PoolObject>(LaserEffect[i], out var instance))
        {
            instance.transform.position = LaserEffect[i].transform.position;
        }
    }
    IEnumerator State1()
    {
        while (lucidHealth.currentHealth > lucidHealth.maxHealth / 2)
        {
            yield return new WaitForSeconds(2f);
            SpawnUnit(0);
            yield return new WaitForSeconds(1f);
            SpawnUnit(1);
        }
        anim.SetBool("State 1 Die", true);
        lucidHealth.GetComponent<EnemyHealth>().enabled = false;
        colider2.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        colider1.SetActive(false);
        lucidHealth.GetComponent<EnemyHealth>().enabled = true;
        StartState2();
    }
    IEnumerator CastSkill()
    {
        GetComponent<EnemyMovingSystem>().moveSpeed = 0;
        yield return new WaitForSeconds(1f);
        GetComponent<EnemyMovingSystem>().moveSpeed = 1000;
    }
    IEnumerator State2()
    {
        GetComponentInParent<EnemyMovingSystem>().enabled = true;
        while (lucidHealth.currentHealth > 0)
        {
            SpawnUnit(0);
            yield return new WaitForSeconds(2f);
            SpawnEffect(0);
            yield return new WaitForSeconds(2f);
            BushEffect(0);
            BushEffect(1);
            BushEffect(2);
            BushEffect(3);
            yield return new WaitForSeconds(2f);
            SpawnUnit(1);
            yield return new WaitForSeconds(2f);
            SpawnEffect(1);
            yield return new WaitForSeconds(2f);
        }
        anim.Play("State 2 Die");
        WinTitle.SetActive(true);
        yield return new WaitForSeconds(2.5f);
        SceneManager.LoadScene("MenuScene");
        this.gameObject.SetActive(false);
    }
}
