using pworld.Scripts.Extensions;
using UnityEngine;

public class BoatMover : MonoBehaviour
{
    public float speed = 10f;

    private bool move = false;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        PExt.DoInSec(3, ()=> move = true);
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (!move) return;
        var delta = transform.forward * (speed * Time.deltaTime);
        Character.local.rig.MovePosition(Character.local.rig.position + delta);
        transform.position += delta;
    }
}
