using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    public GameObject Position1;
    public GameObject Position2;
    public float keisu;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3((Position1.gameObject.transform.position.x + Position2.gameObject.transform.position.x) / 2, (Position1.gameObject.transform.position.y + Position2.gameObject.transform.position.y) / 2, -1 * keisu * Vector3.Distance(Position1.gameObject.transform.position, Position2.gameObject.transform.position));
    }
}
