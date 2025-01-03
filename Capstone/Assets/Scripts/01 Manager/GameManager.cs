using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.InputSystem;

public class GameManager : MonoBehaviour
{
    public static GameManager Inst { get; private set; }

    // �Է�
    private InputManager inputManager;
    public static InputManager InputManager { get { return Inst.inputManager; } }
    public static PlayerInput.PlayerActions PlayerInputs { get { return InputManager.PlayerInputs; } }                                      // �÷��̾� Input
    public static PlayerInput.UIControlActions UIControlInputs { get { return InputManager.UIControlInputs; } }                             // UI���� Input
    public static EControlMode ControlMode { get { return InputManager.CurControlMode; } }                                                  // ���� ���
    public static float MouseSensitive { get { return InputManager.MouseSensitive; } }                                                      // ���콺 �ΰ���
    public static void SetControlMode(EControlMode _mode) { InputManager.SetControlMode(_mode); }                                           // ���� ��� ����
    public static void SetMouseSensitive(float _sensitive) { InputManager.SetMouseSensitive(_sensitive); }                                  // ���콺 �ΰ��� ����

    // ��ų
    private SkillManager skillManager;
    public static SkillManager SkillManager { get { return Inst.skillManager; } }
    public static PlayerAttack[] Skills { get { return SkillManager.Skills; } }

    // ������Ʈ Ǯ
    private PoolManager poolManager;
    public static PoolManager PoolManager { get { return Inst.poolManager; } }
    public static List<GameObject> PoolObjects { get { return PoolManager.poolObjects; } }
    public static int ObjectNum { get { return PoolManager.ObjectNum; } }
    public static GameObject GetPooledObject() { return PoolManager.GetPooledObject(); }

    // �̺�Ʈ
    private EventManager eventManager;
    public static EventManager EventManager { get { return Inst.eventManager; } }
    public static void HandlePlayerDeath() { EventManager.HandlePlayerDeath(); }
    public static void RegisterMonster(MonsterScript _monster) { EventManager.RegisterMonster(_monster); }
    public static void UnregisterMonster(MonsterScript _monster) { EventManager.UnregisterMonster(_monster); }
    public static void NotifyMonsters() { EventManager.NotifyMonsters(); }

    private void SetSubManagers()
    {
        inputManager = GetComponent<InputManager>();
        inputManager.SetManager();
        skillManager = GetComponent<SkillManager>();
        skillManager.SetManager();
        eventManager = GetComponent<EventManager>();
        eventManager.SetManager();
        poolManager = GetComponent<PoolManager>();
        poolManager.SetManager();
    }

    private void Awake()
    {
        if (Inst != null) { Destroy(gameObject); return; }
        Inst = this;
        DontDestroyOnLoad(gameObject);
        SetSubManagers();
    }
}
