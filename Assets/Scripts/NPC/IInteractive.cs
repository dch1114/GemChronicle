using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public interface IInteractive  
{

    void OpenUI();
    void CloseUI();
    void TryTalk();

    void Closer();

    void Interact();
    Vector3 GetPosition();

    //시점만 잡는다
}


