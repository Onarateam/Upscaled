using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionController : MonoBehaviour {
    [SerializeField]
    Rigidbody2D _RGBD2D;
    [SerializeField]
    Collider2D _ColTake;

    [SerializeField]
    LifeAndTallController lifeAndTallController;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    float upSize, sizePerma;
    [SerializeField]
    int upLife;

    [SerializeField]
    bool levelUp, doubleSaut, bouclier;

    [SerializeField]
    AudioClip _TakeClip;

    private void Start() {
        if (lifeAndTallController == null) lifeAndTallController = GameObject.Find("Player").GetComponent<LifeAndTallController>();
        if (playerController == null) playerController = GameObject.Find("Player").GetComponent<PlayerController>();
    }

    private void OnEnable() {
        Invoke("stopAndActiveCollider", 0.5f);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        //Debug.Log(collision.name);
        if (collision.name == "hit") {
            //Debug.Log(upLife + "    " + lifeAndTallController.currentLife + "    " + lifeAndTallController.maxLife);
            if (sizePerma > 0 || (upLife > 0 && LifeAndTallController.currentLife < lifeAndTallController.maxLife) || (upSize > 0 && lifeAndTallController.currentSize < lifeAndTallController.maxSize)) {
                LifeAndTallController.currentLife += upLife;
                lifeAndTallController.currentSize += upSize;
                if (sizePerma > 0) {
                    lifeAndTallController.minSize = sizePerma;
                    lifeAndTallController.currentSize = sizePerma;
                }

                AudioSource.PlayClipAtPoint(_TakeClip, transform.position);

                Destroy(gameObject);
            }

            if (levelUp) {
                lifeAndTallController.minSize = 1;
                lifeAndTallController.currentSize = 1;
                playerController.levelUp();
                Destroy(gameObject);
            }

            if (doubleSaut) {
                PlayerController.doubleSautUp = true;
                Destroy(gameObject);
            }
            if (bouclier) {
                PlayerController.bouclierUp = true;
                Destroy(gameObject);
            }
        }
    }

    private void stopAndActiveCollider() {
        _RGBD2D.constraints = RigidbodyConstraints2D.FreezePositionX;
        _ColTake.enabled = true;
    }
}
