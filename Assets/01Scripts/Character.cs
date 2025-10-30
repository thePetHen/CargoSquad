using System;
using UnityEngine;


public class Character : MonoBehaviour
{

    public static Character local;
    
    [SerializeReference]
    public CharacterMovement movement = new();

    [SerializeReference] public CharacterArms arms = new();
    public CharacterData data;
    public Rigidbody rig;
    public Camera cam;

    private void Awake()
    {
        local = this;
        data = GetComponent<CharacterData>();
        rig = GetComponent<Rigidbody>();
        cam = Camera.main;
        
        
        movement.Awake(this);
        arms.Awake(this);
        

    }
    
    public void Start()
    {
        movement.Start();
        arms.Start();
    }
    
    public void Update()
    {
        movement.Update();
        arms.Update();
    }

    public void FixedUpdate()
    {
        movement.FixedUpdate();
        arms.FixedUpdate();
    }
    
}
