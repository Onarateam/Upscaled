using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiThrowAttackController : EnnemiAttackController {

    [SerializeField]
    GameObject _ToThrow;
    [SerializeField]
    Vector2 _Force;
    [SerializeField]
    bool kinematic;

    private new void OnTriggerExit2D(Collider2D collision)
    {
        base.OnTriggerExit2D(collision);

        if (collision.name == "playerThrow")
        {
            _Anim.SetBool("throw", true);
        }
    }

    public void Throw()
    {
        GameObject tmp = Instantiate(_ToThrow, transform);
        tmp.transform.SetParent(transform.parent);

        if (kinematic)
            tmp.GetComponent<Rigidbody2D>().velocity = new Vector2(Mathf.Sign(transform.localScale.x) * _Force.x, _Force.y);
        else
            tmp.GetComponent<Rigidbody2D>().AddForce(new Vector2(Mathf.Sign(transform.localScale.x)*_Force.x, _Force.y));
    }
}
