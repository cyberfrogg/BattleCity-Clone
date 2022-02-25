using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnPoint : MonoBehaviour
{
    public GameObject[] objects;

    // Start is called before the first frame update
    void Start()
    {
        GameObject tile = (GameObject)Instantiate(objects[Random.Range(0, objects.Length)], transform.position, Quaternion.identity);
        tile.transform.parent = transform;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
