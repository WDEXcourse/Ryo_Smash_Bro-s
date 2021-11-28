using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JudgePlatform : MonoBehaviour
{
    private Rigidbody rb;
    public GameObject Receiver;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Receiver.SendMessage("IsTrigger");
        }
    }
    public void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Player")
        {
            Receiver.SendMessage("NotIsTrigger");
        }
    }
}
