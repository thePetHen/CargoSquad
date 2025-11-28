using System;
using UnityEngine;


public class RopeStopper : MonoBehaviour
{
    public SailRope rope;

    private float fastenRotation = -140;
    public float UnfastenRotation => fastenRotation + 180;

    private void Awake()
    {
        Debug.Log($"fastenRotation: {fastenRotation}");
    }


    private void Update()
    {
        
        transform.localEulerAngles =  new Vector3(0, rope.Fasten ? fastenRotation : UnfastenRotation, 0);
        
    }
}
