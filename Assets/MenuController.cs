using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class MenuController : MonoBehaviour
{
    Button[] _Buttons;
    public int currentId;



    // Start is called before the first frame update
    void Start()
    {
        _Buttons = GetComponentsInChildren<Button>();
        currentId = 0;
        _Buttons[currentId].Select();
    }

    int mod(int x, int m) {
        return (x % m + m) % m;
    }

    private bool m_Down, m_Up, m_Jump;
    private float last_Down, last_Up, last_Jump;
    // Update is called once per frame
    void Update() {        
    }

    private void OnGUI() {
        if (Input.GetAxisRaw("Jump") != 0 && last_Jump == 0) {
            m_Jump = true;
        } else {
            m_Jump = false;
        }
        last_Jump = Input.GetAxisRaw("Jump");

        if (m_Jump)
            _Buttons[currentId].OnSubmit(null);


        if (Input.GetAxisRaw("Vertical") > 0 && last_Up <= 0) {
            m_Up = true;
            currentId = mod(currentId - 1, _Buttons.Length);
            EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(wait());

        } else {
            m_Up = false;
        }
        last_Up = Input.GetAxisRaw("Vertical");

        if (Input.GetAxisRaw("Vertical") < 0 && last_Down >= 0) {
            m_Down = true;
            currentId = (currentId + 1) % _Buttons.Length;
            
            EventSystem.current.SetSelectedGameObject(null);
            StartCoroutine(wait());
        } else {
            m_Down = false;
        }
        last_Down = Input.GetAxisRaw("Vertical");
    }

    IEnumerator wait() {
        yield return new WaitForEndOfFrame();
        EventSystem.current.SetSelectedGameObject(_Buttons[currentId].gameObject);
    }
}
