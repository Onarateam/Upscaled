using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour {

    [SerializeField]
    protected Rigidbody2D _RGBD2D;
    [SerializeField]
    protected Collider2D _ColAttack, _ColHit;
    [SerializeField]
    protected Animator _Anim;
    [SerializeField]
    protected float move, jump, dist, distGround;
    [SerializeField]
    protected Vector3 diff, diffGround;

    [SerializeField]
    protected bool canJump, canAttack;

    Transform target;
    protected bool grounded;
    [SerializeField]
    protected LayerMask wall;

    [SerializeField]
    int life, side;
    public int damageDone = 10;

    // Use this for initialization
    void Start () {
		
	}

    // Update is called once per frame
    protected virtual void FixedUpdate ()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position + diffGround, transform.up, distGround, wall);
        Debug.DrawRay(transform.position + diffGround, transform.up * distGround, Color.red);

        if (hitGround.collider != null)
        {
            if(canJump)
                _Anim.SetBool("jump", false);

            grounded = true;
        }
        else
        {
            grounded = false;
            _Anim.SetBool("up", _RGBD2D.velocity.y > 0);
        }

        _Anim.SetBool("grounded", grounded);


        if (_Anim.GetBool("die"))
            return;


        if (!_Anim.GetBool("hurt") && !_Anim.GetBool("attack"))
            transform.localScale = new Vector3(Mathf.Abs(transform.localScale.x) * (target ? side * Mathf.Sign(transform.position.x - target.position.x) : Mathf.Sign(transform.localScale.x)), transform.localScale.y, transform.localScale.z);

        if (canJump && target && grounded)
        {
            RaycastHit2D hit = Physics2D.Raycast(transform.position + diff, transform.right* Mathf.Sign(transform.localScale.x), dist, wall);
           //Debug.DrawRay(transform.position+diff, transform.right* dist* Mathf.Sign(transform.localScale.x), Color.green);

            if (hit.collider != null)
                _Anim.SetBool("jump", true);
        }
    }

    public void Move(float f)
    {
        //transform.Translate(new Vector3(f, 0, 0));
        _RGBD2D.velocity = new Vector2(f* move * Mathf.Sign(transform.localScale.x), _RGBD2D.velocity.y);
    }

    public void Jump(float f)
    {
        _RGBD2D.velocity = new Vector2(_RGBD2D.velocity.x, f*jump);
    }


    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.name == "visible")
        {
            target = collision.transform;
            _Anim.SetBool("walk", true);
        }
        else if (collision.name == "sword")
        {
            Hitted();
        }
    }

    protected void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.name == "visible")
        {
            target = null;
            _Anim.SetBool("walk", false);
        }
    }

    protected void Hitted()
    {
        _RGBD2D.velocity = new Vector2(0, _RGBD2D.velocity.y);

        if(_ColAttack)_ColAttack.enabled = false;
        _ColHit.enabled = false;
        StartCoroutine(CameraShake.instance.Shake(0.25f, 0.25f, true));

        life--;

        GameObject go = FxController._instance.Instantiate(Random.Range(3, 5), transform, 0.5f);
        go.transform.Rotate(Vector3.forward * Random.Range(0, 360));
        go.transform.localScale = Vector3.one * 0.5f;

        if (life <= 0)
        {
            _Anim.SetBool("die", true);

            go = FxController._instance.Instantiate(Random.Range(1, 3), transform, 2);
            go.transform.Rotate(Vector3.forward * Random.Range(0, 360));
            go.transform.localScale = Vector3.one * 0.5f;
        }
        else
        {
            _Anim.SetBool("hurt", true);
        }
    }

    protected void EnableHitAttackBox()
    {
        //if (_ColAttack) _ColAttack.enabled = true;
        _ColHit.enabled = true;
    }

    public void Die() {
        gameObject.SetActive(false);
    }

    [SerializeField]
    AudioClip[] Clips;
    [SerializeField]
    AudioSource Audio;
    void PlayAudio(int id)
    {
        Audio.Stop();
        Audio.PlayOneShot(Clips[id]);
        //AudioSource.PlayClipAtPoint(Clips[id], transform.position);
    }
}
