using CongTDev.ObjectPooling;
using UnityEngine;

public class DamageFeedBack : GlobalReference<DamageFeedBack>
{
    public Transform Canvas;
    public Prefab NormalDamagePrefab;
    public Prefab CritDamagePrefab;
    public void DisplayDamage(Transform target, int damage, bool isCrit = false)
    {
        var prefabToSpawn = isCrit ? CritDamagePrefab : NormalDamagePrefab;

        if(PoolManager.Get<DamagePopup>(prefabToSpawn, out var intance))
        {
            intance.transform.SetParent(Canvas);
            intance.followTransform = target;
            intance.transform.localScale = Vector3.one;
            intance.text.SetText(damage.ToString());
            intance.StartCount();
        }
    }
}
