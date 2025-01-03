using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;


public partial class PlayerController : ObjectScript
{
    private PlayerCamera playerCam;     // �÷��̾� ī�޶�
    private Rigidbody playerRB;         // �÷��̾� ������ٵ�      
    public Rigidbody PlayerRB { get { return playerRB; } }

    public static event Action OnPlayerDeath; // �÷��̾��� ������ �˸��� �̺�Ʈ

    private bool isDead;

    // �Է� ����
    private PlayerInput.PlayerActions PlayerInput { get { return GameManager.PlayerInputs; } }  // Input System Player �Է�
    public Vector3 MoveInput { get { return PlayerInput.Movement.ReadValue<Vector2>(); } }      // wasd �Է�
    public Vector2 MouseDelta { get { return PlayerInput.Look.ReadValue<Vector2>(); } }         // ���콺 ��ǥ
    public bool AttackTrigger { get { return PlayerInput.Attack.triggered; } }                  // ����
    public bool JumpTrigger { get { return PlayerInput.Jump.triggered; } }                      // ����                                                                                
    public bool InteractTrigger { get { return PlayerInput.Interact.triggered; } }              // ��ȣ�ۿ�
    public bool SupportUITrigger { get { return PlayerInput.SupportUI.triggered; } }            // AI ��ȭâ


    public void PlayerDie()
    {
        if (isDead) return; // �ߺ� ó�� ����
        isDead = true;

        // ��� �̺�Ʈ ȣ��
        OnPlayerDeath?.Invoke();

        // ��� �� ��Ȱ��ȭ �Ǵ� ������Ʈ��
        // PlayerInput.Disable();
        playerRB.isKinematic = true;
        GetComponent<CapsuleCollider>().enabled = false;
        this.enabled = false;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }
    private void UpdateInputs()
    {
        PlayerInteract();       // ��ȣ�ۿ� ��ü Ž��
        PlayerAttack();
        PlayerJump();
    }

    private void Update()
    {
        Debug.Log(playerRB);
        Debug.Log(playerCam);
        playerCam.Rotate();     // ī�޶� ȸ��
        PlayerMove();           // �÷��̾� �̵�
        UpdateInputs();         // ��Ÿ ����
    }

    public void Start()
    {
        playerRB = GetComponent<Rigidbody>();
        playerCam = GetComponentInChildren<PlayerCamera>();
    }

    public override void OnEnable()
    {
        base.OnEnable();

        PlayManager.SetCurPlayer(this);
        PlayManager.SetPlayerMaxHP(MaxHP);
    }
}
