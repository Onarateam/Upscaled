using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextInfoController : MonoBehaviour
{
    [SerializeField]
    public Text _Text;
    [SerializeField]
    Image _Img;

    public static TextInfoController _instance;
    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
        Enable(false);
    }

    public void Enable(bool b, string text = null)
    {
        _Text.enabled = b;
        _Img.enabled = b;
        _Text.text = text;
    }
}
