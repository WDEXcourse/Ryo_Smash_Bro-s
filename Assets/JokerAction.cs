using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerAction : MonoBehaviour
{
    private float pushTime;
    private Rigidbody rb;
    [SerializeField]
    int JumpCount = 0;
    [SerializeField]
    private float MoveSpeed = 0;
    [SerializeField]
    private float JumpPower = 0;
    [SerializeField]
    private float ShortJumpPower = 0;
    [SerializeField]
    private float GravityPower = 0;
    private Animator anim;
    private AnimatorStateInfo stateInfor;
    bool pushAD;
    public GameObject Receiver1;
    public GameObject Receiver2;
    public GameObject Receiver3;
    bool downInput;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.S))
        {
            downInput = true;
        }

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
            pushTime+=Time.deltaTime;
        }
        else if (JumpCount <= 1 && Input.GetKeyUp(KeyCode.W))
        {
            Debug.Log(pushTime);
            if (pushTime <= 0.1f)
            {
                rb.AddForce(Vector3.up * ShortJumpPower, ForceMode.Impulse);
                Debug.Log("小ジャンプ");
            }
            else
            {
                rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                Debug.Log("大ジャンプ");
                pushTime = 0;
            }
            JumpCount++;
            pushTime = 0;
        }

        pushAD = (Input.GetKey(KeyCode.A)) || (Input.GetKey(KeyCode.D));
        anim.SetBool("isRunning",pushAD);
        Debug.Log(pushAD);
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * 9.81f * GravityPower,ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            JumpCount = 0;
        }

        if (collision.gameObject.tag == "Platform1")
        {
            JumpCount = 0;
        }

        if (collision.gameObject.tag == "Platform2")
        {
            JumpCount = 0;
        }

        if (collision.gameObject.tag == "Platform3")
        {
            JumpCount = 0;
        }
    }

    private void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.tag == "Platform1" && downInput == true)
        {
            Debug.Log("S");
            Receiver1.SendMessage("IsTrigger");
            downInput = false;
        }

        if (collision.gameObject.tag == "Platform2" && downInput == true)
        {
            Receiver2.SendMessage("IsTrigger");
            downInput = false;
        }

        if (collision.gameObject.tag == "Platform3" && downInput == true)
        {
            Receiver3.SendMessage("IsTrigger");
            downInput = false;
        }
    }

    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            JumpCount = 1;
        }

        if (collision.gameObject.tag == "Platform1")
        {
            JumpCount = 1;
        }

        if (collision.gameObject.tag == "Platform2")
        {
            JumpCount = 1;
        }

        if (collision.gameObject.tag == "Platform3")
        {
            JumpCount = 1;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Platform1")
        {
            Receiver1.SendMessage("NotIsTrigger");
        }

        if (other.gameObject.tag == "Platform2")
        {
            Receiver2.SendMessage("NotIsTrigger");
        }

        if (other.gameObject.tag == "Platform3")
        {
            Receiver3.SendMessage("NotIsTrigger");
        }
    }
}
