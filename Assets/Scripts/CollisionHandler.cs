using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] float delayTime = 0.5f;

    // SFX
    [SerializeField] AudioClip explosionSFX;
    [SerializeField] AudioClip finishSFX;

    // Particles
    [SerializeField] ParticleSystem explosionParticles;
    [SerializeField] ParticleSystem finishParticles;

    int currentSceneIndex;

    // transitioning to crash sequence or finish sequence
    bool isTransitioning = false;

    // for cheats
    bool collisionsDisabled = false;

    private void Start()
    {
        currentSceneIndex = SceneManager.GetActiveScene().buildIndex;
    }

    private void Update()
    {
        //cheats for the developer
        DisableObstacleCollisions();
        SkipLevel();
    }

    private void OnCollisionEnter(Collision collision)
    {
        switch (collision.gameObject.tag)
        {
            case "Start":
                Debug.Log("Starting");
                break;
            case "Finish":
                StartFinishSequence();
                break;
            //case "Fuel":
            //    Debug.Log("Picked up fuel");
            //    break;
            default:
                StartCrashSequence();
                break;
        }
    }

    void ReloadLevel()
    {
        SceneManager.LoadScene(currentSceneIndex);
    }

    void StartCrashSequence()
    {
        if (!isTransitioning && !collisionsDisabled)
        {
            FindObjectOfType<Movement>().GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(explosionSFX);

            explosionParticles.Play();

            FindObjectOfType<Movement>().enabled = false;

            Invoke("ReloadLevel", delayTime);

            isTransitioning = true;
        }
    }

    void LoadNextLevel()
    {
        // SceneManager.sceneCountInBuildSettings - 1 is the index of the last scene
        if (currentSceneIndex < SceneManager.sceneCountInBuildSettings - 1)
        {
            SceneManager.LoadScene(currentSceneIndex + 1);
        }
        else if (currentSceneIndex == SceneManager.sceneCountInBuildSettings - 1)
        {
            // load first scene (restart game)
            SceneManager.LoadScene(0);
        }
    }

    void StartFinishSequence()
    {
        if (!isTransitioning)
        {
            FindObjectOfType<Movement>().GetComponent<AudioSource>().Stop();
            GetComponent<AudioSource>().PlayOneShot(finishSFX);

            finishParticles.Play();

            FindObjectOfType<Movement>().enabled = false;

            Invoke("LoadNextLevel", delayTime);

            isTransitioning = true;
        }
    }

    // cheat for the developer
    void DisableObstacleCollisions()
    {
        if (Input.GetKeyDown(KeyCode.C))
        {
            Debug.Log("DisableObstacleCollisions() called");
            // toggle collisionsDisabled
            collisionsDisabled = !collisionsDisabled;
        }
    }

    // cheat for the developer
    void SkipLevel()
    {
        if (Input.GetKeyDown(KeyCode.N))
        {
            Debug.Log("SkipLevel() called");
            LoadNextLevel();
        }
    }
}
