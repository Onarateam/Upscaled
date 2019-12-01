using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestController : MonoBehaviour {
    [SerializeField]
    Animator _Anim;
    [SerializeField]
    AudioClip _Open;

    private void Start() {
        foreach (Transform child in transform) {
            child.gameObject.SetActive(false);
        }
    }
    private void OnDisable() {
        if(!transform.parent.gameObject.activeSelf) Invoke("launchObjects", 0);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if ((collision.name == "hit" || collision.name == "sword") && _Anim != null) {
            if (!_Anim.GetBool("isOpen"))
                AudioSource.PlayClipAtPoint(_Open, transform.position);
            _Anim.SetBool("isOpen", true);
            Invoke("launchObjects", 0.5f);
        } 
    }

    private void launchObjects() {
        int count = transform.childCount;
        for (int i = 0; i < count; i++) {
            Transform child = transform.GetChild(0);
            if(_Anim != null) child.parent = transform.parent;
            else child.parent = transform.parent.parent;
            child.gameObject.SetActive(true);
            child.GetComponent<Rigidbody2D>().AddForce((Vector2.up + Vector2.right * Random.Range(-1.0f, 1.0f)) * 400);
        }
    }
}
