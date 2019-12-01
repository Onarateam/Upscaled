using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LifeAndTallController : MonoBehaviour {
    public static int currentLife = 100;

    [SerializeField]
    Camera mainCam;
    [SerializeField]
    CameraController camController;
    [SerializeField]
    PlayerController playerController;
    [SerializeField]
    Image lifeBar, sizeBar;

    private float origineFOV, origineDiffY, origineDistGround, origineJumpForce, origineSpeed;

    [SerializeField]
    public float minSize, maxSize, currentSize, coeffPerteTaille;
    [SerializeField]
    public int maxLife;
    Vector3 origineDiffGround;
    private void Start() {
        origineFOV = mainCam.orthographicSize;
        origineDiffY = camController.diffY;
        origineDistGround = playerController.distGround;
        origineDiffGround = playerController.diffGround;
        origineJumpForce = playerController.jumpForce;
        origineSpeed = playerController.speed;
    }

    // Update is called once per frame
    void FixedUpdate() {
        if (currentSize <= maxSize) {
            if (currentSize == 1) {
                transform.localScale = new Vector3(currentSize * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), currentSize, currentSize);
                mainCam.orthographicSize = origineFOV * transform.localScale.y;
            } else if (currentSize > 1) {
                float signeScale = (transform.localScale.x / Mathf.Abs(transform.localScale.x));
                float pas = Time.deltaTime * 0.8f;
                transform.localScale = new Vector3(Mathf.Lerp(transform.localScale.x, currentSize * signeScale, pas), Mathf.Lerp(transform.localScale.y, currentSize, pas), Mathf.Lerp(transform.localScale.z, currentSize, pas));
                mainCam.orthographicSize = Mathf.Lerp(mainCam.orthographicSize, origineFOV * (1 + (transform.localScale.y - 1) / 5), pas);
            }

            camController.diffY = origineDiffY / transform.localScale.y;
            playerController.distGround = origineDistGround * transform.localScale.y;
            playerController.diffGround = origineDiffGround * transform.localScale.y;

            float coeffProperties = (1 + (transform.localScale.y - 1) / 10);
            playerController.speed = origineSpeed * coeffProperties;
            playerController.jumpForce = origineJumpForce * coeffProperties;
            sizeBar.fillAmount = (transform.localScale.y - 1) / (maxSize - 1);

            /*
            transform.localScale = new Vector3(currentSize * (transform.localScale.x / Mathf.Abs(transform.localScale.x)), currentSize, currentSize);
            camController.diffY = origineDiffY / currentSize;
            playerController.distGround = origineDistGround * currentSize;
            playerController.diffGround = origineDiffGround * currentSize;

            float coeffProperties = (1 + (currentSize - 1) / 10);
            playerController.speed = origineSpeed * coeffProperties;
            playerController.jumpForce = origineJumpForce * coeffProperties;
            */
            if (currentSize > minSize) currentSize -= Time.deltaTime * coeffPerteTaille;
        } else if(currentSize < minSize) {
            currentSize = minSize;
            sizeBar.fillAmount = 0;
        } else if(currentSize > maxSize) {
            currentSize = maxSize;
            sizeBar.fillAmount = 1;
        }

        if (currentLife < 1) {
            currentLife = 0;
            lifeBar.fillAmount = 0;
        } else if (currentLife > maxLife) {
            currentLife = maxLife;
            lifeBar.fillAmount = 1;
        } else {
            lifeBar.fillAmount = (float)currentLife / (float)maxLife;
        }
    }
}
