using System;
using System.Collections.Generic;
using _01Scripts;
using UnityEngine;


public class Storage : MonoBehaviour
{
    public Stack<Pickup> pickups = new();
    public Transform popTransform;

    public void Store(Pickup pickup)
    {
        pickups.Push(pickup);
        pickup.gameObject.SetActive(false);
    }

    public void Pop()
    {
        if(pickups.Count == 0) return;
        var pickup = pickups.Pop();
        pickup.gameObject.transform.position = popTransform.position;
        pickup.gameObject.SetActive(false);
    }
    
    
    private void OnCollisionEnter(Collision other)
    {
        var pickup = other.gameObject.GetComponent<Pickup>();
        if (pickup == null) return;
        if (pickup.isHeld) return;
        Store(pickup);
        
        
    }
}
