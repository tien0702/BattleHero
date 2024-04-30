using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    Camera cam;

    Coroutine _coroutine;

    private void Awake()
    {
        cam = Camera.main;
    }

    public void Shake(float duration, float magnitude)
    {
        if (_coroutine != null) StopCoroutine(_coroutine);
        _coroutine = StartCoroutine(StartShake(duration, magnitude));
    }

    IEnumerator StartShake(float duration, float magnitude)
    {
        Vector3 originalPos = cam.transform.position;
        float elapsed = 0.0f;
        while (elapsed < duration)
        {
            float x = Random.Range(-1f, 1f) * magnitude;
            float z = Random.Range(-1f, 1f) * magnitude;
            cam.transform.position += new Vector3(x, 0, z);
            elapsed += Time.deltaTime;
            yield return null;
        }

        cam.transform.position = originalPos;
    }

}
