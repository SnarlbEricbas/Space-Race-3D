using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Shuttle : MonoBehaviour
{
    Rigidbody rb;
    AudioSource audioSource;
    float thrustPower = 3f;
    [SerializeField] AudioClip thrustClip;
    [SerializeField] AudioClip pickableClip;
    [SerializeField] AudioClip deathClip;
    [SerializeField] ParticleSystem thrustParticle;
    public GameObject M;
    public GameManager GM;

    enum GameState { Alive, Dying, Transitioning }
    GameState currentState = GameState.Alive;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        M = GameObject.Find("GameManager");
        GM = M.GetComponent<GameManager>();
    }

    // Update is called once per frame
    void Update()
    {
        if (currentState != GameState.Alive) { audioSource.Stop(); return; }

        if (Input.GetKey(KeyCode.Space) || Input.GetKey(KeyCode.W))
        {
            rb.AddRelativeForce(Vector3.up * thrustPower);
            if (!audioSource.isPlaying)
            {
                audioSource.PlayOneShot(thrustClip);
            }
            thrustParticle.Play();
        }
        else
        {
            audioSource.Stop();
            thrustParticle.Stop();
        }

        if (Input.GetKey(KeyCode.A))
        {
            rb.AddRelativeForce(Vector3.left);
        }
        if (Input.GetKey(KeyCode.D))
        {
            rb.AddRelativeForce(Vector3.right);
        }
    }
    private void OnCollisionEnter(Collision collision)
    {   
        if (collision.gameObject.tag == "safe"){}
        else if (collision.gameObject.tag == "Hazard")
        {
            rb.isKinematic = true;
            thrustParticle.Stop();
            audioSource.Stop();
            audioSource.PlayOneShot(deathClip);
            GM.RemoveLife();
            if (GM.Lives <= 0)
            {
                currentState = GameState.Dying;
                Invoke("GameOver", 1);
            }
            else
            {
                Invoke("LoadSameLevel", 1);
            }
        }
        else if (collision.gameObject.tag == "Finish")
        {
            currentState = GameState.Transitioning;
            thrustParticle.Stop();
            Invoke("LoadNextLevel", 1f);
        }
        else if (collision.gameObject.tag == "Pickup")
        {
            audioSource.PlayOneShot(pickableClip);
            Destroy(collision.gameObject);
            GM.AddLife();
        }
    }
    public void LoadSameLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void LoadNextLevel()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void LoadMenuLevel()
    {
        SceneManager.LoadScene(0);
    }
    public void GameOver()
    {
        SceneManager.LoadScene(12);
    }
    public void Exit()
    {
        Application.Quit();
    }
}

