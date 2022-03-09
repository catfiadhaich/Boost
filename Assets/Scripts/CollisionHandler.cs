using UnityEngine;
using UnityEngine.SceneManagement;

public class CollisionHandler : MonoBehaviour
{
    [SerializeField] private float sceneDelay = 3f;
    [SerializeField] private AudioClip crashClip;
    [SerializeField] private AudioClip successClip;
    [SerializeField] private ParticleSystem explosion;

    private AudioSource rocketAudio;
    private bool isDone = false;

    private void Awake()
    {
        rocketAudio = GetComponent<AudioSource>();
        isDone = false;
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (!isDone)
        {
            switch (collision.gameObject.tag)
            {
                case "LaunchPad":
                    break;
                case "LandingPad":
                    StartSuccessSequence();
                    break;
                default:
                    StartCrashSequence();
                    break;
            }
        }
    }

    private void StartCrashSequence()
    {
        GetComponent<Movement>().enabled = false;
        isDone = true;
        explosion.transform.position = transform.position;
        explosion.Play();
        rocketAudio.PlayOneShot(crashClip);
        Invoke("ReloadScene", sceneDelay);
    }

    private void StartSuccessSequence()
    {
        isDone = true;
        GetComponent<Movement>().enabled = false;
        rocketAudio.PlayOneShot(successClip);
        Invoke("LoadNextScene", sceneDelay);
    }

    private void ReloadScene()
    {
        int currentScene = SceneManager.GetActiveScene().buildIndex;
        SceneManager.LoadScene(currentScene);
    }

    private void LoadNextScene()
    {
        int nextScene = SceneManager.GetActiveScene().buildIndex + 1;
        
        if (nextScene > SceneManager.sceneCount)
        {
            nextScene = 0;
        }
        isDone = false;
        SceneManager.LoadScene(nextScene);
    }
}
