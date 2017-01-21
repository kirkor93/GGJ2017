using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fly : MonoBehaviour
{
    public enum FlyType
    {
        SpeedUp,
        SlowDown,
        SightIncrease,
        SightDecrease,
        RandomFeature
    }

    [SerializeField]
    private FlyType _flyType;

    [Range(0.0f, 500.0f)]
    [SerializeField]
    private float _echoEnergyBoost = 100.0f;

    [Range(0.0f, 100.0f)]
    [SerializeField]
    private float _boostValue;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    private float _duration;

    private bool _initialized = false;
    private Collider2D _collider;
    private Renderer _renderer;

    private const float SpeedMin = 0.5f;
    private const float SpeedMax = 2.0f;
    private const float SightMin = 0.5f;
    private const float SightMax = 2.0f;

    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<Renderer>();

        _initialized = true;
    }

    private void Awake()
    {
        Initialize();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!_initialized)
        {
            Initialize();
        }

        Bat bat = other.GetComponent<Bat>();
        if (bat == null)
        {
            Debug.Log(string.Format("Fly triggered with {0}. Nothing to do here.", other.gameObject.name));
            return;
        }

        float value = _boostValue;
        bool movedFromRandom = false;
        bool repeat;
        FlyType selectedCase = _flyType;
        do
        {
            repeat = false;
            switch (selectedCase)
            {
                case FlyType.SlowDown:
                    {
                        value *= -1.0f;
                        goto case FlyType.SpeedUp;
                    }

                case FlyType.SpeedUp:
                    {
                        if (movedFromRandom)
                        {
                            value *= Random.Range(SpeedMin, SpeedMax);
                        }

                        StartCoroutine(SpeedCoroutine(bat, value));
                        break;
                    }

                case FlyType.SightDecrease:
                    {
                        value *= -1.0f;
                        goto case FlyType.SightIncrease;
                    }
                case FlyType.SightIncrease:
                    {
                        if (movedFromRandom)
                        {
                            value *= Random.Range(SightMin, SightMax);
                        }

                        StartCoroutine(SightCoroutine(bat, value));

                        break;
                    }
                case FlyType.RandomFeature:
                    {
                        movedFromRandom = true;
                        repeat = true;
                        selectedCase = (FlyType)Random.Range((int)FlyType.SpeedUp, (int)FlyType.RandomFeature);
                        break;
                    }
            }
        } while (repeat);

        _collider.enabled = false;
        _renderer.enabled = false;
    }

    private IEnumerator SpeedCoroutine(Bat bat, float value)
    {
        bat.speed += value;
        yield return new WaitForSeconds(_duration);
        bat.speed -= value;

        Destroy(gameObject);
    }

    private IEnumerator SightCoroutine(Bat bat, float value)
    {
        Light l = bat.GetComponentInChildren<Light>();
        l.range += value;
        yield return new WaitForSeconds(_duration);
        l.range -= value;

        Destroy(gameObject);
    }
}
