using System;
using pworld.Scripts.Extensions;
using UnityEngine;

public class Helm : MonoBehaviour
{
    public float turnPerAngle = .16f;

    public float totalTurn = 0;
    void Update()
    {
        Boat.me.mover.headingDirection = Boat.me.mover.headingDirection.RotateAroundAxis(Vector3.up, turnPerAngle * totalTurn * Time.deltaTime);
    }
    
    private void FixedUpdate()
    {
        var signedAngle = Vector3.SignedAngle(transform.up, transform.parent.TransformDirection(Vector3.up), transform.right);
        totalTurn = signedAngle;
//        Debug.Log($"totalTurn: {totalTurn}, signedAngle: {signedAngle}");
    }

    private void LateUpdate()
    {
        
    }
}
