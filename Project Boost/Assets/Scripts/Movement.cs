using System;
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
    [SerializeField] private AudioClip thrusterSound;
    [SerializeField] private ParticleSystem thrustParticles;


    private Rigidbody _rocketRigidbody;
    private AudioSource _audioSource;
    private bool _isRocketThrusting = false;

    void Start()
    {
        _rocketRigidbody = GetComponent<Rigidbody>();
        _audioSource = GetComponent<AudioSource>();
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
            ProcessThrust_RocketThrusting();
        }
        else if (_isRocketThrusting)
        {
            ProcessThrust_RocketNotThrusting();
        }
    }

    private void ProcessThrust_RocketThrusting()
    {
        _isRocketThrusting = true;
        _rocketRigidbody.AddRelativeForce(Vector3.up * (thrustRatio * Time.deltaTime));

        if (!_audioSource.isPlaying)
        {
            _audioSource.PlayOneShot(thrusterSound);
        }
        if (!thrustParticles.isPlaying)
        {
            thrustParticles.Play();
        }
    }

    private void ProcessThrust_RocketNotThrusting()
    {
        _isRocketThrusting = false;
        _audioSource.Stop();
        thrustParticles.Stop();
    }

    private void ProcessRotation()
    {
        if (Input.GetKey(KeyCode.A))
        {
            ApplyRotation(rotationRatio);
        }
        else if (Input.GetKey(KeyCode.D))
        {
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