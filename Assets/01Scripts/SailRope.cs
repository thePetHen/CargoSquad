#region

using UnityEngine;

#endregion

public class SailRope : MonoBehaviour
{
    public float pullForce = 10;

    private ConfigurableJoint joint_g;

    private Rigidbody rig_g;

    private bool fasten = false;
    public Transform sailVisual;

    public float GetHoistPercentage()
    {
        var midY = joint_g.connectedAnchor.y;
        var limitLength = joint_g.linearLimit.limit;

        return 1-Mathf.InverseLerp(midY - limitLength, midY + limitLength, transform.localPosition.y);
    }
    
    public bool Fasten
    {
        get
        {
            return fasten;
        }
        set
        {
            fasten = value;
            //joint_g.yMotion = value ? ConfigurableJointMotion.Locked : ConfigurableJointMotion.Free;
        }
    }

    private void Awake()
    {
        joint_g = GetComponent<ConfigurableJoint>();
        rig_g = GetComponent<Rigidbody>();
    }

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Start()
    {
    }

    // Update is called once per frame
    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.L))
        {
            Fasten = !Fasten;
            Debug.Log($"Fasten: {Fasten}");
        }
        
        Debug.Log($"Percentage: {GetHoistPercentage()}");

        sailVisual.transform.localScale = new Vector3(493f, 850f * GetHoistPercentage(), 100f);
    }

    private void FixedUpdate()
    {
        if (Fasten)
        {
            var velocity = rig_g.linearVelocity;
            velocity.y = 0;
            rig_g.linearVelocity = velocity;
        }
        else
        {
            rig_g.AddForce(transform.up * pullForce * Time.fixedDeltaTime, ForceMode.Acceleration);
        }
    }
}