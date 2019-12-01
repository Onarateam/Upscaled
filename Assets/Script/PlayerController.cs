using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour {
    public static int retainLastLife = LifeAndTallController.currentLife;
    public static bool bouclierUp = false;///
    public static bool doubleSautUp = false;///

    [SerializeField]
    Animator _Anim;
    [SerializeField]
    Rigidbody2D _RGBD2D;
    [SerializeField]
    Collider2D _ColHit;
    [SerializeField]
    public float jumpForce, speed, maxScale, distGround, coeffSword;

    float timer = -1;

    [SerializeField]
    public Vector3 diffGround;
    bool grounded, doubleJump;
    [SerializeField]
    LayerMask wall;

    [SerializeField]
    Material _AdditiveMaterial;

    [SerializeField]
    LifeAndTallController lifeAndTallController;

    [SerializeField]
    UIController uiController;

    private Vector3 origine;

    [SerializeField]
    public Animator _Shield;
    float timerShield;
    [SerializeField]
    Image _ShieldUI;


    // Use this for initialization
    void Awake () {
        _AdditiveMaterial.SetFloat("_Enable", 0);
        timerShield = 0;

        m_Jump = false;
        m_Attack = false;
    }

    private bool m_Jump, m_Attack, m_Ranger;
    private float last_Jump, last_Attack, last_Ranger;
    // Update is called once per frame
    void Update ()
    {
        if (Input.GetAxisRaw("Jump") != 0 && last_Jump == 0) {
            m_Jump = true;
        }
        else {
            m_Jump = false;
        }
        last_Jump = Input.GetAxisRaw("Jump");

        if (Input.GetAxisRaw("Attack") != 0 && last_Attack == 0) {
            m_Attack = true;
        }
        else {
            m_Attack = false;
        }
        last_Attack = Input.GetAxisRaw("Attack");

        if (Input.GetAxisRaw("Ranger") != 0 && last_Ranger == 0) {
            m_Ranger = true;
        } else {
            m_Ranger = false;
        }
        last_Ranger = Input.GetAxisRaw("Ranger");


        ///
        if (bouclierUp) {
            if(!_ShieldUI.transform.parent.gameObject.activeSelf) _ShieldUI.transform.parent.gameObject.SetActive(true);
            timerShield += Time.deltaTime;
            _ShieldUI.fillAmount = timerShield / 10.0f;
        } else if(_ShieldUI.transform.parent.gameObject.activeSelf) {
            _ShieldUI.transform.parent.gameObject.SetActive(false);
        }
        ///

        ///
        if ((grounded || (!doubleJump && doubleSautUp)) && !_Anim.GetBool("attack") && m_Jump) {
        ///
            if (grounded)
                _Anim.SetBool("jump", true);
            else {
                _RGBD2D.velocity = new Vector2(_RGBD2D.velocity.x, 0);
                doubleJump = true;
                _Anim.SetBool("doubleJump", true);
                FxController._instance.Instantiate(0, transform, 0.6f);
            }


            if (_Anim.GetBool("sword")) _RGBD2D.AddForce(Vector2.up * jumpForce * coeffSword);
            else _RGBD2D.AddForce(Vector2.up * jumpForce * coeffSword);
        }


        if (grounded && !_Anim.GetBool("hit") && m_Attack) {
            _Anim.SetBool("attack", true);
            _Anim.SetBool("sword", true);
        }
        if (grounded && m_Ranger) {
            _Anim.SetBool("sword", !_Anim.GetBool("sword"));
        }


        if (timerShield >= 10)
        {
            if(!_Shield.GetBool("create"))
                PlayAudio(6);
            _Shield.SetBool("create", true);
            _Shield.SetBool("destruction", false);
            timerShield = 0;
        }



        if (Input.GetKeyDown(KeyCode.A) && LayerResizeController.layerId < maxScale)
        {
            levelUp();
        }
    }

    
    public void levelUp() {
        if (LayerResizeController.layerId < maxScale) {
            LayerResizeController.layerId++;
            timer = 0;
            origine = transform.position;

            retainLastLife = LifeAndTallController.currentLife;

            PlayAudio(3);

            CameraController._instance.NextClip(LayerResizeController.layerId);
        }
    }

    public void launchLastLevel() {
        LifeAndTallController.currentLife = retainLastLife;
        timer = 0;
    }
    public void restartGame() {
        LayerResizeController.layerId = 0;
        LifeAndTallController.currentLife = lifeAndTallController.maxLife;
        PlayerController.doubleSautUp = false;///
        PlayerController.bouclierUp = false;///
        timer = 0;
    }

    void FixedUpdate()
    {
        RaycastHit2D hitGround = Physics2D.Raycast(transform.position + diffGround, transform.up, distGround, wall);
        Debug.DrawRay(transform.position + diffGround, transform.up * distGround, Color.red);

        if (hitGround.collider != null) {
            grounded = true;
            doubleJump = false;
            _Anim.SetBool("jump", false);
            _Anim.SetBool("doubleJump", false);

        } else {
            grounded = false;
        }

        _Anim.SetBool("grounded", grounded);



        if (_Anim.GetBool("sword")) _RGBD2D.velocity = new Vector2(_Anim.GetBool("attack") || _Anim.GetBool("hit") || stop ? 0 : Input.GetAxis("Horizontal")* speed * coeffSword, _RGBD2D.velocity.y);
        else _RGBD2D.velocity = new Vector2(_Anim.GetBool("attack") || _Anim.GetBool("hit") || stop ? 0 : Input.GetAxis("Horizontal") * speed * coeffSword, _RGBD2D.velocity.y);

        _Anim.SetBool("run", _RGBD2D.velocity.x != 0);

        if(_RGBD2D.velocity.x != 0)
            transform.localScale = new Vector3(Mathf.Sign(_RGBD2D.velocity.x)*Mathf.Abs(transform.localScale.x), transform.localScale.y, transform.localScale.z);


        if (timer != -1)
        {
            timer += Time.deltaTime*1.5f;
            //transform.position = Vector3.Lerp(transform.position, Vector3.zero, timer);
            transform.position = new Vector3(Mathf.Lerp(origine.x, transform.position.x / 100, timer) , transform.position.y, transform.position.z);

            _AdditiveMaterial.SetFloat("_Enable", Mathf.Abs(Mathf.Cos(Mathf.Min(1, timer)*Mathf.PI*2)-1)/2);

            if (timer >= 1)
                timer = -1;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.transform.name == "attack") {
            bool isFront = Mathf.Sign(collision.transform.parent.position.x - transform.position.x) == Mathf.Sign(transform.localScale.x);
            //Debug.Log(collision.transform.parent.name);
            if (collision.transform.parent.GetComponent<EnnemiController>() != null)
                Hit(collision.transform.parent.GetComponent<EnnemiController>().damageDone, isFront);
            else if(collision.transform.parent.GetComponent<LaserController>() != null)
                Hit(collision.transform.parent.GetComponent<LaserController>().damageDone, isFront);
            else if (collision.transform.parent.GetComponent<MineController>() != null)
                Hit(collision.transform.parent.GetComponent<MineController>().damageDone, isFront);
        } else if(collision.transform.name.Contains("DeadZone")) {
            Hit(lifeAndTallController.maxLife, true, true);
        }
    }

    bool shieldBroken = false;
    void ShieldBrokenStop()
    {
        shieldBroken = false;
    }
    public void Hit(int damageDone, bool isFront, bool ignoreShield = false)
    {
        if(!ignoreShield && isFront && _Shield.GetBool("create")) {
            PlayAudio(7);
            _Shield.SetBool("create", false);
            _Shield.SetBool("destruction", true);
            shieldBroken = true;
            Invoke("ShieldBrokenStop", 1);
        }
        else {
            if (shieldBroken && isFront)
                return;

            LifeAndTallController.currentLife -= damageDone;
            _ColHit.enabled = false;
            _Anim.SetBool("attack", false);

            float scale = Mathf.Max(0.25f, Mathf.Min(1f, damageDone / 20f));
            GameObject go = FxController._instance.Instantiate(Random.Range(3, 5), transform, 0.5f);
            go.transform.Rotate(Vector3.forward * Random.Range(0, 360));
            go.transform.localScale = Vector3.one * scale;

            if (LifeAndTallController.currentLife > 0) {
                _Anim.SetBool("hit", true);
                Invoke("EnableHitBox", 1);
            } else {
                _Anim.SetBool("die", true);
                _Anim.SetBool("hit", false);
                _ColHit.enabled = false;
                this.enabled = false;
                _RGBD2D.velocity = new Vector2(0, _RGBD2D.velocity.y);


                go = FxController._instance.Instantiate(Random.Range(1, 3), transform, 2);
                go.transform.Rotate(Vector3.forward * Random.Range(0, 360));
                go.transform.localScale = Vector3.one * scale;
            }
        }
        StartCoroutine(CameraShake.instance.Shake(0.25f, 0.25f));
    }

    void playerDeathMenu() {
        uiController.launchDeathMenu();
    }

    void EnableHitBox() {
        _ColHit.enabled = true;
    }


    public void SetBoolToFalse(string name) {
        _Anim.SetBool(name, false);
    }

    bool stop = false;
    public void StopPlayer()
    {
        stop = true;
        Invoke("UnstopPlayer", 0.4f);
    }
    void UnstopPlayer()
    {
        stop = false;
    }

    [SerializeField]
    AudioClip[] Clips;
    [SerializeField]
    AudioSource Audio;
    void PlayAudio(int id)
    {
        Audio.PlayOneShot(Clips[id]);
        //AudioSource.PlayClipAtPoint(Clips[id], transform.position);
    }
}
