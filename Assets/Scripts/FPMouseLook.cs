using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//鼠标移动
public class FPMouseLook : MonoBehaviour
{

    private Transform CameraTransform;
    [SerializeField] private Transform characterTransform;//引入 使角色随着相机旋转
    private Vector3 cameraRotation;//保存每一帧X轴值
    float xRotation = 0f;
    public float MouseSensitivity;
    public Vector2 MaxminAngle;

    // Start is called before the first frame update
    void Start()
    {
        CameraTransform = transform;
    }

    private void Update()
    {
        if (Input.GetKeyDown("="))
        {
            MouseSensitivity += 0.5f;
        }
        if (Input.GetKeyDown("-") && MouseSensitivity >= 1.5f)
        {
            MouseSensitivity -= 0.5f;
        }

        float mouseX = Input.GetAxis("Mouse X") * MouseSensitivity *Time.deltaTime;
        float mouseY = Input.GetAxis("Mouse Y") * MouseSensitivity *Time.deltaTime;

        xRotation -= mouseY;
        xRotation = Mathf.Clamp(xRotation, MaxminAngle.x, MaxminAngle.y);
        transform.localRotation = Quaternion.Euler(xRotation, 0f, 0f);
        characterTransform.Rotate(Vector3.up * mouseX);


    }

    // Update is called once per frame
    // private void FixedUpdate()
    // {
    //     var tmp_MouseX = Input.GetAxis("Mouse X");
    //     var tmp_MouseY = Input.GetAxis("Mouse Y");

    //     cameraRotation.x -= tmp_MouseY * MouseSensitivity;
    //     cameraRotation.y += tmp_MouseX * MouseSensitivity;

    //     cameraRotation.x = Mathf.Clamp(value: cameraRotation.x, min: MaxminAngle.x, max: MaxminAngle.y);
    //     CameraTransform.rotation = Quaternion.Euler(x: cameraRotation.x, y: cameraRotation.y, z: 0);
    //     characterTransform.rotation = Quaternion.Euler(x: 0, y: cameraRotation.y, z: 0);
    // }
}
