using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class MonsterScript
{
    [SerializeField]
    private float duration = 3f; // cc�� ���ӽð�
    [SerializeField]
    private float dottedDamage = 5f;

    public bool IsDebuffed { get; private set; }
    public bool IsDotted { get; private set; }  // ��Ʈ ������ ����

    public override void GetHit(float _damage)
    {
        curHP -= _damage;
        HPbar.gameObject.SetActive(true);
        HPbar.SetHPValue(curHP);

        if (curHP <= 0)
        {
            state = EMonsterState.DIE;

            // ���� ���� ��� ó��
            MonsterAction();

            PlayManager.MonsterNum++;   // ��ġ�� ���� �� ����
            PlayManager.SetMonsterNum();
        }
    }

    IEnumerator TempAttack()
    {
        this.transform.LookAt(PlayManager.PlayerPos);

        IsAttack = true;

        GameObject bullet = Instantiate(monsterBullet, attackPoint.position, attackPoint.rotation);

        MonsterAttack monsterAttack = bullet.GetComponent<MonsterAttack>();
        if (monsterAttack != null) monsterAttack.attack = this.Attack;

        Rigidbody rb = bullet.GetComponent<Rigidbody>();
        if (rb != null) rb.velocity = attackPoint.forward * 20;

        yield return new WaitForSeconds(1.0f);

        IsAttack = false;
    }

    // ���ӽð� ���� �����̻� ����
    IEnumerator ApplyCCType(EStatusEffect _ccType)
    {
        IsDebuffed = true; 

        if(_ccType == EStatusEffect.SLOW) // ��ȭ
        {
            monsterNav.speed *= 0.5f;
        }
        else if (_ccType == EStatusEffect.DOT_DAMAGE) // ��Ʈ��
        {
            IsDotted = true;
            StartCoroutine(DottedDamage());
        }
        else if (_ccType == EStatusEffect.STUN) // ����
        {
            this.monsterNav.isStopped = true;
            StopCoroutine(TempAttack());
            IsAttack = false;
        }
        else if(_ccType == EStatusEffect.NERF_STAT) // ���ݷ� & ���� ����
        {
            this.Attack *= 0.7f;
            this.Defense *= 0.7f;
        }

        float elapsedTime = 0f;

        while (elapsedTime < duration)
        {
            elapsedTime += Time.deltaTime;
            yield return null;
        }

        IsDebuffed = false;
        RemoveCC(_ccType);
    }

    // ���� ���� ����
    private void RemoveCC(EStatusEffect _ccType)
    {
        switch(_ccType)
        {
            case EStatusEffect.SLOW:
                monsterNav.speed = Speed;
                break;
            case EStatusEffect.DOT_DAMAGE:
                IsDotted = false;
                break;
            case EStatusEffect.STUN:
                monsterNav.isStopped = false;
                StartCoroutine(TempAttack());
                IsAttack = true;
                break;
            case EStatusEffect.NERF_STAT:
                this.Attack /= 0.7f;
                this.Defense /= 0.7f;
                break;
            default:
                return;
        }
    }

    IEnumerator DottedDamage()
    {
        while(IsDotted)
        {
            curHP -= dottedDamage;
            HPbar.SetHPValue(curHP);
            yield return new WaitForSeconds(1.0f);
        }
    }
}
