using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trap : MonoBehaviour 
{
    [SerializeField] private int damage = 10;
    private void OnCollisionEnter2D(Collision2D col)
    {
        col.collider.transform.GetComponent<CombatCharacter>().TakeDamage(damage);
    }
}
