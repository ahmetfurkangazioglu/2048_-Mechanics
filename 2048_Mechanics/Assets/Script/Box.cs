using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Box : MonoBehaviour
{
    [SerializeField] GameManager manager;

    public void BoxOperation()
    {
        manager.BoxExplosion(transform.position);
        gameObject.SetActive(false);
    }
}
