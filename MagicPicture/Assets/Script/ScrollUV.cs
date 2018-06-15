using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScrollUV : MonoBehaviour {

    public float    scrollSpeed;
    public Renderer render;

    void Start()
    {
        render = GetComponent<Renderer>();
    }

    void FixedUpdate()
    {
        float offset = Time.time * scrollSpeed;
        render.material.SetTextureOffset("_MainTex", new Vector2(offset, 0));
    }
}