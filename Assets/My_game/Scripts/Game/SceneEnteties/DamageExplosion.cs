using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DamageExplosion : MonoBehaviour
{
    [SerializeField] private float explosionAnimTime = 0.417f;

    private void Update()
    {
        explosionAnimTime -= Time.deltaTime;
        if (explosionAnimTime <= 0)
        {
            Destroy(this.gameObject);
        }
    }
}
