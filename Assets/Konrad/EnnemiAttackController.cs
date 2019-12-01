using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiAttackController : EnnemiController {

    [SerializeField]
    LayerMask player;
    [SerializeField]
    protected Vector3 diffAttack;
    [SerializeField]
    float distAttack;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	protected override void FixedUpdate () {

        base.FixedUpdate();

        if (!canAttack)
            return;
        
        RaycastHit2D hitAttack = Physics2D.Raycast(transform.position + diffAttack, transform.right * Mathf.Sign(transform.localScale.x), distAttack, player);
        Debug.DrawRay(transform.position + diffAttack, transform.right * Mathf.Sign(transform.localScale.x) * distAttack, Color.red);

        if (hitAttack.collider != null)
        {
            _Anim.SetBool("attack", true);
        }
    }

    public void StopAnim(string name)
    {
        _Anim.SetBool(name, false);
    }
}
