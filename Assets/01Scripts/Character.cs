using System;
using UnityEngine;


public class Character : MonoBehaviour
{

    [SerializeReference]
    public CharacterMovement movement = new();
    public CharacterData data;
    public Rigidbody rig;
    public Camera cam;

    private void Awake()
    {
        data = GetComponent<CharacterData>();
        rig = GetComponent<Rigidbody>();
        movement.Awake(this);
        cam = Camera.main;

    }
    
    public void Start()
    {
        movement.Start();
    }
    
    public void Update()
    {
        movement.Update();
    }

    public void FixedUpdate()
    {
        movement.FixedUpdate();
    }
    
}
