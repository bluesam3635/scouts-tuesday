using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour {
    public void GivePoints(int n)
    {
        points += n;
        Debug.Log("Points now at: " + n);
    }
    public int points;
	// Use this for initialization
	void Start () {
		points = 0;
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
