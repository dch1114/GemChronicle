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

    Vector3 GetPosition();
}


