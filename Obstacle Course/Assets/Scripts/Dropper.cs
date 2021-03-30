using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Dropper : MonoBehaviour
{
    [SerializeField] float timeToWait = 5f;

    private MeshRenderer rendrer;
    private new Rigidbody rigidbody;
     
    // Start is called before the first frame update
    void Start()
    {
        this.rendrer = GetComponent<MeshRenderer>();
        this.rigidbody = GetComponent<Rigidbody>();
        
        this.rendrer.enabled = false;
        this.rigidbody.useGravity = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.time > this.timeToWait)
        {
            Debug.Log("5 seconds has elapsed");
            
            this.rendrer.enabled = true;
            this.rigidbody.useGravity = true;
        }
    }
}