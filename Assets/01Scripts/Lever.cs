using System;
using pworld.Scripts;
using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;
using UnityEngine.Serialization;

public class Lever : MonoBehaviour
{
    public TextMeshPro speedShower;
    public Rigidbody lever;
    
    public float speedMul;
    public float angularDrag = .98f;

    public float MaxRotation => lever.GetComponent<ConfigurableJoint>().angularZLimit.limit;

    [Button]
    public int GetSpeedFromAngle(float angle)
    {
        int speedLevel = -3;
        
        for (int i = 0; i < 6; i++)
        {
            if (angle <= -MaxRotation + (i + 1) * StepSize)
            {
                break;
            }

            speedLevel++;
        } 
//        Debug.Log($"angle: {angle}, SpeedLevel: {speedLevel}");
        return speedLevel;
    }
    
    private float StepSize => (MaxRotation * 2f) / 7f;
    [Button]
    public float GetWantedAngle(int speedLevel)
    {
//        Debug.Log($"maxRotation {MaxRotation}");
        return speedLevel * StepSize;
    }

    public float levelLockSpring = 5;
    public float levelLockDamp = 2;
    
    private void FixedUpdate()
    {
        var angle = Vector3.SignedAngle(lever.transform.up, transform.TransformDirection(Vector3.up), lever.transform.forward);
        int speedLevel = GetSpeedFromAngle(angle);
        
        speedShower.text = speedLevel.ToString();
        
        var moverSpeed = Mathf.Pow(speedLevel * speedMul, 2f) * (speedLevel < 0 ? -1 : 1);
        
        Boat.me.mover.speed = moverSpeed;
//        Debug.Log($"moverSpeed: {moverSpeed}, speedMul {speedMul}, speedLevel {speedLevel}");
        var wantedRotation = GetWantedAngle(speedLevel);
        var deltaRotation = wantedRotation - angle;
        Debug.Log($"deltaRotation: {deltaRotation}");
        //deltaRotation = FRILerp.PLerp(0f, deltaRotation * levelLockSpring, levelLockDamp, Time.fixedDeltaTime);
        var torque = Vector3.Cross(transform.up, transform.right) * (deltaRotation * levelLockSpring);
        lever.AddTorque(torque * Time.fixedDeltaTime, ForceMode.Acceleration);
        
        lever.angularVelocity *= angularDrag;
        lever.linearVelocity *= Utility.BoatDrag;
    }
}
