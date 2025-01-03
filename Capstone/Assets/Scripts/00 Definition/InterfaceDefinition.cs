using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHittable          // �������� ���� �� �ִ� ������Ʈ���� ���������� ������ �ϴ� �������̽�
{
    public bool IsPlayer { get; }
    public bool IsMonster { get; }
    public void GetHit(float _damage);   // ������ �ޱ�
}

public interface IInteractable         // ��ȣ�ۿ��� ������ ������Ʈ�� �ʼ� ����
{
    public InteractScript InteractManager { get; }
    public void SetInteractScript(InteractScript _interact);
    public bool CanInteract { get; }
    public string InfoTxt { get; }          // ��ȣ�ۿ� ���� �ؽ�Ʈ
    public void StartInteract();            // ��ȣ�ۿ� ����
    public void StopInteract();             // ��ȣ�ۿ� �ߴ�
}
