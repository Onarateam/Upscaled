using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraShake : MonoBehaviour
{
    public static CameraShake instance;

    [SerializeField]
    GameObject player, fireworks, menuEnd;
    private void Awake()
    {
        instance = this;
    }

    public IEnumerator Shake(float duration, float magnitude, bool slowTime = false, bool isEnd = false)
    {
        float elapsed = 0;

        while(elapsed < duration)
        {
            float x = UnityEngine.Random.Range(-1f, 1f) * magnitude;
            float y = UnityEngine.Random.Range(-1f, 1f) * magnitude;

            transform.localPosition = new Vector3(x, y, 0);

            if(slowTime)
                Time.timeScale = 1+0.75f*(Mathf.Cos(Mathf.PI*2*elapsed/duration)-1)/2;
            

            elapsed += Time.deltaTime;

            yield return null;
        }

        transform.localPosition = Vector3.zero;
        Time.timeScale = 1;

        if (isEnd) {
            menuEnd.SetActive(true);
            fireworks.SetActive(true);
            Destroy(player.GetComponent<PlayerController>());
            Destroy(player.GetComponent<Rigidbody2D>());
        }
    }

    public IEnumerator ShakeEnd(float duration, float magnitude, bool slowTime, Transform pos) {         
        StartCoroutine(Shake(duration, magnitude, slowTime, true));

        float elapsed = 0;

        while (elapsed < duration) {
            float x = UnityEngine.Random.Range(-1f, 1f);
            float y = UnityEngine.Random.Range(-1f, 1f);

            transform.localPosition = new Vector3(x, y, 0);

            GameObject go = FxController._instance.Instantiate(UnityEngine.Random.Range(3, 5), pos, 0.5f);
            go.transform.Rotate(Vector3.forward * UnityEngine.Random.Range(0, 360));
            go.transform.Translate(new Vector3(x, y, 0));

            elapsed += 0.3f;

            yield return new WaitForSeconds(0.3f);
        }
    }
}
