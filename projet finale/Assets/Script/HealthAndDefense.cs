using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HealthAndDefense : MonoBehaviour
{
    [SerializeField] private int _health = 100;

    public void RecieveDamage(int damage)
    {
        _health -= damage;
        Debug.Log("health remaining: " + _health);
    }

}
