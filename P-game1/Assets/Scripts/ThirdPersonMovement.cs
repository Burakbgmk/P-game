using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float jumpForce = 10f;

    public Transform cam;

    Vector3 moveDir;
    private float turnSmoothVelocity;
    private float animMaxSpeed = 5.029f;
    Rigidbody rb;
    Fighter fighter;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space)) Jump();
        // Double jump should be avoided and this should be in fixedupdate..
    }

    void FixedUpdate()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        Vector3 direction = new Vector3(horizontal, 0f, vertical).normalized;

        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(transform.position + moveDir.normalized * speed * Time.deltaTime);
            RunAnimator();

        }
        

        else
        {
            IdleAnimator();
            return;
        }
        
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    void RunAnimator()
    {
        GetComponent<Animator>().SetFloat("forwardSpeed",animMaxSpeed);
    }

    void IdleAnimator()
    {
        GetComponent<Animator>().SetFloat("forwardSpeed", 0);
    }
}
