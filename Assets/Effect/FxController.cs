using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxController : MonoBehaviour
{
    public static FxController _instance;

    [SerializeField]
    GameObject[] _Prefabs;
    

    // Start is called before the first frame update
    void Awake()
    {
        _instance = this;
    }

    public GameObject Instantiate(int id, Transform parent, float delayDelete) 
    {
        GameObject tmp = Instantiate(_Prefabs[id], parent);
        tmp.transform.localPosition = Vector3.zero;
        tmp.transform.SetParent(null);

        StartCoroutine(DeleteGO(tmp, delayDelete));

        return tmp;
    }

    IEnumerator DeleteGO(GameObject go, float delayTime) {
        yield return new WaitForSeconds(delayTime);
        Destroy(go);
    }
}
