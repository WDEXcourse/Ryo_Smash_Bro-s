using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class Action2P : MonoBehaviour
{
    public Vector3 startPos;
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
    private bool MoveStop;
    private AnimatorStateInfo stateInfo;
    public GameObject HitJudgement;
    public Text HP;
    public Text ShadowHP;
    private int PlayerHP;
    private Collider HitCollider;


    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("1");
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        transform.position = startPos;
        HitJudgement.SetActive(false);
        transform.rotation = Quaternion.Euler(0, -90, 0);
    }

    // Update is called once per frame
    void Update()
    {
        HP.text = PlayerHP.ToString();
        ShadowHP.text = PlayerHP.ToString();
        if (MoveStop == false)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                downInput = true;
            }

            if (Input.GetKey(KeyCode.J))
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.position += new Vector3(-10 * MoveSpeed * Time.deltaTime, 0, 0);
                anim.SetBool("isRunning", true);
            }
            else if (Input.GetKeyUp(KeyCode.J))
            {
                anim.SetBool("isRunning", false);
            }

            if (Input.GetKey(KeyCode.L))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.position += new Vector3(10 * MoveSpeed * Time.deltaTime, 0, 0);
                anim.SetBool("isRunning", true);
            }
            else if (Input.GetKeyUp(KeyCode.L))
            {
                anim.SetBool("isRunning", false);
            }
        }

        if (Input.GetKey(KeyCode.E))
        {
            anim.SetBool("Attack", true);
        }
        else
        {
            anim.SetBool("Attack", false);
        }

        if (Input.GetKey(KeyCode.M))
        {
            anim.SetBool("Knife1", true);
        }
        else
        {
            anim.SetBool("Knife1", false);
        }

        if (Input.GetKey(KeyCode.I))
        {
            pushTime += Time.deltaTime;
        }
        else if (JumpCount < 1 && Input.GetKeyUp(KeyCode.I))
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

        if (JumpCount == 1 && Input.GetKeyDown(KeyCode.I))
        {
            rb.AddForce(Vector3.up * AirJumpPower, ForceMode.Impulse);
            Debug.Log("空中ジャンプ");
            JumpCount++;
        }

        stateInfo = anim.GetCurrentAnimatorStateInfo(0);
        if (stateInfo.IsName("KnifeAction1"))
        {
            MoveStop = true;
            HitJudgement.SetActive(true);
        }
        else
        {
            MoveStop = false;
            HitJudgement.SetActive(false);
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * 9.81f * GravityPower, ForceMode.Acceleration);
    }

    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.tag == "Ground")
        {
            JumpCount = 0;
        }

        if (collision.gameObject.tag == "Edge")
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

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "HitJudgement")
        {
            PlayerHP += 10;
            Vector3 KnockBackGrowth;
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

    private void OnTriggerStay(Collider other)
    {
        if (other.gameObject.tag == "LeftEdge" && transform.rotation == Quaternion.Euler(0,-90,0))
        {
            Debug.Log("左端");
            anim.SetBool("Teeter", true);
        }
        else if (other.gameObject.tag == "RightEdge" && transform.rotation == Quaternion.Euler(0,90,0))
        {
            anim.SetBool("Teeter", true);
        }
    }

    public void UpPower()
    {
        transform.position += transform.up * upPower;
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

        if (other.gameObject.tag == "LeftEdge")
        {
            anim.SetBool("Teeter", false);
        }
        else if (other.gameObject.tag == "RightEdge")
        {
            anim.SetBool("Teeter", false);
        }
    }
}