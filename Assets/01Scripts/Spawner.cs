using UnityEngine;


public class Spawner : MonoBehaviour
{
    public GameObject prefab;
    
    public void Spawn()
    {
        var spawnPoint = transform.GetChild(0).transform;
        
        Instantiate(prefab, spawnPoint.position, spawnPoint.rotation);
    }
}
