using pworld.Scripts;
using UnityEngine;


public class CameraLook : MonoBehaviour
{
    public void Update()
    {
        Camera.main.transform.forward = PInput.LookToDirection(PInput.Me.mouseLook, Vector3.forward);
        //Camera.main.transform.up = PInput.LookToDirection(PInput.Me.mouseLook, Vector3.up);
    }
    
    
}
