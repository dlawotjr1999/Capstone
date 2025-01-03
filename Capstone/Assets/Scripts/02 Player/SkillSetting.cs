using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public partial class PlayerAttack
{ 
    public bool IsDrained { get; private set; }

    public void SetEmotion(EEmotion _emotion)
    {
        // �ð�ȿ�� ������ ���� �ִµ� �� �� ���� ��
        switch (_emotion)
        {
            case EEmotion.EHappy:
                
                break;
            case EEmotion.EAngry:
                
                break;
            case EEmotion.ENeutral:
                
                break;
            case EEmotion.EDisgust:
                
                break;
            case EEmotion.EFear:
                
                break;
            case EEmotion.ESad:
                
                break;
            case EEmotion.ESurprise:
                
                break;
            default: 
                return;
        }
        StatusEffect = (EStatusEffect)_emotion;
    }

    public void SetSkillType(ESkill _skill)
    {
        Skill = _skill;
    }
}

