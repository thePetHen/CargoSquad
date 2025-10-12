using System;
using UnityEngine;

public class CharacterMover : MonoBehaviour
{
    private Rigidbody rig;
    
    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    public float movementSpeed = 1;
    public float drag = .95f;
    
    // Update is called once per frame
    void Update()
    {

        if (Input.GetKey(KeyCode.W))
            input.z = 1;
        if(Input.GetKey(KeyCode.S))
            input.z = -1;
        if(Input.GetKey(KeyCode.A))
            input.x = -1;
        if(Input.GetKey(KeyCode.D))
            input.x = 1;
        

    }

    public Vector3 input;

    private void FixedUpdate()
    {
        rig.AddForce(input * (movementSpeed * Time.fixedDeltaTime), ForceMode.Acceleration);
        rig.linearVelocity = rig.linearVelocity * drag;
    }
}
