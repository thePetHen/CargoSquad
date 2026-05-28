using UnityEngine;

public class PickupUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).transform.gameObject.SetActive(Character.local.pickup.pickupPossible);
    }
}

