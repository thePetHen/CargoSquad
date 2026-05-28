using pworld.Scripts.Extensions;
using UnityEngine;

namespace _01Scripts
{
    public class CharacterPickup : CharacterModule
    {

        public float maxPickUpLength = 2f;
        public bool isHolding => pickup != null;
        private Pickup pickup;
        public Vector3 holdPoint;
        public bool pickupPossible = false;
        
        public override void Start()
        {
           
        }

        public override void Update()
        {
            if(Physics.Raycast(PExt.GetMiddleScreenRay(), out var hit, maxPickUpLength))
            {
                var FoundPickup = hit.transform.GetComponent<Pickup>();
                pickupPossible = FoundPickup != null && !isHolding;
                if (Input.GetKeyDown(KeyCode.Mouse0) && pickupPossible)
                {
                    FoundPickup.PickUp();
                    pickup = FoundPickup;
                }
            }
            else
            {
                pickupPossible = false;
            }

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                if (isHolding)
                {
                    pickup.Drop();
                    pickup.rig.AddForce(Camera.main.transform.forward * tossForce, ForceMode.VelocityChange);
                    pickup = null;
                }
            }

            if (isHolding)
            {
                pickup.transform.position = Camera.main.transform.TransformPoint(holdPoint);
            }
        }

        public float tossForce = 10f;


        public override void FixedUpdate()
        {
            
        }
    }
}