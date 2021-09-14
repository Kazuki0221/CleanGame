using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UnityCanTest : MonoBehaviour
{
    //CharacterController characterController;
    Animator animator;

    /*Vector3 velocity;
    [SerializeField] float speed = 0.01f;
    [SerializeField] float jumpPower = 6f;
    float jumpValue;
    [SerializeField] bool isFirstJump;
    */
    // Start is called before the first frame update
    void Start()
    {
        //characterController = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        /*var input = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));

        if (characterController.isGrounded)
        {
            velocity = Vector3.down;
            animator.SetBool("is grounded", true);
            if (Input.GetButtonDown("Jump"))
            {
                isFirstJump = true;
                jumpValue = jumpPower;
                velocity.y = jumpPower;
                animator.SetTrigger("Jumping");
                animator.SetBool("is grounded", false);
            }
        }

        if (Input.GetButtonDown("Horizontal") || Input.GetButtonDown("Vertical"))
        {
            if (input.magnitude > 0)
            {
                transform.LookAt(transform.position + input.normalized);
                velocity = transform.forward * speed + new Vector3(0, velocity.y, 0);
                animator.SetFloat("Speed", input.magnitude);
            }
            else
            {
                animator.SetFloat("Speed", 0);
            }
        }

        if(jumpValue >= -jumpPower)
        {
            jumpValue += Physics.gravity.y * Time.deltaTime;
            animator.SetFloat("JumpPower", jumpValue);
        }
        velocity.y += Physics.gravity.y * Time.deltaTime;
        characterController.Move(velocity * Time.deltaTime);
        */

        var h = Vector3.right * Input.GetAxisRaw("Horizontal");
        var v = Vector3.forward * Input.GetAxisRaw("Vertical");

        if(Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            transform.rotation = Quaternion.LookRotation( h + v);
        }
        if (Input.GetButton("Horizontal") || Input.GetButton("Vertical"))
        {
            transform.position += transform.forward * 2f * Time.deltaTime;
            animator.SetBool("is running", true);
        }
        
        else
        {
            animator.SetBool("is running", false);
        }

        

        if (Input.GetKey(KeyCode.Space))
        {
            animator.SetBool("is jumping", true);
        }
        else
        {
            animator.SetBool("is jumping", false);
        }
        
    }
}
