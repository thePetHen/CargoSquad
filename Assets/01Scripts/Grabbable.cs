using System;
using UnityEngine;


public class Grabbable : MonoBehaviour
{
    public float spring = 20;
    public bool useBoatDrag = false;
    private Rigidbody rig;

    private void Awake()
    {
        rig = GetComponent<Rigidbody>();
    }

    public void FixedUpdate()
    {
        if (useBoatDrag)
        {
            rig.linearDamping = Utility.BoatDrag;
        }
        
    }
}
