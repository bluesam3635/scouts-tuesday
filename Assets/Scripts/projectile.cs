using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour 
{
    [SerializeField]int baseDamage = 1;
    [SerializeField]bool damageScaling = true;
	void OnCollisionEnter2D(Collision2D c)
    {
            var cc = c.gameObject.GetComponent<CombatCharacter>();
            if (cc)
            {
                if (damageScaling)
                {
                    cc.TakeDamage ((int)Mathf.Round(baseDamage * c.relativeVelocity.magnitude));
                }
                else
                {
                    cc.TakeDamage (baseDamage);
                }
            }
            Destroy(gameObject);
    }
}
