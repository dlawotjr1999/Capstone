using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCScript : MonoBehaviour, IInteractable
{
    public InteractScript InteractManager { get; private set; }

    public bool CanInteract { get { return true; } }

    public string InfoTxt { get { return "��ȭ"; } }

    public void SetInteractScript(InteractScript _script)
    {
        InteractManager = _script;
    }

    public virtual void StartInteract()
    {
        Debug.Log(this.name + " ��ȣ�ۿ� ����");
        PlayManager.OpenDialogue(this);
    }

    public virtual void StopInteract()
    {
        Debug.Log(this.name + " ��ȣ�ۿ� ����");
        PlayManager.StopPlayerInteract();
        GameManager.SetControlMode(EControlMode.FIRST_PERSON);
    }

    private void Start()
    {
        
    }
}
