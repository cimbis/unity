using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float levelLoadingDelay = 1f;
    [SerializeField] public AudioClip collisionSound;
    [SerializeField] public AudioClip successSound;
    [SerializeField] private ParticleSystem successParticles;
    [SerializeField] private ParticleSystem collisionParticles;
    
    
    private const string FriendlyTag = "Friendly";
    private const string FinishTag = "Finish";
    private const string FuelTag = "Fuel";

    private int _currentSceneIndex;
    private AudioSource _audioSource;
    private bool _isTransitioning = false;
    private bool _isCollisionDisabled = false;
    

    private void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
        _audioSource = GetComponent<AudioSource>();
    }

    private void Update()
    {
        RespondToDebugKeys();
    }

    private void RespondToDebugKeys()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            LoadNextLevel();
        }
        else if(Input.GetKeyDown(KeyCode.C))
        {

            _isCollisionDisabled = !_isCollisionDisabled;
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (_isTransitioning || _isCollisionDisabled) return;
        
        switch (other.gameObject.tag)
        {
            case FriendlyTag:
                break;
            case FinishTag:
                SuccessSequence();
                break;
            case FuelTag:
                break;
            default:
                CrashSequence();
                break;
        }
    }

    void LevelEndSequance()
    {
        GetComponent<Movement>().enabled = false;
        _isTransitioning = true;
    }
    
    private void SuccessSequence()
    {
        LevelEndSequance();
        _audioSource.Stop();
        _audioSource.PlayOneShot(successSound);
        successParticles.Play();
        Invoke(nameof(LoadNextLevel), levelLoadingDelay);
    }

    private void CrashSequence()
    {
        LevelEndSequance();
        _audioSource.Stop();
        _audioSource.PlayOneShot(collisionSound);
        collisionParticles.Play();
        Invoke(nameof(ReloadLevel), 1f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        var sceneToLoadIndex = _currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1
            ? 0
            : _currentSceneIndex + 1;

        SceneManager.LoadScene(sceneToLoadIndex);
    }
}