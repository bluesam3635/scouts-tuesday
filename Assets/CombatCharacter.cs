using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityStandardAssets._2D;

public class CombatCharacter : MonoBehaviour {

	// Use this for initialization
    [SerializeField] GameObject shotObject;
	void Start () 
    {
		curHP = maxHP;
        attackTimer = attackRate;
	}
    [SerializeField] private float attackRate = 0.1f;
	[SerializeField] public int maxHP = 10;
    [SerializeField] private float shootSpeed = 1f;
    private int curHP;
    private float attackTimer;
	// Update is called once per frame
	void Update () 
    {
        attackTimer += Time.deltaTime;
	}
    void DoAttack(Vector3 dir)
    {
        attackTimer = 0;
        // physics
        var go = GameObject.Instantiate(shotObject);
        float facing = 1;
        if (dir.x > 0)
        {
            go.transform.position = transform.Find("RightCheck").position;
        }
        else 
        {
            go.transform.position = transform.Find("LeftCheck").position;
            facing = -1;
        }
        go.GetComponent<Rigidbody2D>().velocity = new Vector2(facing * shootSpeed,0) + transform.GetComponent<Rigidbody2D>().velocity; 
        // art
        // TODO
    }
    public void Attack()
    {
        if (attackTimer >= attackRate)        
            DoAttack(transform.localScale);
        // int arr = 0;
        // if (Input.GetButtonDown("Left"))
        // {
        //     arr += 1;
        // }
        // if (Input.GetButtonDown("Jump"))
        // {
        //     arr += 4;
        // }
        // if (Input.GetButtonDown("Right"))
        // {
        //     arr -= 1;
        // }
        // if (Input.GetButtonDown("Crouch"))
        // {
        //     arr -= 4;
        // }
        // if (transform.GetComponent<PlatformerCharacter2D>().m_Grounded)
        // {
        //     arr += 16;
        // }
        
        
        // switch(arr)
        // {
        //     case 1: 
        //         // attack left
                
        //     break;
        //     case 4:
        //         // attack up
        //     break;
        //     case 5:
        //         // attack left/up
        //     break;
        //     case -1:
        //         // attack right
        //     break;
        //     case 3:
        //         // attack right/up
        //     break;
        //     case -4:
        //         // attack down
        //     break;
        //     case -3:
        //         // attack left/down
        //     break;
        //     case -5:
        //         // attack right/down
        //     break;
        //     case 16:
        //         // air attack facing
        //     break;
        //     case 17:
        //         // air attack left
        //     break;
        //     case 20:
        //         // air attack up
        //     break;
        //     case 21:
        //         // air attack left/up
        //     break;
        //     case 15:
        //         // air attack right
        //     break;
        //     case 19:
        //         // air attack right/up
        //     break;
        //     case 12:
        //         // air attack down
        //     break;
        //     case 13:
        //         // air attack down/left
        //     break;
        //     case 11:
        //         // air attack down/right
        //     break;
        //     case 0:
        //     default:
        //         // attack in facing direction
        //     break;
        // }
    }
    public void Heal(int n)
    {
        curHP = Math.Min(maxHP,curHP+n);
    }
    public void Heal()
    {
        Heal(maxHP);
    }
    public void TakeDamage(int n)
    {
        curHP -= n;
        if (curHP <= 0)
        {
            Die();
        }
    }
    public void Die()
    {
        Destroy(gameObject);
    }
    
    
}
