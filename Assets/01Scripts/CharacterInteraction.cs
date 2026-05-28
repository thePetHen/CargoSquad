using _01Scripts;
using pworld.Scripts.Extensions;
using UnityEngine;

public class CharacterInteraction : CharacterModule
{
    public bool possibleInteraction = false;
    
    public override void Start()
    {
        
    }

    public override void Update()
    {
        if(Physics.Raycast(PExt.GetMiddleScreenRay(), out var hit, 2))
        {
            var storage = hit.transform.GetComponent<Storage>();
            var spawner = hit.transform.GetComponent<Spawner>();
            possibleInteraction = storage || spawner;
            
            if (Input.GetKeyDown(KeyCode.E) && possibleInteraction)
            {
                if(storage)
                    storage.Pop();
                if(spawner)
                    spawner.Spawn();
            }
        }
        else
        {
            possibleInteraction = false;
        }
    }

    public override void FixedUpdate()
    {
        
    }
}
