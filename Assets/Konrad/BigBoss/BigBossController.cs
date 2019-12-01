using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigBossController : EnnemiAttackController
{
    [SerializeField]
    float distJump, distLeap, originScale, distStopJump;
    bool jumpLeap = false;
    bool stopJump = false;

    // Start is called before the first frame update
    void Start()
    {
        distJump *= transform.localScale.x / originScale;
        distLeap *= transform.localScale.x / originScale;
        distStopJump *= transform.localScale.x / originScale;
    }

    // Update is called once per frame
      protected override void FixedUpdate()
      {
          base.FixedUpdate();
        
          RaycastHit2D hitWall = Physics2D.Raycast(transform.position + diffAttack, transform.right * Mathf.Sign(transform.localScale.x), distStopJump, wall);
         // Debug.DrawRay(transform.position + diffAttack, transform.right * Mathf.Sign(transform.localScale.x) * distStopJump, Color.black);

          if (hitWall.collider != null)
              stopJump = true;
          else
              stopJump = false;
      }

    void MoveAfterJump()
    {
        transform.Translate(distJump*Vector3.right*Mathf.Sign(transform.localScale.x));
    }

    void MoveAfterLeap()
    {
        transform.Translate(distLeap * Vector3.right * Mathf.Sign(transform.localScale.x));
    }

    private new void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);
        
        if (!stopJump && collision.name == "playerThrow" && !_Anim.GetBool("jump") && !_Anim.GetBool("leap") && Mathf.Sign(collision.transform.parent.localScale.x)==Mathf.Sign(transform.localScale.x))
        {
            if(jumpLeap)
                _Anim.SetBool("jump", true);
            else
                _Anim.SetBool("leap", true);

            jumpLeap = !jumpLeap;
        }
    }

    void End() {
        StartCoroutine(CameraShake.instance.ShakeEnd(4, 0.5f, true, transform));
    }

    void EnableHitBoxWithDelay()
    {
        Invoke("EnableHitAttackBox", 1.5f);
    }
}
