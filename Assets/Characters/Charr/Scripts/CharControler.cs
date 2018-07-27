using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class CharControler : MonoBehaviour
{
    public GameObject armature;

    public float walkSpeed = 1.5f;
    public float runSpeed = 4;

    public float turnSmoothTime = 0.2f;
    float turnSmoothVelocity;

    public float SpeedSmoothTime = 0.1f;
    float speedSmoothVelocity;
    float currentSpeed;
    bool drawnd = false;

    Animator animator;

    // Use this for initialization
    void Start()
    {
        animator = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if (EventSystem.current.IsPointerOverGameObject())
        {
            return;
        }

        Moving();
    }

    void Moving()
    {
        /*
        animator.SetBool("isWalking", false);
        animator.SetBool("isRunning", false);

        float targetSpeed = 0;
        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            animator.SetBool("isWalking", true);
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + this.camera.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        targetSpeed = walkSpeed * inputDir.magnitude;

        if (Input.GetKey(KeyCode.LeftShift))
        {
            animator.SetBool("isRunning", true);
            targetSpeed = runSpeed * inputDir.magnitude;
        }

        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, SpeedSmoothTime);
        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, SpeedSmoothTime, Time.deltaTime);*/

        Vector2 input = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));
        Vector2 inputDir = input.normalized;

        if (inputDir != Vector2.zero)
        {
            float targetRotation = Mathf.Atan2(inputDir.x, inputDir.y) * Mathf.Rad2Deg + Camera.main.transform.eulerAngles.y;
            transform.eulerAngles = Vector3.up * Mathf.SmoothDampAngle(transform.eulerAngles.y, targetRotation, ref turnSmoothVelocity, turnSmoothTime);
        }

        bool running = Input.GetKey(KeyCode.LeftShift);
        float targetSpeed = ((running) ? runSpeed : walkSpeed) * inputDir.magnitude;
        currentSpeed = Mathf.SmoothDamp(currentSpeed, targetSpeed, ref speedSmoothVelocity, SpeedSmoothTime);

        transform.Translate(transform.forward * currentSpeed * Time.deltaTime, Space.World);

        float animationSpeedPercent = ((running) ? 1 : .5f) * inputDir.magnitude;
        animator.SetFloat("speedPercent", animationSpeedPercent, SpeedSmoothTime, Time.deltaTime);

        if (Input.GetKeyDown(KeyCode.T))
        {
            if (!this.drawnd)
            {
                this.drawnd = true;
                animator.SetBool("isDrawing", this.drawnd);
            }
        } else if (Input.GetKeyDown(KeyCode.Tab))
        {
            if (this.drawnd)
            {
                this.drawnd = false;
                animator.SetBool("isDrawing", this.drawnd);
            }
        }
    }

    void DrawingWeapon()
    {
        if (this.drawnd)
        {
            GameObject[] weaponSlots = GameObject.FindGameObjectsWithTag("Stuff_Slots");
            weaponSlots[0].transform.GetChild(0).transform.parent = weaponSlots[4].transform;
            weaponSlots[4].transform.GetChild(0).transform.localPosition = Vector3.zero;
            weaponSlots[4].transform.GetChild(0).transform.localEulerAngles = Vector3.zero;
        }else if (!this.drawnd)
        {
            GameObject[] weaponSlots = GameObject.FindGameObjectsWithTag("Stuff_Slots");
            weaponSlots[4].transform.GetChild(0).transform.parent = weaponSlots[0].transform;
            weaponSlots[0].transform.GetChild(0).transform.localPosition = Vector3.zero;
            weaponSlots[0].transform.GetChild(0).transform.localEulerAngles = Vector3.zero;
        }
    }

}