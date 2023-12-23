using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraWork : MonoBehaviour
{
    public GameObject Position1;
    public GameObject Position2;
    public float keisu;
    [SerializeField]
    float betweenDistance;
    [SerializeField]
    float CameraZ;
    // Start is called before the first frame update
    void Start()
    {
        Position1 = GameObject.Find("Mario");
        Position2 = GameObject.Find("Mario2");
    }

    // Update is called once per frame
    void Update()
    {
        betweenDistance = Vector3.Distance(Position1.gameObject.transform.position, Position2.gameObject.transform.position);
        CameraZ = -1 * keisu * betweenDistance;
        CameraZ = Mathf.Clamp(CameraZ, -100, -30);
        this.transform.position = new Vector3((Position1.gameObject.transform.position.x + Position2.gameObject.transform.position.x) / 2, (Position1.gameObject.transform.position.y + Position2.gameObject.transform.position.y) / 2, CameraZ);
    }
}
