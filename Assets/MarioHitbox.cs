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
    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
        Jub1Hitbox0 = transform.Find("Armature/Root/HandIK.L/Jub1Hitbox0").gameObject;
        Jub1Hitbox1 = transform.Find("Armature/Root/HandIK.L/Jub1Hitbox1").gameObject;
        Jub1Hitbox2 = transform.Find("Armature/Root/HandIK.L/Jub1Hitbox2").gameObject;
        Jub1Hitbox3 = transform.Find("Armature/Root/HandIK.L/Jub1Hitbox3").gameObject;
        Debug.Log(Jub1Hitbox0.name);
        sphereCollider0 = Jub1Hitbox0.GetComponent<SphereCollider>();
    }

    // Update is called once per frame
    void Update()
    {
        AnimatorClipInfo[] clipInfo = anim.GetCurrentAnimatorClipInfo(0);
        string clipName = clipInfo[0].clip.name;
        Debug.Log(clipName);

        if (clipInfo[0].clip.length == 0.01)
        {
            sphereCollider0.enabled = true;
        }

        if (clipInfo[0].clip.length == 0.03)
        {
            sphereCollider0.enabled = false;
        }
    }
}
