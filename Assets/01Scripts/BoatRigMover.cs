using System;
using UnityEngine;

public class BoatRigMover : MonoBehaviour
{

    public float drag = .98f;
    public float speed = 10f;

    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        Character.local.rig.MovePosition(Character.local.rig.position + rig.linearVelocity * Time.fixedDeltaTime);
        rig.linearVelocity = Vector3.right * speed;
        rig.linearVelocity *= drag;
    }
}
