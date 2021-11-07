using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerAction : MonoBehaviour
{
    private int frame;
    private Rigidbody rb;

    [SerializeField]
    private float MoveSpeed = 0;
    [SerializeField]
    private float JumpPower = 0;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKey(KeyCode.A))
        {
            transform.rotation = Quaternion.Euler(0, -90, 0);
            transform.position += new Vector3(-10 * MoveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position += new Vector3(10 * MoveSpeed * Time.deltaTime, 0, 0);
        }

        if (Input.GetKey(KeyCode.W))
        {
            frame++;
            if (frame > 3f)
            {
                rb.AddForce(Vector3.up * JumpPower);
                Debug.Log("大ジャンプ");
                frame = 0;
            }
        }

        if (Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log(frame);
            frame = 0;
            if (frame <= 3f)
            {
                rb.AddForce(Vector3.up * JumpPower*0.5f);
                Debug.Log("小ジャンプ");
            }
        }
    }
}
