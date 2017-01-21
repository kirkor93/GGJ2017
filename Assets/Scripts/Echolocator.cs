using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echolocator : MonoBehaviour
{
    [SerializeField]
    private Transform _shadowSphere;

    [SerializeField]
    private Light _light;

    [SerializeField]
    private Vector3 _movementDirection;

    [SerializeField]
    [Range(0.0f, 100.0f)]
    private float _movementSpeed;

    [SerializeField]
    [Range(0.0f, 10.0f)]
    private float _lifetime = 10.0f;

    private float _timer;

    private void Awake()
    {
        _movementDirection.Normalize();
    }

    private void Update()
    {
        transform.position += _movementDirection * _movementSpeed * Time.deltaTime;
        _timer += Time.deltaTime;
        if (_timer >= _lifetime)
        {
            Destroy(gameObject);
        }
    }
}
