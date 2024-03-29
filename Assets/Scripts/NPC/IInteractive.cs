using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public enum InteractType
{
    Potal,
    NPC,
    SuperPotal
}
public interface IInteractive  
{

    void OpenUI();
    void CloseUI();
    void TryTalk();

    InteractType GetType();
    void Closer();

    void Interact();
    Vector3 GetPosition();

    //������ ��´�
}


