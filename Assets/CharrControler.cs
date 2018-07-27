using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharrControler : MonoBehaviour {

    public float speed = 6f;
    public float gravity = 20f;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float SpeedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;

    private Vector3 move = Vector3.zero;

    CharacterController controler;
    Animator animator;

	void Start () {
        controler = GetComponent<CharacterController>();
        animator = GetComponent<Animator>();
	}
	
	void Update () {
        this.Moving();
        this.UpdateAnimator();
	}

    void Moving()
    {
        if (controler.isGrounded)
        {
            Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal")*speed, Input.GetAxisRaw("Vertical") * turnSmoothTime);
            Vector2 inputDir = input.normalized;

            /*if (inputDir != Vector2.zero)
            {
                float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg;
                transform.Rotate(Vector3.up * Mathf.SmoothDampAngle(transform.rotation.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime));
            }

            float targetSpeed = speed * inputDir.magnitude;
            currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, SpeedSmoothTime);

            move = transform.forward * currentSpeed;*/
            move = transform.forward * inputDir;

        }

        move.y -= gravity * Time.deltaTime;
        controler.Move(move * Time.deltaTime);
    }

    void UpdateAnimator()
    {
        animator.SetFloat("speedPercent", currentSpeed, SpeedSmoothTime, Time.deltaTime);
    }
}
