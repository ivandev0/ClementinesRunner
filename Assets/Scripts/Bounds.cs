using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bounds : MonoBehaviour
{
    void Start()
    {
        
    }

    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D col) {
        Destroy(col.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D col) {
        Destroy(col.gameObject);
    }
}
