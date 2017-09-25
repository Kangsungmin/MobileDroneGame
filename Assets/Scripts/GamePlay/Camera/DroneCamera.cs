﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DroneCamera : MonoBehaviour
{
    private const float Y_ANGLE_MIN = -70.0f;
    private const float Y_ANGLE_MAX = 70.0f;

    Transform lookAt;
    Transform camTransform;

    private Camera cam;
    private float distance = 4.5f;
    private float currentX = 0.0f;
    private float currentY = 15.0f;
    public VirtualJS_Left moveJoystickLeft;//조이스틱 객체
    // Use this for initialization
    void Start()
    {
        lookAt = GameObject.FindGameObjectWithTag("Player").transform;
        camTransform = transform;
        cam = Camera.main;
    }
    private void Update()
    {
        currentY = lookAt.eulerAngles.x + 15.0f;
        //currentY = Mathf.Clamp(currentY, Y_ANGLE_MIN, Y_ANGLE_MAX);
        currentX = lookAt.eulerAngles.y;
    }
    // Update is called once per frame
    void LateUpdate()
    {
        Vector3 dir = new Vector3(0, 0.0f, -(distance + 1.0f));//z축으로 간겨을 둔 것
        //currentX += moveJoystickLeft.Horizontal();//조이스틱으로 카메라 회전

        Quaternion rotation = Quaternion.Euler(currentY, currentX, 0);

        camTransform.position = lookAt.position + rotation * dir;
        Vector3 CamlookPos = lookAt.position + rotation * new Vector3(0, 2.0f, distance);//드론의 살짝 뒤를 본다

        camTransform.LookAt(CamlookPos);//보는 방향
    }
}
