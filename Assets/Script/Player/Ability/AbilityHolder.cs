using CongTDev.ObjectPooling;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AbilityHolder : MonoBehaviour
{
    public Character character;
    public List<AbilityInfo> Abilities;
    private void Update()
    {
        for (int i = 0; i < Abilities.Count; i++)
        {
            if (Input.GetKeyDown(Abilities[i].ability.Key))
            {
                AbilityActive(i);
            }
        }
    }
    private void AbilityActive(int i)
    {
        if (Time.time < Abilities[i].CoolDownTime)
            return;

        if (Abilities[i].ability.ManaCost > character.Mana)
            return;

        SpawnEffect(i);
        character.Mana -= Abilities[i].ability.ManaCost;
        Abilities[i].ability.Active(gameObject);
        StartCoroutine(AbilityCoolDown(Abilities[i]));
        Abilities[i].CoolDownTime = Time.time + Abilities[i].ability.cooldownTime + Abilities[i].ability.activeTime;
        Abilities[i].handler.RunCooldownAnim(Abilities[i].ability.cooldownTime);
    }
    IEnumerator AbilityCoolDown(AbilityInfo abilityInfo)
    {
        yield return new WaitForSeconds(abilityInfo.ability.activeTime);
        abilityInfo.ability.BeginCoolDown(gameObject);
    }
    private void SpawnEffect(int i)
    {
        if (PoolManager.Get<PoolObject>(Abilities[i].prefab, out var instance))
        {
            instance.transform.position = Abilities[i].transform.position;
            instance.transform.SetParent(transform);
            instance.transform.localScale = Vector3.one;
        }
    }

    [Serializable]
    public class AbilityInfo
    {
        public Ability ability;
        public AbilityHandler handler;
        public Prefab prefab;
        public Transform transform;
        public float CoolDownTime;
    }
}
