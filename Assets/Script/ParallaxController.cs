using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ParallaxController : MonoBehaviour {

    [SerializeField]
    Transform _Player;
    [SerializeField]
    Transform[] _Layers;
    [SerializeField]
    float[] _LayersCoef;

    Vector3 startPosPlayer;
    Vector3[] startPosLayers;

    // Use this for initialization
    void Start () {
        startPosPlayer = _Player.position;

        startPosLayers = new Vector3[_Layers.Length];
        for (int i = 0; i < _Layers.Length; ++i)
        {
            startPosLayers[i] = _Layers[i].position;
        }
    }

    private void Update()
    {
        Vector3 delta = _Player.position - startPosPlayer;

        for(int i = 0; i < _Layers.Length; ++i)
        {
            _Layers[i].position = startPosLayers[i] + delta * _LayersCoef[i];
        }
    }
}
