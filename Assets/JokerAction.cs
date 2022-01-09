using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class JokerAction : MonoBehaviour
{
    public float startPos;
    private float pushTime;
    private Rigidbody rb;
    [SerializeField]
    float downPower;
    [SerializeField]
    float upPower;
    [SerializeField]
    int JumpCount = 0;
    [SerializeField]
    private float MoveSpeed = 0;
    [SerializeField]
    private float JumpPower = 0;
    [SerializeField]
    private float ShortJumpPower = 0;
    [SerializeField]
    private float AirJumpPower = 0;
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
        Debug.Log("1");
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        transform.position = new Vector3(-41.4355f, 26.119f, 0);
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
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.A))
        {
            anim.SetBool("isRunning", false);
        }

        if (Input.GetKey(KeyCode.D))
        {
            transform.rotation = Quaternion.Euler(0, 90, 0);
            transform.position += new Vector3(10 * MoveSpeed * Time.deltaTime, 0, 0);
            anim.SetBool("isRunning", true);
        }
        else if (Input.GetKeyUp(KeyCode.D))
        {
            anim.SetBool("isRunning", false);
        }



        if (Input.GetKey(KeyCode.W))
        {
            pushTime+=Time.deltaTime;
        }
        else if (JumpCount < 1 && Input.GetKeyUp(KeyCode.W))
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

        if (JumpCount == 1 && Input.GetKeyDown(KeyCode.W))
        {
            rb.AddForce(Vector3.up * AirJumpPower, ForceMode.Impulse);
            Debug.Log("空中ジャンプ");
            JumpCount++;
        }

        if(transform.position == new Vector3(-80, 3, 0) )//&& transform.rotation.x == 180)
        {
            transform.rotation = Quaternion.Euler(0, 180, 0);
            anim.SetBool("Teeter", true);
        }
        else
        {
            anim.SetBool("Teeter", false);
        }
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
            //transform.position -= transform.up*downPower;
            Receiver1.SendMessage("IsTrigger");
            downInput = false;
        }

        if (collision.gameObject.tag == "Platform2" && downInput == true)
        {
            //transform.position -= transform.up * downPower;
            Receiver2.SendMessage("IsTrigger");
            downInput = false;
        }

        if (collision.gameObject.tag == "Platform3" && downInput == true)
        {
            //transform.position -= transform.up * downPower;
            Receiver3.SendMessage("IsTrigger");
            downInput = false;
        }
    }

    public void UpPower()
    {
        transform.position += transform.up*upPower;
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
