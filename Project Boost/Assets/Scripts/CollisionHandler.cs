using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    private const string FriendlyTag = "Friendly";
    private const string FinishTag = "Finish";
    private const string FuelTag = "Fuel";
    private int _currentSceneIndex;
    [SerializeField] private float levelLoadingDelay = 1f;


    void Start()
    {
        _currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void OnCollisionEnter(Collision other)
    {
        switch (other.gameObject.tag)
        {
            case FriendlyTag:
                Debug.Log("This thing is friendly " + other.gameObject.tag);
                break;
            case FinishTag:
                Debug.Log("It's done " + other.gameObject.tag);
                SuccessSequence();
                break;
            case FuelTag:
                Debug.Log("Picked up some fuel " + other.gameObject.tag);
                break;
            default:
                Debug.Log("Bukhhhhhhh " + other.gameObject.tag);
                CrashSequence();
                break;
        }
    }

    private void SuccessSequence()
    {
        Invoke(nameof(LoadNextLevel), levelLoadingDelay);
    }
    
    private void CrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        Invoke(nameof(ReloadLevel), 1f);
    }

    private void ReloadLevel()
    {
        SceneManager.LoadScene(_currentSceneIndex);
    }

    private void LoadNextLevel()
    {
        Debug.Log("Current scene index" + _currentSceneIndex);
        var sceneToLoadIndex = _currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1
            ? 0
            : _currentSceneIndex + 1;

        SceneManager.LoadScene(sceneToLoadIndex);
    }
}