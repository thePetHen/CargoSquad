using System;
using UnityEngine;


public class Boat : MonoBehaviour
{
    public static Boat me;
    public Rigidbody rig;
    public BoatRigMover mover;

    public void Awake()
    {
        me = this;
        rig = GetComponent<Rigidbody>();
        mover = GetComponent<BoatRigMover>();
    }
}
