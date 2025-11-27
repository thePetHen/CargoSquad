using System;
using pworld.Scripts;
using UnityEngine;

public class BoatRigMover : MonoBehaviour
{

    public float drag = .98f;
    public float speed = 10f;
    public float turnAmount = 0;

    private Rigidbody rig;
    public Vector3 headingDirection;
    
    [SerializeReference]
    public KeepUpright keepUpright = new KeepUpright();

    public class KeepUpright
    {
        public float spring = 15;
        public float damp = 45;

        public Vector3 DoIt(Vector3 angularVelocity, Vector3 Target, Transform transform)
        {
            var deltaAngUp = Vector3.Cross(transform.up, Vector3.up).normalized * Vector3.Angle(transform.up, Vector3.up);

            var deltaAngLookAt = Vector3.Cross(transform.forward, Target).normalized *
                                 Vector3.Angle(transform.forward, Target);

            angularVelocity = FRILerp.PLerp(angularVelocity, (deltaAngLookAt + deltaAngUp) * spring, damp, Time.fixedDeltaTime);

            return angularVelocity;
        }
    }
    

    public float turnSpeed = 5;
    
    public void Update()
    {
        var temp = 0f;

        if(Input.GetKey(KeyCode.LeftControl))
        {
            if (Input.GetKeyDown(KeyCode.W))
            {
                speed += 5;
            }
            if (Input.GetKeyDown(KeyCode.S))
            {
                speed -= 5;
            }
            if (Input.GetKeyDown(KeyCode.Q))
            {
                temp = turnSpeed;
            }
            if (Input.GetKeyDown(KeyCode.E))
            {
                temp = -turnSpeed;
            }
        }
        
        var rotator = Quaternion.AngleAxis(temp, Vector3.up);
        headingDirection = rotator * headingDirection;
        headingDirection.Normalize();
        Debug.DrawLine(transform.position, transform.position + headingDirection, Color.yellow);
    }

   


    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
        headingDirection = transform.forward;
    }

    public void FixedUpdate()
    {
        //Character.local.rig.MovePosition(Character.local.rig.position + (rig.GetPointVelocity(Character.local.rig.position) * Time.fixedDeltaTime));
        var force = transform.forward * speed;
        rig.AddForce(force, ForceMode.Acceleration);
        GetComponentInChildren<Lever>().lever.AddForce(force, ForceMode.Acceleration);
        rig.linearVelocity *= drag;
        
        rig.angularVelocity = keepUpright.DoIt(rig.angularVelocity, headingDirection, transform);
        rig.angularVelocity *= Utility.BoatDrag;
    }
    
}
