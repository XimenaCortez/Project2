using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[DisallowMultipleComponent]
public class Ocillator : MonoBehaviour{
    [SerializeField] Vector3 movementVector = new Vector3(10f, 10f, 10f);
    [SerializeField] float period = 2f;
    // TODO: remove from inspector later
    [Range(0,1)]
    [SerializeField]
    float movementFactor; // 0 for no movement, 1 for fully moved

    Vector3 startingPos;

    // Start is called before the first frame update
    void Start(){
        startingPos = transform.position; 
        // Get the obstcales starting position
    }

    // Update is called once per frame
    void Update(){
        // protect against Nan
        if (period <== Mathf.Episilon) {return;}

        float cycles = Time.time / period;
        const float tau = Mathf.PI * 2; // about 6.28
        float rawSinwave = Mathf.Sin(cycles * tau);
        print(rawSinwave);

        movementFactor = (rawSinwave / 2f) + 0.5f;

        // to calculate the change in movement
        Vector3 offset = movementVector * movementFactor;
        // setting the obstcles new position
        transform.position= startingPos + offset;
        
    }
}
