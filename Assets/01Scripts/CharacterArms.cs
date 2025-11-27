
using pworld.Scripts;
using pworld.Scripts.Extensions;
using UnityEngine;
using UnityEngine.Serialization;

public class CharacterArms : CharacterModule
{
    public GameObject handPrefab;
    public GameObject ghostHandPrefab;
    public float maxGrabLength = 2f;
    [FormerlySerializedAs("grabSpring")] public float defaultGrabSpring = 20f;
    [FormerlySerializedAs("grabDamp")] public float defaultGrabDamp = 15f;


    public float GrabSpring => grabbable ? grabbable.spring : defaultGrabSpring;
    public float GrabDamp => grabbable ? grabbable.damp : defaultGrabDamp;
    private Grabbable grabbable;
    private GameObject iHand;
    private GameObject iGhostHand;
    private Rigidbody grabbedRig;
    private Vector3 localGrabbedPosition;

    private bool grabbing = false;
    
    
    
    public override void Start()
    {
        iGhostHand = Object.Instantiate(ghostHandPrefab, transform, true);
        iGhostHand.name = "GhostHand";
        iGhostHand.SetActive(false);
        
        
        iHand = Object.Instantiate(handPrefab, transform, true);
        iHand.name = "Hand";
        iHand.SetActive(false);
    }

    
    
    public override void Update()
    {
        if(Physics.Raycast(PExt.GetMiddleScreenRay(), out var hit, maxGrabLength, LayerMask.GetMask($"Grabbable")))
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                grabbing = true;
                iHand.transform.position = hit.point;
                grabbedRig = hit.collider.attachedRigidbody;
                grabbable = grabbedRig.GetComponent<Grabbable>();
                Debug.Log($"Grabbed {grabbedRig.name}, grabbable {grabbable}");
                localGrabbedPosition = grabbedRig.transform.InverseTransformPoint(hit.point);
            }
            else
            {
//                Debug.Log("Ghost");
                iGhostHand.gameObject.SetActive(true);
                iGhostHand.transform.position = hit.point;
            }
        }
        else
        {
            iGhostHand.SetActive(false);
        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            grabbing = false;
            grabbable = null;
            iHand.SetActive(false);
        }
        
        if (grabbing)
        {
            var ray = PExt.GetMiddleScreenRay();
            var pullToPoint = ray.GetPoint(1.5f);
            iGhostHand.SetActive(true);
            iGhostHand.transform.position = pullToPoint;
            iHand.SetActive(true);
            iHand.transform.position = grabbedRig.transform.TransformPoint(localGrabbedPosition);
        }
        
    }

    public override void FixedUpdate()
    {
        
        if (grabbing)
        {
            var ray = PExt.GetMiddleScreenRay();
            var pullToPoint = ray.GetPoint(1.5f);
            var pullForce = FRILerp.PLerp(Vector3.zero, (pullToPoint - iHand.transform.position) * GrabSpring, GrabDamp, Time.fixedDeltaTime);
            
            grabbedRig.AddForceAtPosition(pullForce, iHand.transform.position);
            Debug.DrawLine(iHand.transform.position, pullToPoint, Color.red);
            
            Debug.Log($"GrabSpring {GrabSpring}, GrabDamp {GrabDamp}");
    
        }
    }
}
