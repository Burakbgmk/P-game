using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float jumpForce = 10f;

    public Transform cam;

    Vector3 direction;
    Vector3 moveDir;
    private float turnSmoothVelocity;
    bool isJumped;

    Rigidbody rb;
    MAnimation mAnimation;

    private void Start()
    {
        rb = this.GetComponent<Rigidbody>();
        mAnimation = GetComponent<MAnimation>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        isJumped = Input.GetKeyDown(KeyCode.Space);
        if (isJumped && rb.transform.position.y < 0.03)
        {
            Jump();
        }
    }

    void FixedUpdate()
    {
        if (direction.magnitude >= 0.1f)
        {
            float targetAngle = Mathf.Atan2(direction.x, direction.z)*Mathf.Rad2Deg + cam.eulerAngles.y;
            float angle = Mathf.SmoothDampAngle(transform.eulerAngles.y, targetAngle, ref turnSmoothVelocity, turnSmoothTime);
            transform.rotation = Quaternion.Euler(0f, angle, 0f);
            moveDir = Quaternion.Euler(0f, targetAngle, 0f) * Vector3.forward;
            rb.MovePosition(transform.position + moveDir.normalized * speed * Time.deltaTime);
            mAnimation.RunAnimator();
        }
        else
        {
            mAnimation.IdleAnimator();
        }
        
    }

    private void Jump()
    {
        rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
    }

    
}
