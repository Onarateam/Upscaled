using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerResizeController : MonoBehaviour {

    public static int layerId = 0;

    [SerializeField]
    int id, idStop;
    [SerializeField]
    float coefResize;
    Vector3 scale;
    float timer = -1;
    
    [SerializeField]
    List<GameObject> _gameobjectsToEnable, _gameObjectsToDisable;
    [SerializeField]
    bool enable;

    // Use this for initialization
    void Awake () {
        scale = transform.localScale;      

        if(layerId < id)
        {
            DisableLayer();
            DisableGameObject(true);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if(layerId > id)
        {
            timer = 0;
            scale /= coefResize;
            ++id;

            if (id >= idStop)
            {
                DisableLayer();
            }
            else if (layerId <= id && !enable)
            {
                EnableGameObject();
                EnableLayer();
                enable = true;
            }
        }

        if(timer != -1)
        {
            timer += Time.deltaTime * 0.5f;
            transform.localScale = Vector3.Lerp(transform.localScale, scale, timer);

            if (timer >= 1)
            {
                timer = -1;
                if (id >= idStop) {
                    this.enable = false;
                }
            } else if (timer >= 0.1f) {
                if (id >= idStop) DisableGameObject();
            }
        }
    }

    private void DisableLayer()
    {
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = false;
        }
    }
    private void EnableLayer()
    {
        foreach (Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            c.enabled = true;
        }
    }



    private void DisableGameObject(bool start = false)
    {
        if(start)
        {
            for (int i = 0; i < _gameobjectsToEnable.Count; ++i)
            {
                _gameobjectsToEnable[i].SetActive(false);
            }
        }
        else
        {
            for (int i = 0; i < _gameObjectsToDisable.Count; ++i)
            {
                _gameObjectsToDisable[i].SetActive(false);
            }
        }
    }
    private void EnableGameObject()
    {
        for (int i = 0; i < _gameobjectsToEnable.Count; ++i)
        {
            _gameobjectsToEnable[i].SetActive(true);
        }
    }
}
