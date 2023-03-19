using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MarioHitbox : MonoBehaviour
{
    private Animator anim;
    private AnimationClip AnimationClip;
    public GameObject Jub1Hitbox0;
    public GameObject Jub1Hitbox1;
    public GameObject Jub1Hitbox2;
    public GameObject Jub1Hitbox3;
    public Collider sphereCollider0;
    public Collider sphereCollider1;
    public Collider sphereCollider2;
    public Collider sphereCollider3;
    public MeshRenderer mr0;
    public MeshRenderer mr1;
    public MeshRenderer mr2;
    public MeshRenderer mr3;

    private void Awake()
    {
        Jub1Hitbox0 = transform.Find("Armature/Root/HandIK.L/Jub1Hitbox0").gameObject;
        Jub1Hitbox1 = transform.Find("Armature/Root/HandIK.L/Jub1Hitbox1").gameObject;
        Jub1Hitbox2 = transform.Find("Armature/Root/HandIK.L/Jub1Hitbox2").gameObject;
        Jub1Hitbox3 = transform.Find("Armature/Root/HandIK.L/Jub1Hitbox3").gameObject;
        sphereCollider0 = Jub1Hitbox0.GetComponent<SphereCollider>();
        sphereCollider1 = Jub1Hitbox1.GetComponent<SphereCollider>();
        sphereCollider2 = Jub1Hitbox2.GetComponent<SphereCollider>();
        sphereCollider3 = Jub1Hitbox3.GetComponent<SphereCollider>();
        mr0 = Jub1Hitbox0.GetComponent<MeshRenderer>();
        mr1 = Jub1Hitbox1.GetComponent<MeshRenderer>();
        mr2 = Jub1Hitbox2.GetComponent<MeshRenderer>();
        mr3 = Jub1Hitbox3.GetComponent<MeshRenderer>();
    }

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Debug.Log(Jub1Hitbox0.name);
        sphereCollider0.enabled = false;
        sphereCollider1.enabled = false;
        sphereCollider2.enabled = false;
        sphereCollider3.enabled = false;
        mr0.enabled = false;
        mr1.enabled = false;
        mr2.enabled = false;
        mr3.enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        //AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        //string clipName = clipInfo[0].clip.name;
        //Debug.Log(clipName);
        //Debug.Log(clipInfo[0].clip.length);

        //if (clipInfo[0].clip.length == 1)
        //{
        //    sphereCollider0.enabled = true;
        //    sphereCollider1.enabled = true;
        //    sphereCollider2.enabled = true;
        //    sphereCollider3.enabled = true;
        //    mr0.enabled = true;
        //    mr1.enabled = true;
        //    mr2.enabled = true;
        //    mr3.enabled = true;
        //    Debug.Log("on");
        //}

        //if (clipInfo[0].clip.length == 3)
        //{
        //    sphereCollider0.enabled = false;
        //    sphereCollider1.enabled = false;
        //    sphereCollider2.enabled = false;
        //    sphereCollider3.enabled = false;
        //    mr0.enabled = false;
        //    mr1.enabled = false;
        //    mr2.enabled = false;
        //    mr3.enabled = false;

        //}
    }

    void OnMarioJub1Hitbox()
    {
        sphereCollider0.enabled = true;
        sphereCollider1.enabled = true;
        sphereCollider2.enabled = true;
        sphereCollider3.enabled = true;
        mr0.enabled = true;
        mr1.enabled = true;
        mr2.enabled = true;
        mr3.enabled = true;
        Debug.Log("on");
    }

    void OffMarioJub1Hitbox()
    {
        sphereCollider0.enabled = false;
        sphereCollider1.enabled = false;
        sphereCollider2.enabled = false;
        sphereCollider3.enabled = false;
        mr0.enabled = false;
        mr1.enabled = false;
        mr2.enabled = false;
        mr3.enabled = false;
        Debug.Log("off");
    }
}
