using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ObjectScript : MonoBehaviour, IHittable
{
    [SerializeField]
    private float maxHP;
    protected float curHP;

    [SerializeField]
    private float attack;
    [SerializeField]
    private float attackSpeed;
    [SerializeField]
    private float defense;
    [SerializeField]
    private float speed;

    public Vector3 Position { get { return transform.position; } }                                  // ��ǥ
    public Vector2 Position2 { get { return new(transform.position.x, transform.position.z); } }    // ��� ��ǥ

    public float MaxHP
    {
        get { return maxHP; }                         // �ִ� ü��
        protected set { maxHP = value; }
    }
    public float Attack
    {
        get { return attack; }                        // ���ݷ�
        protected set { attack = value; }
    }
    public float AttackSpeed                          // ���� �ӵ�
    {
        get { return attackSpeed; }
        protected set { attackSpeed = value; }
    }
    public float Defense                            // ����
    {
        get { return defense; }
        protected set { defense = value; }
    }
    public virtual float Speed
    {
        get { return speed; }                      // ���� �ӵ�
        protected set { speed = value; }
    }


    public bool IsDead { get; set; }                          // ���� ����
    public virtual bool IsUnstoppable { get; } = true;                  // ��Ʈ ���� ���� ����

    public virtual bool IsPlayer { get { return false; } }
    public virtual bool IsMonster { get { return false; } }

    public virtual void GetHit(float _damage)
    {

    }

    public virtual void OnEnable()
    {
        curHP = maxHP;
    }
}
