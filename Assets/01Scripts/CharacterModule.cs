using UnityEngine;

public abstract class CharacterModule
{

    public Character character;

    public virtual void Awake(Character character)
    {
        this.character = character;
    }

    public abstract void Start();
    
    public abstract void Update();
    public abstract void FixedUpdate();
    public Transform transform => character.transform;
    public Rigidbody rig => character.rig;
    
    public CharacterData data => character.data;

    public Camera camera => character.cam;

}