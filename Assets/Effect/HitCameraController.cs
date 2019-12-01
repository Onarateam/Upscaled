using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class HitCameraController : MonoBehaviour
{
    [SerializeField]
    Camera _camera, _mainCamera;


    bool ping = true;
    public Material _DisplayMat, _AdditiveMaterial;
    CommandBuffer _CB;
    RenderTexture rTex, rTexPing, rTexPong;
    private void Awake()
    {
/*
        rTex = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        rTexPing = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);
        rTexPong = new RenderTexture(Screen.width, Screen.height, 16, RenderTextureFormat.ARGB32);

        RenderTexture rt = RenderTexture.active;
        RenderTexture.active = rTex;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rTexPing;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rTexPong;
        GL.Clear(true, true, Color.clear);
        RenderTexture.active = rt;

        _camera.targetTexture = rTex;


        _CB = new CommandBuffer();
        _CB.name = "Screen buffer";

        int tempID = Shader.PropertyToID("_Temp1");
        _CB.GetTemporaryRT(tempID, -1, -1, 24, FilterMode.Bilinear);

        _CB.Blit(BuiltinRenderTextureType.CameraTarget, tempID);

        _CB.SetGlobalTexture("_ScreenTexture", tempID);

        _CB.Blit(rTexPing, rTexPong);
        _CB.SetGlobalTexture("_PreviousTexture", rTexPong);


        _CB.SetRenderTarget(rTexPing);
        _CB.ClearRenderTarget(true, true, Color.clear); // clear before drawing to it each frame!!
        _CB.Blit(BuiltinRenderTextureType.CameraTarget, rTexPing, _AdditiveMaterial);

        _CB.SetGlobalTexture("_GrowTexture", rTexPing);

        _camera.AddCommandBuffer(CameraEvent.AfterEverything, _CB);


        SpriteFunctions(_camera, 1, 1);

        Update();*/
        SpriteFunctions(_camera, 1, 1);
    }

    // Use this for initialization
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        _camera.orthographicSize = _mainCamera.orthographicSize;
    }


    public GameObject theSprite;
    public void SpriteFunctions(Camera theCamera, int fitToScreenWidth, int fitToScreenHeight)
    {

        // If fitToScreenWidth is set to 1 then the width fits the screen width.
        // If it is set to anything over 1 then the sprite will not fit the screen width, it will be divided by that number.
        // If it is set to 0 then the sprite will not resize in that dimension.

        SpriteRenderer sr = theSprite.GetComponent<SpriteRenderer>();
        if (sr == null) return;

        theSprite.transform.localScale = new Vector3(1, 1, 1);

        float width = sr.sprite.bounds.size.x;
        float height = sr.sprite.bounds.size.y;

        float worldScreenHeight = theCamera.orthographicSize * 2.0f;
        float worldScreenWidth = worldScreenHeight / Screen.height * Screen.width;

        if (fitToScreenWidth != 0)
        {
            theSprite.transform.localScale = new Vector3(worldScreenWidth / width / fitToScreenWidth,
                theSprite.transform.localScale.y,
                theSprite.transform.localScale.z);
        }

        if (fitToScreenHeight != 0)
        {
            theSprite.transform.localScale = new Vector3(theSprite.transform.localScale.x,
                worldScreenHeight / height / fitToScreenHeight,
                theSprite.transform.localScale.z);
        }

    }
}
