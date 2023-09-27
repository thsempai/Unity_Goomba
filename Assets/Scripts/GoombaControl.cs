using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Animator))]
[RequireComponent(typeof(Rigidbody2D))]
public class GoombaControl : MonoBehaviour
{
    private Animator animator;

    public InputActionAsset actions;
    private InputAction xAxis;

    public float walkSpeed = 4f;
    private float speed = 0f;

    private Rigidbody2D rb2d;
    public float jumpForce = 400f;

    [Header("Jump")]
    public float jumpAngleThreshold = 45f;
    
    private bool isJumping = false;

    [Header("Debug")]
    [SerializeField]private bool debugJump = false; 


    // Start is called before the first frame update
    void Awake()
    {
        xAxis = actions.FindActionMap("GoombaActionMap").FindAction("X Axis");
        animator = GetComponent<Animator>();
        rb2d = GetComponent<Rigidbody2D>();
    }

    void OnEnable(){
        actions.FindActionMap("GoombaActionMap").Enable();
        actions.FindActionMap("GoombaActionMap").FindAction("Jump").performed += OnJump;
    }

    void OnDisable(){
        actions.FindActionMap("GoombaActionMap").Disable();
        actions.FindActionMap("GoombaActionMap").FindAction("Jump").performed -= OnJump;
    }

    // Update is called once per frame
    void Update()
    {
        MoveX();
    }

    private void OnJump(InputAction.CallbackContext context){
        if(isJumping) return;

        rb2d.AddForce(Vector2.up * jumpForce);
        isJumping = true;
    }

    private void MoveX(){
        speed = xAxis.ReadValue<float>() * walkSpeed;
        animator.SetFloat("speed", speed);

        Vector3 movement = Vector3.right * speed * Time.deltaTime;
        transform.position += movement;
    }

    private void OnCollisionEnter2D(Collision2D collision){
        if(collision.transform.CompareTag("ground")){
            Vector2 contactPosition = collision.GetContact(0).point;
            Vector2 normal = collision.GetContact(0).normal;
            
            if(debugJump) Debug.DrawRay(contactPosition, normal * 2f, Color.red, 3f);

            if(Vector2.Angle(Vector2.up, normal) > jumpAngleThreshold) return;
            
            isJumping = false;
        }
    }
}
