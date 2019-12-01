using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading;

public class PortalController : MonoBehaviour
{
    [SerializeField]
    Animator _Anim;
    [SerializeField]
    Collider2D _hitCol;

    [SerializeField]
    bool repeat, launchRight;

    [SerializeField]
    float timeBetweenLaunch;

    private GameObject lastLaunhedObject;
    private void Start() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.name == "hit" && _Anim != null) {
            _Anim.SetBool("openPortal", true);
            _hitCol.enabled = false;
            Invoke("launchObjects", timeBetweenLaunch);
        }
    }

    private void launchObjects() {
        int count = transform.childCount;
        Vector2 vectorForce;
        if (launchRight) vectorForce = Vector2.right;
        else vectorForce = Vector2.left;

        if (repeat && count > 0 && (lastLaunhedObject == null || !lastLaunhedObject.activeSelf)) {
            int rand = Random.Range(0, count - 1);

            Transform child = transform.GetChild(rand);
            lastLaunhedObject = Instantiate(child.gameObject, transform);

            lastLaunhedObject.transform.parent = transform.parent;
            lastLaunhedObject.SetActive(true);
            lastLaunhedObject.GetComponent<Rigidbody2D>().AddForce(vectorForce * 300);
        } else if(!repeat && count > 0){
            Transform child = transform.GetChild(0);
            child.parent = transform.parent;
            child.gameObject.SetActive(true);
            child.GetComponent<Rigidbody2D>().AddForce(vectorForce * 300);
        } else if (count <= 0) {
            _Anim.SetBool("openPortal", false);
            _Anim.SetBool("closePortal", true);
        }
        Invoke("launchObjects", timeBetweenLaunch);
    }

    public void autoDestruction() {
        Destroy(gameObject);
    }
}
