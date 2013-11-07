using UnityEngine;
using System.Collections;

public class GameController : MonoBehaviour
{
	public GameObject aCube;
	public GameObject spotlight;
	private GameObject[,] allCubes;
	public int gridWidth;
	public int gridHeight;
	public Airplane airplane;


	// Use this for initialization
	void Start ()
	{
		spotlight = (GameObject) GameObject.Find("Spotlight");
		airplane = new Airplane();
		airplane.cargo = 0;
		airplane.cargoCapacity = 90;
		gridWidth = 16;
		gridHeight = 9;
		allCubes = new GameObject[gridWidth, gridHeight];
		
		for (int x = 0; x < gridWidth; x++) {
			for (int y = 0; y < gridHeight; y++) {
				allCubes [x, y] = (GameObject)Instantiate (aCube, new Vector3 (x * 2 - 14, y * 2 - 8, 10), Quaternion.identity);
				allCubes[x,y].GetComponent<CubeBehavior>().x = x;
				allCubes[x,y].GetComponent<CubeBehavior>().y = y;
			}
		}
		
		airplane.x = 0;
		airplane.y = 8;
		allCubes[0,8].renderer.material.color = Color.red;

	}
	
	// Update is called once per frame
	void Update ()
	{
		ProcessKeyboardInput();
	
	}
	
	public void ProcessKeyboardInput() {
		if (Input.GetKeyDown(KeyCode.UpArrow)) {
			airplane.cargo = Mathf.Min(airplane.cargo + 10, airplane.cargoCapacity);
			print("+10 = "+airplane.cargo);
		}
		
		if (Input.GetKeyDown(KeyCode.DownArrow)) {
			airplane.cargo = Mathf.Max(airplane.cargo - 10, 0);
			print("-10 = "+airplane.cargo);
		}
		
	}

	public void HighlightCube (GameObject cube) {
		spotlight.SetActive(true);
		spotlight.transform.position = cube.transform.position; 
		spotlight.transform.Translate(0, 0, -1.5f);		
	}
	
	public void ProcessClickedCube (GameObject clickedCube, int x, int y)
	{

		// If the player clicks an inactive airplane, it should highlight
		if (x == airplane.x && y == airplane.y && airplane.active == false) {
  			airplane.active = true;
  			clickedCube.renderer.material.color = Color.yellow;
			HighlightCube(clickedCube);
		}  
		// If the player clicks an active airplane, it should unhighlight
		else if (x == airplane.x && y == airplane.y && airplane.active) {
  			airplane.active = false;
  			clickedCube.renderer.material.color = Color.red;
			spotlight.SetActive(false);
		}  
		// If the player clicks the sky and there isn’t an active airplane, nothing happens.
		
		// If the player clicks the sky and there is an active airplane, the airplane teleports to that location 
		else if (airplane.active && (x != airplane.x || y != airplane.y) ) {
			// Set the old cube to white
			allCubes[airplane.x, airplane.y].renderer.material.color = Color.white;

			// Set the new cube to yellow (since the airplane is still active)
			allCubes[x, y].renderer.material.color = Color.yellow;

			// Update the airplane to be in the new location
			airplane.x = x;
			airplane.y = y;
			
			HighlightCube(clickedCube);

		}


	}
}
