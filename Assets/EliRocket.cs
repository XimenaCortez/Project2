using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;



public class EliRocket : MonoBehaviour{
    [SerializeField] float rcsThrust = 100f;
    [SerializeField] float mainThrust = 100f;
    // Audio 
    [SerializeField] AudioClip mainEngine;
    [SerializeField] AudioClip deathSound;
    [SerializeField] AudioClip finishSound;
    //Particles
    [SerializeField] ParticleSystem mainEngineParticles;
    [SerializeField] ParticleSystem deathParticles;
    [SerializeField] ParticleSystem finishParticles;


    Rigidbody rigidBody;
    AudioSource audioSource;

    enum State { Alive, Dying, Transcending }
    State state = State.Alive;

    // Start is called before the first frame update
    void Start(){
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update(){
        // Only rotate and RespondtoThrustInput when EliRocket is alive
        if (state == State.Alive){
            RespondtoThrustInput();
            RespondtoRotateInput();
        }
    }
    
    private void RespondtoRotateInput(){
        print("Rotation");
        float rotationThisFrame = rcsThrust * Time.deltaTime;

        // freeze rotation before we take manual control of rotation
        rigidBody.freezeRotation = true; 
        //Rotate left or right
        if (Input.GetKey(KeyCode.A)){
            // rotate left about the x-axis 
            transform.Rotate(Vector3.forward * rotationThisFrame);
        }
        else if (Input.GetKey(KeyCode.D)){
            // rotate left about the y-axis
            transform.Rotate(-Vector3.forward * rotationThisFrame);
        }
        //resume physics control 
        rigidBody.freezeRotation = false; 
    }

    void OnCollisionEnter(Collision collision){
        if (state != State.Alive){
            return;
        }

        switch (collision.gameObject.tag){
            case "Friendly":
                print("Friendly"); 
                break; 
            case "Finish":
                StartTransitionSequence();
                break;

            default:
                StartDyingSequence();
                break;
        }
    }

    private void RespondtoThrustInput(){
        //GetKey applies all the time anf will report the status of the rotate key 
        if (Input.GetKey(KeyCode.Space)){
            ApplyThrust();
        }
        else{
            audioSource.Stop();
            mainEngineParticles.Stop();
        }
    }

    private void StartTransitionSequence(){
        state = State.Transcending;
        audioSource.Stop();
        audioSource.PlayOneShot(finishSound);
        finishParticles.Play();
        Invoke("LoadNextScene", 1f);
    }

    private void StartDyingSequence(){
        state = State.Dying;
        audioSource.Stop();
        audioSource.PlayOneShot(deathSound);
        deathParticles.Play();
        Invoke("LoadCurrentScene", 1f);

    }

    private void LoadNextScene(){
        //TODO: allow for more than 2 levels
        SceneManager.LoadScene(1);

    }

    private void LoadCurrentScene(){
        SceneManager.LoadScene(0);
    }

    private void ApplyThrust(){
        // Adding force in the direction the rocket will fly
            rigidBody.AddRelativeForce(Vector3.up * mainThrust * Time.deltaTime);
            // so audio doesn't play 
            if (!audioSource.isPlaying){
                audioSource.PlayOneShot(mainEngine);
            }
            mainEngineParticles.Play();
    }
}