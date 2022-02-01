using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour {
    public float backgroundSpeed = 0.25f;
    private new Renderer renderer;

    void Start() {
        renderer = GetComponent<Renderer>();
    }

    void Update() {
        renderer.material.mainTextureOffset += new Vector2(backgroundSpeed * Time.deltaTime, 0);
    }
}
