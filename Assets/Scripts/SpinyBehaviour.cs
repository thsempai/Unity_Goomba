using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Animator))]
public class SpinyBehaviour : MonoBehaviour
{

    private Animator animator;
    public float walkSpeed = 2f;

    [Header("Debug")]
    [SerializeField] private bool CheckGapDebug = false;


    private bool CheckGap(){


        // float factor = 1f;
        // if(walkSpeed < 0f) factor = -1f;

        Vector2 start = transform.position + Vector3.right * 0.5f * (walkSpeed > 0f ? 1f : -1f);

        Vector2 direction = Vector2.up * -1;

        RaycastHit2D hit = Physics2D.Raycast(start, direction, 1f);

        if(CheckGapDebug) Debug.DrawRay(start, direction * 2f, Color.cyan);

        if(hit.collider != null){
            if (hit.transform.CompareTag("ground")){
                return false;
            }
        }
        return true;
    }

    private bool CheckSide(){

        Vector2 start = transform.position + Vector3.up * 0.05f + Vector3.right * 0.5f * (walkSpeed > 0f ? 1f : -1f);

        Vector2 direction = Vector2.right * (walkSpeed > 0f ? 1f : -1f);

        RaycastHit2D hit = Physics2D.Raycast(start, direction, 0.05f);

        if(CheckGapDebug) Debug.DrawRay(start, direction * 0.05f, Color.cyan);

        if(hit.collider != null){
            if (hit.transform.CompareTag("ground") || hit.transform.CompareTag("enemy")){
                return true;
            }
        }
        return false;
    }

    void Start(){
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        //move
        Vector3 move = Vector3.right * walkSpeed * Time.deltaTime;
        transform.position += move;
        if(CheckGap() || CheckSide()){
            walkSpeed *= -1;
        }
        animator.SetFloat("speed", walkSpeed);
    }
}
