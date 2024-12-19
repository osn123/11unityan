using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unitychan_Controller : MonoBehaviour
{
    Rigidbody rigid;
    Animator animator;

    public float rotateForce = 5;
    public float runForce = 10;
    public float maxRunSpeed = 2;
    public float jumpforce = 250;

    void Start()
    {
        Application.targetFrameRate = 60;
        // 必要なコンポーネントを自動取得
        rigid = GetComponent<Rigidbody>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (rigid == null) return;  //物理運動が入っていない場合は終了

        transform.Rotate(0, Input.GetAxis("Horizontal") * rotateForce, 0);

        if (Input.GetButtonDown("Jump")){
            rigid.AddForce(transform.up * jumpforce);
            animator.SetTrigger("Jump");
        }

        bool isRun = Input.GetAxis("Vertical") > 0.01;
        Vector3 vel_xz = new Vector3(rigid.velocity.x, 0, rigid.velocity.z);
        if (isRun && vel_xz.magnitude < maxRunSpeed)
        {
            rigid.AddForce(transform.forward * Input.GetAxis("Vertical") * runForce);
        }
        animator.SetBool("Run", isRun);
    }
}