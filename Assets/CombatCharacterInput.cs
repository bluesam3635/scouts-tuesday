using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatCharacterInput : MonoBehaviour {

    CombatCharacter cc;
	// Use this for initialization
	void Start () {
		cc = gameObject.GetComponent<CombatCharacter>();
	}
	
    
    
    
	// Update is called once per frame
	void Update () 
	{	
        if (Input.GetButtonDown("Attack"))    
        {
            cc.Attack();
        }
	}
}
