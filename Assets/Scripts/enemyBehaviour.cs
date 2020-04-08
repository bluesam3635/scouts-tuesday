using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
public class enemyBehaviour : MonoBehaviour 
{
    PlatformerCharacter2D pc;
    // THIS IS ALL TERRIBLE TEMPORARY STUFF
//    [SerializeField] public float turnTime = 3f;
    [SerializeField] public bool faceRight = false;
    float cTime = 0f;
    void Update () {
        if (Physics2D.OverlapCircleAll(pc.m_RightCheck.position, pc.wallTouchRadius, pc.whatIsWall).Length != 0 && faceRight)
            faceRight = false;
        else if (Physics2D.OverlapCircleAll(pc.m_LeftCheck.position, pc.wallTouchRadius, pc.whatIsWall).Length != 0 && !faceRight)
            faceRight = true;
        int h = -1;
        if (faceRight)
            h = 1;
        pc.Move(h,false,false);
    }
    
	// Use this for initialization
	void Start () {
		pc = gameObject.GetComponent<PlatformerCharacter2D>(); 
	}
	
	// Update is called once per frame
}
