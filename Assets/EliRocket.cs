using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EliRocket : MonoBehaviour{
    Rigidbody rigidBody;
    AudioSource audioSource;
    // Start is called before the first frame update
    void Start(){
        rigidBody = GetComponent<Rigidbody>();
        audioSource = GetComponent<AudioSource>();
        
    }

    // Update is called once per frame
    void Update(){
        ProcessInput();
        
    }
    private void ProcessInput(){
        if (Input.GetKey(KeyCode.Space)){
            // Adding force in the direction the rocket will fly
            rigidBody.AddRelativeForce(Vector3.up);

            // so audio doesn't play 
            if (!audioSource.isPlaying){
                audioSource.Play();
            }
        }

        else{
            audioSource.Stop();
        }

        
        //Rotate left or right
        if (Input.GetKey(KeyCode.A)){
            // print("Rotating Left");
            transform.Rotate(Vector3.forward);
        }
        else if (Input.GetKey(KeyCode.D)){
            // print("Rotating Right");
            transform.Rotate(-Vector3.forward);
        }

    }
}
