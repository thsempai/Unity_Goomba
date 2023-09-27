using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpinyBehaviour : MonoBehaviour
{
    public float walkSpeed = 2f;

    [Header("Debug")]
    [SerializeField] private bool CheckGapDebug = false;


    private bool CheckGap(){
        Vector2 start = transform.position + Vector3.right * 0.5f;
        Vector2 direction = Vector2.up * -1;

        RaycastHit2D hit = Physics2D.Raycast(start, direction);

        if(CheckGapDebug) Debug.DrawRay(start, direction * 2f, Color.cyan, 3f);

        if(hit.collider != null){
            if (hit.transform.CompareTag("ground")){
                return false;
            }
        }
        return true;
    }

    // Update is called once per frame
    void Update()
    {
        //move
        Vector3 move = Vector3.right * walkSpeed * Time.deltaTime;
        transform.position += move;
        if(CheckGap()){
            walkSpeed *= -1;
        }
    }
}
