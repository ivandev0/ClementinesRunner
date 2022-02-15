using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackgroundLoop : MonoBehaviour {
    private new Renderer renderer;

    void Start() {
        renderer = GetComponent<Renderer>();
    }

    void Update() {
        if (GameManager.Instance.GameIsOver()) return;
        renderer.material.mainTextureOffset += new Vector2(GameManager.Instance.gameSpeed * Time.deltaTime * 0.25f, 0);
    }
}
