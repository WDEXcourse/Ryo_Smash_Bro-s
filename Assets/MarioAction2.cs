using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class MarioAction2 : MonoBehaviour
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
    public float PlayerHP;
    private Collider HitCollider;
    private bool isHit;
    public GameObject GamesetText;
    public float KBG;
    private bool hitStun;
    public float hitStunValue;
    private Vector3 KnockBack;
    float Weight;
    float EnemyWeight;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody>();
        anim = GetComponent<Animator>();
        transform.position = startPos;
        HitJudgement.SetActive(false);
        GamesetText.SetActive(false);
        KBG = ((0.1f + 10 * 0.05f) * PlayerHP / EnemyWeight * 1.4f + 18) * 0.01f;
        EnemyWeight = (100 + 98 / 200);
    }

    // Update is called once per frame
    void Update()
    {
        hitStunValue = KBG * 0.4f - 1;
        HP.text = PlayerHP.ToString();
        ShadowHP.text = PlayerHP.ToString();
        if (hitStun == false)
        {
            if (Input.GetKeyDown(KeyCode.K))
            {
                downInput = true;
            }

            if (Input.GetKey(KeyCode.J))
            {
                transform.rotation = Quaternion.Euler(0, -90, 0);
                transform.position += new Vector3(-10 * MoveSpeed * Time.deltaTime, 0, 0);
                anim.SetBool("MarioDash", true);
            }
            else if (Input.GetKeyUp(KeyCode.J))
            {
                anim.SetBool("MarioDash", false);
            }

            if (Input.GetKey(KeyCode.L))
            {
                transform.rotation = Quaternion.Euler(0, 90, 0);
                transform.position += new Vector3(10 * MoveSpeed * Time.deltaTime, 0, 0);
                anim.SetBool("MarioDash", true);
            }
            else if (Input.GetKeyUp(KeyCode.L))
            {
                anim.SetBool("MarioDash", false);
            }

            if (Input.GetKeyDown(KeyCode.M))
            {
                anim.SetBool("jub1", true);
                if (Input.GetKeyDown(KeyCode.M) && stateInfo.length < 10)
                {
                    anim.SetBool("jub2", true);
                }
                else
                {
                    anim.SetBool("jub2", false);
                }
            }
            else
            {
                anim.SetBool("jub1", false);
            }

            if (Input.GetKey(KeyCode.I))
            {
                anim.SetBool("isJumping", true);
            }
            else
            {
                anim.SetBool("isJumping", false);
            }

            if (Input.GetKey(KeyCode.I))
            {
                pushTime += Time.deltaTime;
            }

            if (JumpCount == 1 && Input.GetKeyDown(KeyCode.I))
            {
                rb.velocity = Vector3.zero;
                rb.AddForce(Vector3.up * AirJumpPower, ForceMode.Impulse);
                Debug.Log("空中ジャンプ");
                JumpCount++;
            }

            if (JumpCount < 1 && Input.GetKey(KeyCode.I) && pushTime <= 0.1f)
            {
                Debug.Log(pushTime);
                rb.AddForce(Vector3.up * ShortJumpPower, ForceMode.Impulse);
                Debug.Log("小ジャンプ");
                JumpCount++;
                pushTime = 0;
            }

            if (JumpCount < 1 && Input.GetKey(KeyCode.I) && pushTime > 0.1f)
            {
                rb.AddForce(Vector3.up * JumpPower, ForceMode.Impulse);
                Debug.Log("大ジャンプ");
                JumpCount++;
                pushTime = 0;
            }

            stateInfo = anim.GetCurrentAnimatorStateInfo(0);
            if (stateInfo.IsName("KnifeAction1"))
            {
                HitJudgement.SetActive(true);
            }
            else
            {
                HitJudgement.SetActive(false);
            }

            if (Input.GetKeyDown(KeyCode.LeftShift))
            {
                anim.SetBool("MarioUpAir", true);
            }
            else
            {
                anim.SetBool("MarioUpAir", false);
            }
        }

        if (hitStun == true)
        {
            if (Input.GetKey(KeyCode.J))
            {
                //ベク変
            }
        }

        if (transform.position.x <= -240 || transform.position.x >= 240 || transform.position.y <= -140 || transform.position.y >= 192)
        {
            Destroy(gameObject);
            GamesetText.SetActive(true);
            Time.timeScale = 0.3f;
        }
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
        if (other.gameObject.tag == "MarioJub1Hitbox0" || other.gameObject.tag == "MarioJub1Hitbox1" || other.gameObject.tag == "MarioJub1Hitbox2" || other.gameObject.tag == "MarioJub1Hitbox3")
        {
            Debug.Log("弱1ヒット");
            PlayerHP += 10;

            KnockBack = new Vector3(KBG * -50000 * Mathf.Cos(Mathf.PI / 6), KBG * -50000 * Mathf.Sin(Mathf.PI / 6) * -1, 0);

            Debug.Log("KB:" + KnockBack);
            isHit = true;
            StartCoroutine("stunTime");
        }
    }

    private void FixedUpdate()
    {
        rb.AddForce(Vector3.down * 9.81f * GravityPower, ForceMode.Acceleration);
        if (isHit == true)
        {
            Debug.Log("isHit");
            rb.AddForce(KnockBack, ForceMode.Impulse);
            isHit = false;
        }
    }

    IEnumerator stunTime()
    {
        //if(hitStunValue = )
        yield return new WaitForSeconds(0.5f);            //1F = 1/60秒
        hitStun = false;
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
        if (other.gameObject.tag == "LeftEdge" && transform.rotation == Quaternion.Euler(0, -90, 0))
        {
            Debug.Log("左端");
            anim.SetBool("Teeter", true);
        }
        else if (other.gameObject.tag == "RightEdge" && transform.rotation == Quaternion.Euler(0, 90, 0))
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