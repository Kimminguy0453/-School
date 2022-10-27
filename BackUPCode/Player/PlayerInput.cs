using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class PlayerInput : MonoBehaviour
{
    [SerializeField] private string HorizontalMoveAxisName = "Horizontal";
    [SerializeField] private string VerticalMoveAxisName = "Vertical";
    [SerializeField] private string RunAxisName = "Run";
    [SerializeField] private string RotateAxisName = "Mouse_X";//�¿�
    [SerializeField] private string RotateYAxisName = "Mouse_Y";

    public float Move_Ver { get; private set; }
    public float Move_Hor { get; private set; }
    public bool run { get; private set; }
    public float rotate { get; private set; }
    public float roteteY { get; private set; }

    public event Action Object_Event;//public event ��������Ʈ�� �̺�Ʈ��

    void Update()
    {
        Move_Ver = Input.GetAxis(VerticalMoveAxisName);
        Move_Hor = Input.GetAxis(HorizontalMoveAxisName);
        run = Input.GetButton(RunAxisName);
        rotate = Input.GetAxis(RotateAxisName);
        roteteY = Input.GetAxis(RotateYAxisName);
        if (Input.GetMouseButtonDown(0))
        {
            Object_Event();
        }
    }
}
