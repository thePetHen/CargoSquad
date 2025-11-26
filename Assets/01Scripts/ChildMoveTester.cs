using System;
using UnityEngine;


public class ChildMoveTester : MonoBehaviour
{
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
  
        rig.MovePosition(rig.position + Boat.me.rig.GetPointVelocity(transform.position) * Time.fixedDeltaTime);
        rig.MoveRotation(rig.rotation * Quaternion.Euler(Boat.me.rig.angularVelocity * Mathf.Rad2Deg * Time.fixedDeltaTime));
        
            
        if (Input.GetKeyDown(KeyCode.F))
        {
            rig.AddForce(transform.forward * (Time.fixedDeltaTime * moveSpeed));
        }

        if (Input.GetKeyDown(KeyCode.V))
        {
            rig.AddForce(transform.forward * (Time.fixedDeltaTime * -moveSpeed));
        }
    }

    public float moveSpeed;
}
