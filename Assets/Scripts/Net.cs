using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Net : MonoBehaviour
{
    [Range(0.0f, 1.0f)]
    public float SpeedDecreasePercent = 0.5f;
    [Range(0.0f, 20.0f)]
    public float SpeedDecreaseTime = 3.0f;

    public bool IsDestroyedOnTrigger = true;
    private Collider2D _collider;

    private void Awake()
    {
        _collider = GetComponent<Collider2D>();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        Bat bat = other.GetComponent<Bat>();
        if (bat == null)
        {
            Debug.Log(string.Format("Net triggered with {0}. Nothing to do here.", other.gameObject.name));
            return;
        }

        _collider.enabled = false;
        StartCoroutine(SlowDownCoroutine(bat));
    }

    private IEnumerator SlowDownCoroutine(Bat bat)
    {
        bat.speed *= 1.0f - SpeedDecreasePercent;
        float timer = 0.0f;
        while (timer < SpeedDecreaseTime)
        {
            yield return null;
            timer += Time.deltaTime;
        }
        bat.speed /= 1.0f - SpeedDecreasePercent;
        if (IsDestroyedOnTrigger)
        {
            Destroy(gameObject);
        }
        else
        {
            _collider.enabled = true;
        }
    }
}
