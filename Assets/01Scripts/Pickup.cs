using System;
using UnityEngine;

namespace _01Scripts
{
    public class Pickup : MonoBehaviour
    {
        public Rigidbody rig;
        public bool isHeld = false;

        
        private void Awake()
        {
            rig = GetComponent<Rigidbody>();
        }

        public void PickUp()
        {
            isHeld = true;
            rig.isKinematic = true;
        }
        
        public void Drop()
        {
            isHeld = false;
            rig.isKinematic = false;
        }
        
    }
}