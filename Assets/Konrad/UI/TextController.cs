using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TextController : MonoBehaviour
{
    [SerializeField]
    SpriteRenderer _Tip;
    [TextArea]
    public string text;

    [SerializeField]
    AudioClip _Open;

    // Start is called before the first frame update
    void Start()
    {
        _Tip.color = Color.clear;
    }

    bool m_Open = false;
    float last_Open = 0;
    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxisRaw("Open") != 0 && last_Open == 0)
        {
            m_Open = true;
        }
        else
        {
            m_Open = false;
        }
        last_Open = Input.GetAxisRaw("Open");


        if (_Tip.color == Color.white && m_Open)
        {
            if (!TextInfoController._instance._Text.enabled)
                AudioSource.PlayClipAtPoint(_Open, transform.position);
            TextInfoController._instance.Enable(TextInfoController._instance._Text.enabled ? false : true, text);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        _Tip.color = Color.white;
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        _Tip.color = Color.clear;
        TextInfoController._instance.Enable(false);
    }
}
