using UnityEngine;

public class InteractionUI : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).transform.gameObject.SetActive(Character.local.interaction.possibleInteraction);
    }
}
