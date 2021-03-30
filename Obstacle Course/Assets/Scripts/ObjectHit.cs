using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectHit : MonoBehaviour
{
    private const string PlayerTag = "Player";
    private const string HitTag = "Hit";

    private void OnCollisionEnter(Collision other)
    {
        if (!other.gameObject.CompareTag(PlayerTag)) return;
        
        GetComponent<MeshRenderer>().material.color = Color.magenta;
        gameObject.tag = HitTag;
    }
}