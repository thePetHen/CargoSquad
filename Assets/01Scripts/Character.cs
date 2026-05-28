using System;
using _01Scripts;
using Sirenix.OdinInspector;
using UnityEngine;


public class Character : MonoBehaviour
{

    public static Character local;
    
    [SerializeReference]
    public CharacterMovement movement = new();
    [SerializeReference]
    public CharacterPickup pickup = new();
    [SerializeReference]
    public CharacterInteraction interaction = new();

    //[SerializeReference] public CharacterArms arms = new();
    public CharacterData data;
    public Rigidbody rig;
    public Camera cam;

    [Button]
    public void FetchCamera()
    {
        Camera.main.GetComponent<PChildPos>().Update();
    }
    
    private void Awake()
    {
        local = this;
        data = GetComponent<CharacterData>();
        rig = GetComponent<Rigidbody>();
        cam = Camera.main;
        
        movement.Awake(this);
        interaction.Awake(this);
        pickup.Awake(this);
        //arms.Awake(this);
        
    }
    
    public void Start()
    {
        movement.Start();
        interaction.Start();
        pickup.Start();
        //arms.Start();
    }
    
    public void Update()
    {
        movement.Update();
        interaction.Update();
        pickup.Update();
        //arms.Update();
    }
    

    public void FixedUpdate()
    {
        movement.FixedUpdate();
        interaction.FixedUpdate();
        pickup.FixedUpdate();
        //arms.FixedUpdate();
        
    }
    
}
