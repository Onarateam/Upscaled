using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MineController : MonoBehaviour
{
    [SerializeField]
    Animator _Anim;
    [SerializeField]
    Rigidbody2D _RGBD2D;
    public int damageDone;

    [SerializeField]
    AudioSource _Audio;
    [SerializeField]
    AudioClip[] _Clips;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _Anim.SetBool("explode", true);
        _RGBD2D.velocity = Vector2.zero;
        _Audio.PlayOneShot(_Clips[0]);
    }

    public void Kill()
    {
        gameObject.SetActive(false);
    }
}
