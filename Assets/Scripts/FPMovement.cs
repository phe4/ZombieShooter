using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//用Rigidbody使角色移动
public class FPMovement : MonoBehaviour
{
    private Transform characterTransform;
    private Rigidbody characterRigidbody;
    private bool isGround;
    public float Speed;
    public float Gravity;
    public float JumpHeight;

    // Start is called before the first frame update
    void Start()
    {
        characterTransform = transform;
        characterRigidbody = GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        if (isGround)
        {
            var Horizontal = Input.GetAxis("Horizontal");
            var Vertical = Input.GetAxis("Vertical");

            var currentDirection = new Vector3(x: Horizontal, y: 0, z: Vertical);

            currentDirection = characterTransform.TransformDirection(currentDirection);
            currentDirection *= Speed;

            var currentVelocity = characterRigidbody.velocity;
            var VelocityChange = currentDirection - currentVelocity;
            VelocityChange.y = 0;

            characterRigidbody.AddForce(force: VelocityChange, ForceMode.VelocityChange);

            if (Input.GetButtonDown("Jump"))
            {
                characterRigidbody.velocity = new Vector3(currentVelocity.x, y: CalculateJumpHeightSpeend(), currentVelocity.z);
            }
        }
        characterRigidbody.AddForce(new Vector3(x: 0, y: -Gravity * Time.deltaTime, z: 0));
    }
    private float CalculateJumpHeightSpeend()
    {
        return Mathf.Sqrt(2 * Gravity * JumpHeight);
    }
    private void OnCollisionStay(Collision other)
    {
        isGround = true;
    }

    private void OnCollisionExit(Collision other)
    {
        isGround = false;
    }
}
