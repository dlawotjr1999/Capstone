using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class ValueDefinition
{
    // �±�
    public const string MONSTER_TAG = "Monster";
    public const string MONSTER_ATTACK_TAG = "MonsterAttack";

    public const string PLAYER_TAG = "Player";
    public const string PLAYER_ATTACK_TAG = "PlayerAttack";

    public const string GROUND_TAG = "Ground";

    // ���̾�
    // public const int GROUND_LAYER = 1 << 6;   // �� ���̾�
    public const int INTERACT_LAYER = 1 << 7; // ��ȣ�ۿ� ���̾�

    public readonly static Vector3 NULL_VECTOR = Vector3.up * 100;      // �ƹ��͵� �ƴ� ����

    // �ֹ�
    public const string SPELL1 = "Expecto Patronum";
    public const string SPELL2 = "Avada Kedavra";
    public const string SPELL3 = "Wingardium Leviosa";

    // ���� �ֹ�(�׽�Ʈ��)
}
