using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void IsTrigger()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    public void NotIsTrigger()
    {
        GetComponent<BoxCollider>().isTrigger = false;
    }
}
