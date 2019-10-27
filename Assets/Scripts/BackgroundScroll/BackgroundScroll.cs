using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundScroll : MonoBehaviour { 

    public float speed = 0.1f;
    private MeshRenderer meshRenderer;

    private float y_Scroll;
    // Start is called before the first frame update
    void Awake()
    {
        meshRenderer = GetComponent<MeshRenderer>();
    }

    // Update is called once per frame
    void Update()
    {
        Scroll();
    }

    void Scroll() {
        y_Scroll = Time.time * speed;

        Vector2 offset = new Vector2( 0f, y_Scroll);
        meshRenderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
