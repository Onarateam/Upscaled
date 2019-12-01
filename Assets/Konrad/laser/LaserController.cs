using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserController : MonoBehaviour
{
    [SerializeField]
    Rigidbody2D _RGBD2D;
    [SerializeField]
    Collider2D _ColAttack, _ColHit, _ColSword;
    [SerializeField]
    public int damageDone;

    [SerializeField]
    AudioSource _Audio;
    [SerializeField]
    AudioClip[] _Clips;

    // Start is called before the first frame update
    void Start()
    {
        _Audio.PlayOneShot(_Clips[1]);
    }

    // Update is called once per frame
    void Update()
    {

    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(collision.transform.name);
        if(collision.gameObject.layer == 0) Invoke("Kill", 0.1f);
        else if (collision.transform.name == "sword" ||
            (_ColAttack.enabled && collision.GetComponentInParent<PlayerController>()._Shield.GetBool("create")
            && Mathf.Sign(collision.transform.position.x - transform.position.x) == -Mathf.Sign(collision.transform.parent.localScale.x)))
        {
            _ColAttack.enabled = false;
            _ColHit.enabled = false;
            _ColSword.enabled = true;
            _RGBD2D.velocity = new Vector2(-_RGBD2D.velocity.x, _RGBD2D.velocity.y);
            _Audio.PlayOneShot(_Clips[1]);
        }
        else if((_ColAttack.enabled && collision.GetComponentInParent<PlayerController>()) ||
            (_ColSword.enabled && collision.GetComponentInParent<EnnemiController>())
            && !collision.GetComponentInParent<LaserController>())
            Invoke("Kill", 0.1f);
    }

    public void Kill()
    {
        AudioSource.PlayClipAtPoint(_Clips[0], transform.position);
        gameObject.SetActive(false);
    }
}
