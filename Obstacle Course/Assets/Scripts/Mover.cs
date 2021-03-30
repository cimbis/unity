using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Mover : MonoBehaviour
{
    [SerializeField] private float xValue;
    [SerializeField] private float zValue;
    [SerializeField] float yValue = 0;

    [SerializeField] private float movementSpeed = 10f;

    // Start is called before the first frame update
    void Start()
    {
        this.PrintInstructions();
    }

    // Update is called once per frame
    void Update()
    {
        this.MovePlayer();
    }

    void PrintInstructions()
    {
        Debug.Log("Welcome to the game!");
        Debug.Log("Use WASD or Arrow Keys");
        Debug.Log("Don't hit the walls!");
    }

    void MovePlayer()
    {
        this.xValue = Input.GetAxis("Horizontal") * Time.deltaTime * this.movementSpeed;
        this.zValue = Input.GetAxis("Vertical") * Time.deltaTime * this.movementSpeed;

        transform.Translate(this.xValue, this.yValue, this.zValue);
    }
}