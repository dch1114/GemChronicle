using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    public void Move(Vector3 _speed)
    {
        //need Camera Limit
        transform.Translate(_speed);
    }
}
