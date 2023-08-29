using CongTDev.ObjectPooling;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UIElements;

public class DamagePopup : PoolObject
{
    private static readonly WaitForEndOfFrame WaitForEndOfFrame = new WaitForEndOfFrame();

    public TextMeshProUGUI text;
    public float activeTime;
    [HideInInspector]
    public Transform followTransform;

    public void StartCount()
    {
        StartCoroutine(FollowCoroutine());
    }

    private IEnumerator FollowCoroutine()
    {
        yield return null;
        float endTime = Time.time + activeTime;
        var randomOffset = (Vector3)Random.insideUnitCircle * 2f;
        while(followTransform != null && Time.time < endTime)
        {
            transform.position = followTransform.position + randomOffset;
            yield return WaitForEndOfFrame;
        }    
        ReturnToPool();
    }
}
