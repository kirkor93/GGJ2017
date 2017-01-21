using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D), typeof(SpriteRenderer))]
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

    [Range(0.0f, 100.0f)]
    [SerializeField]
    private float _boostValue;
    [Range(0.0f, 100.0f)]
    [SerializeField]
    private float _duration;

    private bool _initialized = false;
    private Collider2D _collider;
    private SpriteRenderer _renderer;

    public void Initialize()
    {
        if (_initialized)
        {
            return;
        }

        _collider = GetComponent<Collider2D>();
        _renderer = GetComponent<SpriteRenderer>();

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
//        bat.speed += value;
        yield return new WaitForSeconds(_duration);
        //        bat.speed -= value;

        Destroy(gameObject);
    }
}
