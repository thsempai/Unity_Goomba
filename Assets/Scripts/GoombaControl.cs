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
        rb2d.AddForce(Vector2.up * jumpForce);
    }

    private void MoveX(){
        speed = xAxis.ReadValue<float>() * walkSpeed;
        animator.SetFloat("speed", speed);

        Vector3 movement = Vector3.right * speed * Time.deltaTime;
        transform.position += movement;
    }
}
