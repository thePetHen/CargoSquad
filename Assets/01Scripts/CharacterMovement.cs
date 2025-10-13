using System;
using pworld.Scripts;
using pworld.Scripts.Extensions;
using Unity.VisualScripting;
using UnityEngine;



public class CharacterMovement : CharacterModule
{
    
    public float height = 2;
    public float movementSpeed = 1;
    public float drag = .95f;
    public float gravity = 1f;
    public float groundCorrectionSpring = 10;

    public float groundCorrectionDamping = 15;

    public float jumpForce = 100f;
    // Update is called once per frame

    public override void Start()
    {
        
    }

    public override void Update()
    {

        if (PInput.Me.space == KEY_STATE.justPressed)
        {
            rig.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
        }
    }
    

    public override void FixedUpdate()
    {
        
        
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, height, Utility.StandableMask))
        {
            data.timeSinceGrounded = 0;
            var velocity = rig.linearVelocity;
            var distance = rig.position.y - hit.point.y;
            distance = height - distance;
            distance /= height;

            distance = MathF.Pow(distance, 2);
            velocity.y = Mathf.Lerp(velocity.y, groundCorrectionSpring * distance, Time.fixedDeltaTime * groundCorrectionDamping);
            rig.linearVelocity = velocity;
        }
        else
        {
            data.timeSinceGrounded += Time.deltaTime;
        }
        
        rig.linearVelocity *= drag;
        var forwardOnPlane = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);
        var rightOnPlane = Vector3.ProjectOnPlane(camera.transform.right, Vector3.up);
        
        rig.AddForce(forwardOnPlane * (PInput.Me.daeqws.z * movementSpeed), ForceMode.Acceleration); 
        rig.AddForce(rightOnPlane * (PInput.Me.daeqws.x * movementSpeed), ForceMode.Acceleration); 
        
        if (data.timeSinceGrounded > .1f)
        {
            rig.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
    }
}