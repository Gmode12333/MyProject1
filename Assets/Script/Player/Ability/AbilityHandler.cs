using UnityEngine.UI;
using UnityEngine;
using System.Collections;

public class AbilityHandler : MonoBehaviour
{
    private const float TIME_ELAPSE = 0.02f;
    private static readonly WaitForSeconds _wait = new (TIME_ELAPSE);

    [SerializeField] Image image;
    private Coroutine _cooldownRoutine;

    public void RunCooldownAnim(float cooldownTime)
    {
        if(_cooldownRoutine != null)
        {
            StopCoroutine(_cooldownRoutine);
        }
        _cooldownRoutine = StartCoroutine(CooldownAnim(cooldownTime));
    }

    private IEnumerator CooldownAnim(float cooldownTime)
    {
        image.gameObject.SetActive(true);
        float current = cooldownTime;
        while (current > 0)
        {
            current -= TIME_ELAPSE;
            image.fillAmount = current / cooldownTime;
            yield return _wait;
        }
        image.gameObject.SetActive(false);
    }
}
