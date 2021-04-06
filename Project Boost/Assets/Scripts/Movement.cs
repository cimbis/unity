using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using UnityEngine;
using UnityEngine.PlayerLoop;
using Debug = UnityEngine.Debug;

public class Movement : MonoBehaviour
{
    [SerializeField] private float thrustRatio = 100f;
    [SerializeField] private float rotationRatio = 1f;
    private Rigidbody _rocketRigidbody;
    private AudioSource _rocketBoosterSound;

    void Start()
    {
        _rocketRigidbody = GetComponent<Rigidbody>();
        _rocketBoosterSound = GetComponent<AudioSource>();
    }

    void Update()
    {
        ProcessThrust();
        ProcessRotation();
    }

    private void ProcessThrust()
    {
        if (Input.GetKey(KeyCode.Space))
        {
            Debug.Log("Thrusting");

            _rocketRigidbody.AddRelativeForce(Vector3.up * (thrustRatio * Time.deltaTime));

            if (!_rocketBoosterSound.isPlaying)
            {
                _rocketBoosterSound.Play();
            }
        }
        else
        {
            _rocketBoosterSound.Stop();
        }
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            Debug.Log("Rotate left");
            ApplyRotation(rotationRatio);
        }
        else if (Input.GetKey(KeyCode.D))
        {
            Debug.Log("Rotate right");
            ApplyRotation(-rotationRatio);
        }
    }

    private void ApplyRotation(float rotationThisFrame)
    {
        // freeze rotation so that manual rotation is possible
        _rocketRigidbody.freezeRotation = true;
        transform.Rotate(Vector3.forward * (rotationThisFrame * Time.deltaTime));
        // unfreeze rotation so that physics system can take over
        _rocketRigidbody.freezeRotation = false;
    }
}