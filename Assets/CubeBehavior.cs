using UnityEngine;
using System.Collections;

public class CubeBehavior : MonoBehaviour
{
	GameController aGameController;
	public int x, y;

	// Use this for initialization
	void Start ()
	{
		aGameController = GameObject.Find("GameControllerObject").GetComponent<GameController>();
	}
	
	// Update is called once per frame
	void Update ()
	{
	
	}
	
	void OnMouseDown ()
	{
		aGameController.ProcessClickedCube(this.gameObject, x, y);
	}

}
