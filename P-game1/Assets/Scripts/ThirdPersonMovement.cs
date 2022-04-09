using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ThirdPersonMovement : MonoBehaviour
{

    [SerializeField] float speed = 6f;
    [SerializeField] float turnSmoothTime = 0.1f;
    [SerializeField] float jumpForce = 10f;
    [SerializeField] float gripDragRate;
    public Vector3 movingDistance;

    public Transform cam;

    public Vector3 direction;
    Vector3 moveDir;
    private float turnSmoothVelocity;
    bool isJumped;
    float distToGround;

    Rigidbody rb;
    MAnimation mAnimation;
    Collider colliderPlayer;

    private void Start()
    {
        colliderPlayer = this.GetComponent<Collider>();
        distToGround = colliderPlayer.bounds.extents.y;
        rb = this.GetComponent<Rigidbody>();
        mAnimation = GetComponent<MAnimation>();
    }

    void Update()
    {
        float horizontal = Input.GetAxisRaw("Horizontal");
        float vertical = Input.GetAxisRaw("Vertical");
        direction = new Vector3(horizontal, 0f, vertical).normalized;
        isJumped = Input.GetKeyDown(KeyCode.Space);
        if (isJumped && CanJump())
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
            movingDistance = moveDir.normalized * speed * Time.deltaTime;
            rb.MovePosition(transform.position + movingDistance);
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

    private bool CanJump()
    {
        Vector3 boxExtends = new Vector3(colliderPlayer.bounds.extents.x, colliderPlayer.bounds.extents.x, colliderPlayer.bounds.extents.x);
        return Physics.BoxCast(colliderPlayer.bounds.center, boxExtends, Vector3.down, Quaternion.identity, distToGround + 0.1f);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "Grip")
        {
            speed = speed / gripDragRate;
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.gameObject.tag == "Grip")
        {
            speed = speed * gripDragRate;
        }
    }


}
