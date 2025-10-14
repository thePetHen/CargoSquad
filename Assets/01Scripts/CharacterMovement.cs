using System;
using pworld.Scripts;
using pworld.Scripts.Extensions;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Serialization;


public class CharacterMovement : CharacterModule
{
    
    public float height = 2;
    [FormerlySerializedAs("movementSpeed")] public float groundMovementSpeed = 100;
    public float airMovementSpeed = 20;
    public float drag = .95f;
    public float airDrag = 1f;
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

        data.isCrouched = Input.GetKey(KeyCode.LeftControl);
    }

    public float Height => height * (data.isCrouched ? .66f : 1f);

    public float Drag => data.isGrounded ? drag : airDrag;

    public float MovementSpeed =>
        data.isGrounded ? 
            (groundMovementSpeed / (data.isCrouched ? 2 : 1))
            : airMovementSpeed;

    public override void FixedUpdate()
    {
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out RaycastHit hit, Height, Utility.StandableMask))
        {
            
            data.timeSinceGrounded = 0;
            var velocity = rig.linearVelocity;
            velocity.y = FRILerp.PLerp(velocity.y, ((hit.point.y + Height) - rig.position.y) * groundCorrectionSpring, groundCorrectionDamping, Time.fixedDeltaTime);
            rig.linearVelocity = velocity;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * Height, Color.green);
            data.timeSinceGrounded += Time.deltaTime;
        }
        
        var forwardOnPlane = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);
        var rightOnPlane = Vector3.ProjectOnPlane(camera.transform.right, Vector3.up);
        
        rig.AddForce(forwardOnPlane * (PInput.Me.daeqws.z * MovementSpeed), ForceMode.Acceleration); 
        rig.AddForce(rightOnPlane * (PInput.Me.daeqws.x * MovementSpeed), ForceMode.Acceleration); 
        
        if (data.timeSinceGrounded > .01f)
        {
            data.isGrounded = false;
            rig.AddForce(Vector3.down * gravity, ForceMode.Acceleration);
        }
        else
        {
            data.isGrounded = true;
        }
        
        rig.linearVelocity *= Drag;
        
    }
}