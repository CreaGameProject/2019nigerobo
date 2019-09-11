using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMove : MonoBehaviour
{
    [SerializeField]private float movespeed = 3;
    float moveX, moveZ;

    CharacterController controller;

    private Animator animator;


    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
        animator.SetBool("moving",true);
    }

    // Update is called once per frame
    void Update()
    {
        moveX = Input.GetAxis("Horizontal");
        moveZ = Input.GetAxis("Vertical");
    }
    void FixedUpdate()
    {
        Vector3 cameradirection = Vector3.Scale(Camera.main.transform.forward, new Vector3(1, 0, 1)).normalized;
        Vector3 direction = cameradirection * moveZ + Camera.main.transform.right * moveX;

        controller.SimpleMove(direction*movespeed);

        if (cameradirection != Vector3.zero && (moveX!=0||moveZ!=0))
        {
            transform.rotation = Quaternion.LookRotation(cameradirection);
        }
    }
}
