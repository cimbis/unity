using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Scorer : MonoBehaviour
{
    private int _hits = 0;
    private const string HitTag = "Hit";

    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.CompareTag(HitTag)) return;
    
        this._hits++;
        Debug.Log("Bumped into something, whoops!");
    }
}