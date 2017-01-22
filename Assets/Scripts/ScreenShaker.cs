using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenShaker : Singleton<ScreenShaker>
{
	private AudioSource audioSource;

    [SerializeField]
    private Camera[] _affectedCameras;

    private Vector2 _dPos = Vector2.zero;
    private bool _isShaking;

    private const float DefaultShakePower = 8.0f;
    private const float DefaultShakeTime = 2.0f;

	void Start(){
		audioSource = GetComponent<AudioSource> ();
	}

    public void Shake(float power = DefaultShakePower, float time = DefaultShakeTime)
    {
		if (!_isShaking) {
			StartCoroutine (ShakeCoroutine (power, time));
			audioSource.Play ();
		} 
    }

    private IEnumerator ShakeCoroutine(float power, float time)
    {
        _isShaking = true;
        float currentTime = 0.0f;
        while (currentTime < time)
        {
            currentTime += Time.deltaTime;
            Vector2 delta = Random.insideUnitCircle * Time.deltaTime * power;
            _dPos += delta;
            for (int i = 0; i < _affectedCameras.Length; i++)
            {
                _affectedCameras[i].transform.position += new Vector3(delta.x, delta.y, 0.0f);
            }
            yield return null;
        }

        while (_dPos.sqrMagnitude > 0.001f)
        {
            Vector2 delta = -_dPos /** Time.deltaTime*/;
            _dPos += delta;
            for (int i = 0; i < _affectedCameras.Length; i++)
            {
                _affectedCameras[i].transform.position += new Vector3(delta.x, delta.y, 0.0f);
            }
            yield return null;
        }
        _isShaking = false;
    }
}
