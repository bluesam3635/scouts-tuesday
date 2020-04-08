using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum triggerType
{
    levelEnd,
    conversation,
    tileEvent,
    pickupItem,
    givePoints
}

public class levelEnd : MonoBehaviour 
{
    [SerializeField]public triggerType type;
    private MapLoader ml;
    private GameObject c;
    public GameObject targetTile;
    public GameObject pickup;
 //   private pickableItem item;
    private GameController gc;
    public int numPoints;
    
    void OnTriggerEnter2D(Collider2D c)
    {
        if (c.name == "Character(Clone)")
        {
            switch (type)
            {
                case triggerType.levelEnd:
                    ml.EndLevel();
                break;
                case triggerType.conversation:
                    // TODO
                break;
                case triggerType.tileEvent:
     //               targetTile.GetComponent<triggerableTile>().Trigger();
                break;
                case triggerType.pickupItem:
    //                c.GetComponent<CharacterController>().PickupItem(item);
    //                pickup.GetComponent<worldItem>().PickUp();
                break;
                case triggerType.givePoints:
                    gc.GivePoints(numPoints);
                    Destroy(gameObject);
                break;
            }
        }
    }
	// Use this for initialization
	void Start () {
		ml = GameObject.Find("WorldController").GetComponent<MapLoader>();
        c = GameObject.Find("Character");
 //       item = pickup.GetComponent<item>();
        gc = GameObject.Find("WorldController").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
