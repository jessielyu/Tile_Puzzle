using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class successTXT : MonoBehaviour {
	Text SucceeText;
	
	// Use this for initialization
	void Start () {
		SucceeText = gameObject.GetComponent<Text> ();
		SucceeText.text = "";
	}
	
	// Update is called once per frame
	void Update () {
		GameObject playerctl = GameObject.Find("player");
		if (playerController.player.isSuccess) {
			SucceeText.text = "CONGRATULATIONS! YOU WIN!";
		}
	}
}
