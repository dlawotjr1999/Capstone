using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;

public partial class PlayerController
{
    [SerializeField]
    private InteractScript interactableObject;  // ��ȣ�ۿ� ������ ��ȣ�ۿ� �� ���
    private bool Interacting { get; set; }

    private void DetectObject()
    {
        if (Interacting) { return; }

        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        RaycastHit[] hits = Physics.RaycastAll(ray, 10, ValueDefinition.INTERACT_LAYER);

        InteractScript interact = null;         // ��ȣ�ۿ� ��� Ž��
        for (int i = 0; i < hits.Length; i++)   // ��ó ��� Ȯ��
        {
            GameObject obj = hits[i].collider.gameObject;
            InteractScript script = obj.GetComponentInParent<InteractScript>()
                ?? obj.GetComponentInChildren<InteractScript>();
            if (script == null || !script.CanInteract) { continue; }               // ��ũ��Ʈ�� ���ų� ��ȣ�ۿ� �Ұ����� ���
            interact = script;
        }

        if (interact != null && interact != interactableObject)   // ����� �ٲ� ���
        {
            if (interactableObject != null)
                interactableObject.DisableInteract();

            interactableObject = interact;
            interactableObject.AbleInteract();
        }
        if (interactableObject != null && !interactableObject.CanInteract)  // ����� ����� ���
        {
            interactableObject.DisableInteract();
            interactableObject = null;
        }
    }

    public void StartInteract()                                                     // ��ȣ�ۿ� ����
    {
        Interacting = true;
    }
    public void StopInteract()                                                      // ��ȣ�ۿ� �ߴ�
    {
        interactableObject = null;
        Interacting = false;
    }
    public void StopInteract(InteractScript _interact)
    {
        if (interactableObject != _interact) { return; }
    }

    public void PlayerInteract()
    {
        DetectObject();
        if (interactableObject != null && PlayerInput.Interact.triggered)
        {
            StartInteract();
            interactableObject.StartInteract();
        }
    }
}
