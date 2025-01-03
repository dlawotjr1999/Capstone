using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public enum EControlMode    // ���� ���
{
    FIRST_PERSON,           // 1��Ī
    UI_CONTROL              // UI ����
}

public class InputManager : MonoBehaviour
{
    private PlayerInput inputSystem;                                                                  // ��ü Input System
    public PlayerInput.PlayerActions PlayerInputs { get { return inputSystem.Player; } }              // �÷��̾� ���� Input System
    public PlayerInput.UIControlActions UIControlInputs { get { return inputSystem.UIControl; } }     // UI ���� Input System

    public EControlMode CurControlMode { get; private set; } = EControlMode.UI_CONTROL;     // ���� ���� ���

    private static float m_mouseSensitive = 1;                                              // ���콺 �ΰ���
    public static float MouseSensitive { get { return m_mouseSensitive; } }


    public void SetMouseSensitive(float _sensitive)                                         // ���콺 �ΰ��� ����
    {
        m_mouseSensitive = _sensitive;
        // PlayManager.SetCameraSensitive(_sensitive);
    }


    public void SetManager()
    {
        inputSystem = new();   
    }

    public void SetControlMode(EControlMode _mode)                                          // ���� ��� ����
    {
        CurControlMode = _mode;
        switch (_mode)
        {
            case EControlMode.FIRST_PERSON:
                Cursor.visible = false;
                Cursor.lockState = CursorLockMode.Locked;
                PlayerInputs.Enable();
                UIControlInputs.Disable();
                break;
            case EControlMode.UI_CONTROL:
                Cursor.visible = true;
                Cursor.lockState = CursorLockMode.None;
                UIControlInputs.Enable();
                PlayerInputs.Disable();
                break;
        }
    }

    private void Update()
    {
        if(CurControlMode == EControlMode.FIRST_PERSON)
        {
            if(PlayerInputs.SupportUI.triggered)
            {
                PlayManager.ToggleSupporterUI(true);
                SetControlMode(EControlMode.UI_CONTROL);
            }
        }
        else if(CurControlMode == EControlMode.UI_CONTROL)
        {
            if (UIControlInputs.SupportUI.triggered)
            {
                PlayManager.ToggleSupporterUI(false);
                SetControlMode(EControlMode.FIRST_PERSON);
            }
            else if(UIControlInputs.Interact.triggered)
            {
                PlayManager.StopPlayerInteract();
                SetControlMode(EControlMode.FIRST_PERSON);
            }
        }
    }
}
