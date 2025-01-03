using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(IInteractable))]
public class InteractScript : MonoBehaviour                 // ��ȣ�ۿ��� ������ ������Ʈ�� �ִ� ��ũ��Ʈ
{
    private Vector2 Position2 { get { return new(transform.position.x, transform.position.z); } }

    [SerializeField]
    private float canInteractDist = 2.5f;                                             // ��ȣ�ۿ� ���� �Ÿ�
    private float canInteractAngle = 60f;

    private IInteractable m_interactable;                                             // ������Ʈ �� IInteractable�� ����� ������Ʈ

    private float InteractAngle { get { return canInteractAngle; } }
    public bool CanInteract { get { return DistToPlayer <= canInteractDist && CheckInteractable; } }  // ��ȣ�ۿ� ��������


    public float DistToPlayer { get { return PlayManager.GetDistToPlayer(Position2); } }           // �÷��̾���� �Ÿ�
    public Transform InteractTransform { get { return transform; } }                               // ��ȣ�ۿ� ����� ��ġ

    public bool CheckInteractable { get { return m_interactable.CanInteract; } }

    public void AbleInteract()                 // ���� ���
    {
        if (!CanInteract) { return; }
        ShowToggleUI();
    }
    public void DisableInteract()              // ���� �����
    {
        HideToggleUI();
    }
    private void ShowToggleUI()                 // ���� UI ����
    {
       // PlayManager.ShowInteractInfo(m_interactable.InfoTxt);
    }
    private void HideToggleUI()                 // ���� UI �����
    {
       // PlayManager.HideInteractInfo();
    }

    public void StartInteract()                // ��ȣ�ۿ� ����
    {
        m_interactable.StartInteract();
        HideToggleUI();
    }
    public void StopInteract()                  // ��ȣ�ۿ� �ߴ�
    {
        m_interactable.StopInteract();
        ShowToggleUI();
    }

    private void Awake()
    {
        m_interactable = GetComponent<IInteractable>();
        m_interactable.SetInteractScript(this);
    }
}
