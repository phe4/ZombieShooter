using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class FPCharacterControllerMovement : MonoBehaviour
{
    private CharacterController characterController;

    private Transform characterTransform;
    private Vector3 movementDirection;
    private bool isCrouched;
       
    private float originHeight;
    public float RunningSpeed;
    public float WalkSpeed;
    public float CrouchSpeed;
    public int stamina = 400;
    public Text StaminaText;
    public Slider StaminaSlider;
    public float JumpHeight;
    public float Gravity = 9.8f;
    public float CrouchHeight;

    private PlayerInputHandler _inputHandler;

    // Start is called before the first frame update
    private void Start()
    {
        Screen.SetResolution(1280, 720, true);
        characterController = GetComponent<CharacterController>();
        characterTransform = transform;
        originHeight = characterController.height;
        isCrouched = false;
    }

    // Update is called once per frame
    private void Update()
    {
        UpdateStaminaInfo(stamina);
        float currentSpeed = (Input.GetKey(KeyCode.LeftShift) && (!characterController.isGrounded) && stamina >= 30) ? RunningSpeed : WalkSpeed;
        if (characterController.isGrounded)
        {
            //实现移动
            var Horizontal = Input.GetAxis("Horizontal");
            var Vertical = Input.GetAxis("Vertical");

            //移动方向
            movementDirection = characterTransform.TransformDirection(new Vector3(Horizontal, 0, Vertical));

            //实现跳跃
            //实现左shift加速
            if (Input.GetKey(KeyCode.LeftShift) && stamina >= 2)
            {
                if (!isCrouched && stamina >= 30)
                {
                    currentSpeed = RunningSpeed;
                }
                else
                {
                    currentSpeed = CrouchSpeed;
                }
                stamina -= 2;
            }
            else
            {
                if (isCrouched)
                {
                    currentSpeed = CrouchSpeed;
                }
                else
                {
                    currentSpeed = WalkSpeed;
                    if(stamina <= 399)
                    {
                       stamina += 1;
                    }
                }
            }

            if (Input.GetButtonDown("Jump"))
            {
                movementDirection.y = JumpHeight;
            }

            //实现ctrl下蹲
            if (Input.GetKeyDown(KeyCode.C))
            {
                //实现从站立循序渐进到蹲下
                var currentHeight = isCrouched ? originHeight : CrouchHeight;
                StartCoroutine(DoCrouch(currentHeight));
                isCrouched = !isCrouched;
            }
        }
        else
        {
            if(!Input.GetKey(KeyCode.LeftShift) && stamina <= 399)
            {
                stamina += 1;
            }
            if(Input.GetKey(KeyCode.LeftShift) && stamina >= 2)
            {
                stamina -= 2;
            }
        }
        //实现重力
        movementDirection.y -= Gravity * Time.deltaTime;
        if(characterController.enabled){
            characterController.Move(currentSpeed * Time.deltaTime * movementDirection);
        }
    }

    private IEnumerator DoCrouch(float _target)
    {
        float currentHeight = 0;
        while (Mathf.Abs(characterController.height - _target) > 0.1f)
        {
            yield return null;
            characterController.height = Mathf.SmoothDamp(characterController.height, _target, ref currentHeight, Time.deltaTime * 5);
        }
    }

    private void UpdateStaminaInfo(float _stamina)
    {
        StaminaText.text = "Stamina" ;
        StaminaSlider.value = _stamina;
    }
}
