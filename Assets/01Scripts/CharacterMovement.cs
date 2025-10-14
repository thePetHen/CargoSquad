#region

using pworld.Scripts;
using pworld.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

#endregion

public class CharacterMovement : CharacterModule
{
    public float height = 2;

    [FormerlySerializedAs("movementSpeed")]
    public float groundMovementSpeed = 100;

    public float airMovementSpeed = 20;
    public float drag = .80f;
    public float airDrag = 1f;
    public float gravity = 80f;
    public float groundCorrectionSpring = 22;

    public float groundCorrectionDamping = 80;

    public float jumpForce = 100f;

    public float maxVelocity = 9.7f;
    public float CoyoteTime = .05f;

    public float Height => height * (data.isCrouched ? .66f : 1f);

    public float Drag => data.isGrounded ? drag : airDrag;

    public float MovementSpeed =>
        data.isGrounded
            ? groundMovementSpeed / (data.isCrouched ? 2 : 1)
            : airMovementSpeed;

    // Update is called once per frame

    public override void Start()
    {
    }

    public override void Update()
    {
        if (PInput.Me.space == KEY_STATE.justPressed && (data.timeSinceGrounded < CoyoteTime || Input.GetKey(KeyCode.LeftShift)))
        {
            Debug.Log("Jump");
            rig.AddForce(Vector3.up * jumpForce, ForceMode.VelocityChange);
            var velocity = rig.linearVelocity;
            velocity = Vector3.ClampMagnitude(velocity, maxVelocity);
            rig.linearVelocity = velocity;
        }

        data.isCrouched = Input.GetKey(KeyCode.LeftControl);
    }

    void MovementForce()
    {
        var projectedVelocity = rig.linearVelocity;
        projectedVelocity = Vector3.ProjectOnPlane(projectedVelocity, Vector3.up);
        Debug.Log($"projectedVelocity {projectedVelocity.magnitude}");
        
        var forwardOnPlane = Vector3.ProjectOnPlane(camera.transform.forward, Vector3.up);
        var rightOnPlane = Vector3.ProjectOnPlane(camera.transform.right, Vector3.up);

        var preVelocity = rig.linearVelocity;
        
        
        rig.AddForce(forwardOnPlane * (PInput.Me.daeqws.z * MovementSpeed), ForceMode.Acceleration);
        rig.AddForce(rightOnPlane * (PInput.Me.daeqws.x * MovementSpeed), ForceMode.Acceleration);
        
        if(!data.isGrounded)
        {
            if (preVelocity.magnitude > maxVelocity)
            {
                Debug.Log("Velocity correction");
                var velocity = rig.linearVelocity;
                velocity = Vector3.ClampMagnitude(velocity, preVelocity.magnitude);
                rig.linearVelocity = velocity;
                Debug.Log($"postVelocity {rig.linearVelocity.magnitude}, PreVelocity {preVelocity.magnitude}");
                
            }
        }
        
    }
    
    public override void FixedUpdate()
    {
        if (Physics.Raycast(new Ray(transform.position, Vector3.down), out var hit, Height, Utility.StandableMask))
        {
            data.timeSinceGrounded = 0;
            var velocity = rig.linearVelocity;
            velocity.y = FRILerp.PLerp(velocity.y, (hit.point.y + Height - rig.position.y) * groundCorrectionSpring, groundCorrectionDamping, Time.fixedDeltaTime);
            rig.linearVelocity = velocity;
            Debug.DrawLine(transform.position, hit.point, Color.red);
        }
        else
        {
            Debug.DrawLine(transform.position, transform.position + Vector3.down * Height, Color.green);
            data.timeSinceGrounded += Time.deltaTime;
        }

        MovementForce();


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