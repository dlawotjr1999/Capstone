using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Rendering;

public class PlayManager : MonoBehaviour
{
    public static PlayManager Inst;

    // ���� ���� ���� �Լ�
    private void StartPlay()
    {
        GameManager.SetControlMode(EControlMode.FIRST_PERSON);
    }

    // �÷��̾�
    private static PlayerController Player { get; set; }
    public static float MaxHP { get { return Player.MaxHP; } }                                                                              // �÷��̾� �ִ� ü��
    public static float PlayerAttack { get { return Player.Attack; } }                                                                      // �÷��̾� ���ݷ�
    public static void SetCurPlayer(PlayerController _player) { Player = _player; }                                                         // �÷��̾� ���
    public static bool CheckIsPlayer(ObjectScript _object) { return _object == Player; }                                                    // �÷��̾����� Ȯ��
    public static Vector3 PlayerPos { get { if (IsPlayerSet) return Player.transform.position; return ValueDefinition.NULL_VECTOR; } }      // �÷��̾� ��ġ
    public static Vector2 PlayerPos2 { get { if (IsPlayerSet) return Player.Position2; return ValueDefinition.NULL_VECTOR; } }              // �÷��̾� ��� ��ġ
    public static bool IsPlayerSet { get { return Player != null; } }                                                                       // �÷��̾� ��� ����
    public static float GetDistToPlayer(Vector2 _pos) { if (!IsPlayerSet) return -1; return (PlayerPos2 - _pos).magnitude; }                // �÷��̾���� �Ÿ�
    public static void StopPlayerInteract() { Player.StopInteract(); }                                                                      // ��ȣ�ۿ� ����
    public static void StopPlayerInteract(InteractScript _interact) { Player.StopInteract(_interact); }

    // ���� ����
    
    public static bool IsDrain { get { return Player.IsDrain; } }
    public static void Drain(float _hp) { Player.Drain(_hp); }

    // ���� ����
    public static MonsterSpawnPoint[] spawnPoints;      // ���� ���� ����Ʈ
    public static int TotalMonsterNum { get { return GameManager.ObjectNum; } }             // ����ؾ� �ϴ� ���� ��
    public static int CurMonsterNum;                    // ���� ��ȯ�� ���� ��
    public static int MonsterNum;                       // ��ɴ��� ���� ��

    // UI
    private UIManager playerUI;
    private static UIManager UIManager { get { return Inst.playerUI; } }
    public static bool IsDialogueOpened { get { return UIManager.IsDialogueOpened; } }   // ��ȭâ ���ȴ���
    public static void OpenDialogue(NPCScript _npc) { UIManager.OpenDialogue(_npc); }    // ��ȭâ ����
    public static void CloseDialogue() { UIManager.CloseDialogue(); }                    // ��ȭâ �ݱ�
    public static void SetMonsterNum() { UIManager.SetMonsterNum(); }                    // ���� �� ���� �� ���� 

    public static void ToggleSupporterUI(bool _toggle) { UIManager.ToggleSupporterUI(_toggle); }
    public static void SetPlayerMaxHP(float _hp) { UIManager.SetMaxHP(_hp); }    // ü�¹� �ִ� ü��
    public static void SetPlayerCurHP(float _hp) { UIManager.SetCurHP(_hp); }    // ü�¹� ���� ü��


    private void SetSubManagers()
    {
        playerUI = GetComponent<UIManager>();
        playerUI.SetManager();
    }

    private void Awake()
    {
        if (Inst != null) { Destroy(Inst.gameObject); }
        Inst = this;
        SetSubManagers();
    }

    private void Start()
    {
        StartPlay();
    }
}
