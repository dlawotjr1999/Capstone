using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerCamera : MonoBehaviour
{
    [SerializeField]
    private float mouseSensitivity = 10f;       // ���콺 ����
    [SerializeField]
    private PlayerController player;            // �÷��̾�    
    
    private float verticalRotation = 0f;
    private float horizontalRotation = 0f;

    public void Rotate()
    {
        // ���Ŀ� ���� �߻� �� Time.deltaTime ���
        // float mouseX = player.MouseDelta.x * mouseSensitivity * Time.deltaTime;
        // float mouseY = player.MouseDelta.y * mouseSensitivity * Time.deltaTime;

        float mouseX = Input.GetAxis("Mouse X") * mouseSensitivity;
        float mouseY = Input.GetAxis("Mouse Y") * mouseSensitivity;

        verticalRotation -= mouseY;
        horizontalRotation += mouseX;

        verticalRotation = Mathf.Clamp(verticalRotation, -90f, 90f);                 // ���� ȸ���� -90������ 90���� ����

        transform.localRotation = Quaternion.Euler(verticalRotation, 0f, 0f);        // ī�޶� ���� ȸ�� ���� (X�� ȸ��)
        player.transform.rotation = Quaternion.Euler(0f, horizontalRotation, 0f);    // �÷��̾� �¿� ȸ��
    }
}
