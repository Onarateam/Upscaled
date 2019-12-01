using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LayerController : MonoBehaviour {

    public static int layerId = 0;

    [SerializeField]
    int id, idStop;

    List<Collider2D> _colliders;
    List<GameObject> _gameobjects;
    bool enable = true;
    bool active = true;

    // Use this for initialization
    void Awake () {
        _colliders = new List<Collider2D>();
        foreach(Collider2D c in GetComponentsInChildren<Collider2D>())
        {
            _colliders.Add(c);
        }

        _gameobjects = new List<GameObject>();
        foreach (EnnemiController child in GetComponentsInChildren<EnnemiController>())
        {
            _gameobjects.Add(child.gameObject);
        }
    }
	
	// Update is called once per frame
	void FixedUpdate () {
		if(layerId >= idStop && enable)
        {
            DisableLayer();
            DisableGameObject();
        }
        else if(layerId <= id && !enable)
        {
            EnableLayer();
            EnableGameObject();
        }
    }

    private void DisableLayer()
    {
        for(int i = 0; i < _colliders.Count; ++i)
        {
            _colliders[i].GetComponent<BoxCollider2D>().enabled = false;
        }

        enable = false;
    }
    private void EnableLayer()
    {
        for (int i = 0; i < _colliders.Count; ++i)
        {
            _colliders[i].GetComponent<BoxCollider2D>().enabled = true;
        }

        enable = true;
    }



    private void DisableGameObject()
    {
        for (int i = 0; i < _gameobjects.Count; ++i)
        {
            _gameobjects[i].SetActive(false);
        }

        active = false;
    }
    private void EnableGameObject()
    {
        for (int i = 0; i < _gameobjects.Count; ++i)
        {
            _gameobjects[i].SetActive(true);
        }

        active = true;
    }
}
