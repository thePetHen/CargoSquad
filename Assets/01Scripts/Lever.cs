#region

using Sirenix.OdinInspector;
using TMPro;
using UnityEngine;

#endregion

public class Lever : MonoBehaviour
{
    public TextMeshPro speedShower;
    public Rigidbody lever;

    public float speedMul;
    public float angularDrag = .98f;

    public float levelLockSpring = 5;

    public float MaxRotation => lever.GetComponent<ConfigurableJoint>().angularZLimit.limit;

    private float StepSize => MaxRotation * 2f / 7f;

    private void FixedUpdate()
    {
        var angle = Vector3.SignedAngle(lever.transform.up, transform.TransformDirection(Vector3.up), lever.transform.forward);
        var speedLevel = GetSpeedFromAngle(angle);

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

    [Button]
    public int GetSpeedFromAngle(float angle)
    {
        var speedLevel = -3;

        for (var i = 0; i < 6; i++)
        {
            if (angle <= -MaxRotation + (i + 1) * StepSize) break;

            speedLevel++;
        }

//        Debug.Log($"angle: {angle}, SpeedLevel: {speedLevel}");
        return speedLevel;
    }

    [Button]
    public float GetWantedAngle(int speedLevel)
    {
//        Debug.Log($"maxRotation {MaxRotation}");
        return speedLevel * StepSize;
    }
}