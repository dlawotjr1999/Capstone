using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EventManager : MonoBehaviour
{
    [SerializeField]
    private Image DieFrame;
    [SerializeField]
    private PlayerController Player;

    private List<MonsterScript> monsters;

    public void HandlePlayerDeath()    // �÷��̾ �׾��� �� �ý��ۿ��� ó���� ���
    {
        DieFrame.gameObject.SetActive(true);
        NotifyMonsters();
    }
    public void PlayerRespawn()
    {
        Player.IsDead = false;
        DieFrame.gameObject.SetActive(false);

        Player.PlayerRB.isKinematic = false;
        Player.GetComponent<CapsuleCollider>().enabled = true;
        Player.enabled = true;
        GameManager.SetControlMode(EControlMode.FIRST_PERSON);
        Player.transform.position = new Vector3(0f, 0f, 0f);       // ������ ��ġ
    }
    public void RegisterMonster(MonsterScript monster)
    {
        if (!monsters.Contains(monster))
        {
            monsters.Add(monster);
        }
    }
    public void UnregisterMonster(MonsterScript monster)
    {
        if (monsters.Contains(monster))
        {
            monsters.Remove(monster);
        }
    }
    public void NotifyMonsters()    // �÷��̾ �׾��� �� ���� �ܿ��� ó���� ���
    {
        foreach (var monster in monsters)
        {
            monster.ReactToPlayerDeath();
        }
    }

    public void SetManager()
    {
        monsters = new List<MonsterScript>();
    }

    private void OnEnable()
    {
        PlayerController.OnPlayerDeath += HandlePlayerDeath;
    }

    private void OnDisable()
    {
        PlayerController.OnPlayerDeath -= HandlePlayerDeath;
    }
}
