using UnityEngine;
using System.Collections;

public class playerController : MonoBehaviour {
	public static playerController player;
	public bool isSuccess;

	float speed = 1.0f;

	int numColored;

	Vector3 obstacle1 = new Vector3 (-4.75f, -0.25f, 0.0f);
	Vector3 obstacle2 = new Vector3 (-3.5f, 3.5f, 0.0f);
	Vector3 obstacle3 = new Vector3 (-3.5f, 2.25f, 0.0f);
	Vector3 obstacle4 = new Vector3 (-3.5f, -4.0f, 0.0f);
	Vector3 obstacle5 = new Vector3 (-2.25f, -1.5f, 0.0f);
	Vector3 obstacle6 = new Vector3 (2.75f, 2.25f, 0.0f);
	Vector3 obstacle7 = new Vector3 (2.75f, -2.75f, 0.0f);
	Vector3 obstacle8 = new Vector3 (4.0f, -0.25f, 0.0f);

	Vector3 block00;
	Vector3 block01;
	Vector3 block02;
	Vector3 block03;
	Vector3 block04;
	Vector3 block05;
	Vector3 block06;
	Vector3 block10;
	Vector3 block11;
	Vector3 block12;
	Vector3 block13;
	Vector3 block14;
	Vector3 block15;
	Vector3 block16;
	Vector3 block20;
	Vector3 block21;
	Vector3 block22;
	Vector3 block23;
	Vector3 block24;
	Vector3 block25;
	Vector3 block26;
	Vector3 block30;
	Vector3 block31;
	Vector3 block32;
	Vector3 block33;
	Vector3 block34;
	Vector3 block35;
	Vector3 block36;
	Vector3 block40;
	Vector3 block41;
	Vector3 block42;
	Vector3 block43;
	Vector3 block44;
	Vector3 block45;
	Vector3 block46;
	Vector3 block50;
	Vector3 block51;
	Vector3 block52;
	Vector3 block53;
	Vector3 block54;
	Vector3 block55;
	Vector3 block56;
	Vector3 block60;
	Vector3 block61;
	Vector3 block62;
	Vector3 block63;
	Vector3 block64;
	Vector3 block65;
	Vector3 block66;
	Vector3 block70;
	Vector3 block71;
	Vector3 block72;
	Vector3 block73;
	Vector3 block74;
	Vector3 block75;
	Vector3 block76;
	Vector3 block80;
	Vector3 block81;
	Vector3 block82;
	Vector3 block83;
	Vector3 block84;
	Vector3 block85;
	Vector3 block86;
	Vector3 block90;
	Vector3 block91;
	Vector3 block92;
	Vector3 block93;
	Vector3 block94;
	Vector3 block95;
	Vector3 block96;

	Vector3 win = new Vector3 (-1.0f, -0.25f, 0.0f);
	
	// Use this for initialization
	void Start () {
		isSuccess = false;
		player = this;

		numColored = 0;

		block00 = GameObject.Find("block00").transform.position;
		block01 = GameObject.Find("block01").transform.position;
		block02 = GameObject.Find("block02").transform.position;
		block03 = GameObject.Find("block03").transform.position;
		block04 = GameObject.Find("block04").transform.position;
		block05 = GameObject.Find("block05").transform.position;
		block06 = GameObject.Find("block06").transform.position;
		block10 = GameObject.Find("block10").transform.position;
		block11 = GameObject.Find("block11").transform.position;
		block12 = GameObject.Find("block12").transform.position;
		block13 = GameObject.Find("block13").transform.position;
		block14 = GameObject.Find("block14").transform.position;
		block15 = GameObject.Find("block15").transform.position;
		block16 = GameObject.Find("block16").transform.position;
		block20 = GameObject.Find("block20").transform.position;
		block21 = GameObject.Find("block21").transform.position;
		block22 = GameObject.Find("block22").transform.position;
		block23 = GameObject.Find("block23").transform.position;
		block24 = GameObject.Find("block24").transform.position;
		block25 = GameObject.Find("block25").transform.position;
		block26 = GameObject.Find("block26").transform.position;
		block30 = GameObject.Find("block30").transform.position;
		block31 = GameObject.Find("block31").transform.position;
		block32 = GameObject.Find("block32").transform.position;
		block33 = GameObject.Find("block33").transform.position;
		block34 = GameObject.Find("block34").transform.position;
		block35 = GameObject.Find("block35").transform.position;
		block36 = GameObject.Find("block36").transform.position;
		block40 = GameObject.Find("block40").transform.position;
		block41 = GameObject.Find("block41").transform.position;
		block42 = GameObject.Find("block42").transform.position;
		block43 = GameObject.Find("block43").transform.position;
		block44 = GameObject.Find("block44").transform.position;
		block45 = GameObject.Find("block45").transform.position;
		block46 = GameObject.Find("block46").transform.position;
		block50 = GameObject.Find("block50").transform.position;
		block51 = GameObject.Find("block51").transform.position;
		block52 = GameObject.Find("block52").transform.position;
		block53 = GameObject.Find("block53").transform.position;
		block54 = GameObject.Find("block54").transform.position;
		block55 = GameObject.Find("block55").transform.position;
		block56 = GameObject.Find("block56").transform.position;
		block60 = GameObject.Find("block60").transform.position;
		block61 = GameObject.Find("block61").transform.position;
		block62 = GameObject.Find("block62").transform.position;
		block63 = GameObject.Find("block63").transform.position;
		block64 = GameObject.Find("block64").transform.position;
		block65 = GameObject.Find("block65").transform.position;
		block66 = GameObject.Find("block66").transform.position;
		block70 = GameObject.Find("block70").transform.position;
		block71 = GameObject.Find("block71").transform.position;
		block72 = GameObject.Find("block72").transform.position;
		block73 = GameObject.Find("block73").transform.position;
		block74 = GameObject.Find("block74").transform.position;
		block75 = GameObject.Find("block75").transform.position;
		block76 = GameObject.Find("block76").transform.position;
		block80 = GameObject.Find("block80").transform.position;
		block81 = GameObject.Find("block81").transform.position;
		block82 = GameObject.Find("block82").transform.position;
		block83 = GameObject.Find("block83").transform.position;
		block84 = GameObject.Find("block84").transform.position;
		block85 = GameObject.Find("block85").transform.position;
		block86 = GameObject.Find("block86").transform.position;


		//Debug.Log(block00);
	}
	
	// Update is called once per frame
	void Update () {
	
		//Vector3 curPos = transform.position;
		Vector3 curPos = transform.position;
		Vector3 prevPos = transform.position;

		if (Input.GetKeyDown (KeyCode.RightArrow)) { 
			if (curPos.x < 4.0f) {
				curPos.x += 1.25f;
			}
		} else if (Input.GetKeyDown (KeyCode.LeftArrow)) {
			if (curPos.x > -6.0f) {
				curPos.x -= 1.25f;
			}

		} else if (Input.GetKeyDown (KeyCode.UpArrow)) {
			if (curPos.y < 3.5f) {
				curPos.y += 1.25f;
			}
		} else if (Input.GetKeyDown (KeyCode.DownArrow)) {
			if (curPos.y > -4.0f) {
				curPos.y -= 1.25f;
			}
		}

		if (curPos != obstacle1 && curPos != obstacle2 && curPos != obstacle3 && curPos != obstacle4 &&
		    curPos != obstacle5 && curPos != obstacle6 && curPos != obstacle7 && curPos != obstacle8 && curPos != win) {

			if (block00 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block00").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			} 
			else if (block01 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block01").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block02 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block02").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block03 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block03").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block04 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block04").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block05 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block05").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block06 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block06").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block10 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block10").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block11 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block11").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block12 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block12").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block13 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block13").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block14 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block14").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block15 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block15").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block16 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block16").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block20 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block20").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block21 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block21").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block22 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block22").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block23 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block23").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block24 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block24").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block25 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block25").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block26 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block26").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block30 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block30").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block31 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block31").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block32 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block32").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block33 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block33").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block34 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block34").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block35 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block35").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block36 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block36").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block40 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block40").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block41 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block41").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block42 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block42").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block43 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block43").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block44 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block44").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block45 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block45").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block46 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block46").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block50 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block50").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block51 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block51").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block52 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block52").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block53 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block53").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block54 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block54").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block55 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block55").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block56 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block56").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block60 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block60").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block61 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block61").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block62 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block62").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block63 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block63").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block64 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block64").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block65 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block65").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block66 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block66").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block70 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block70").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block71 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block71").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block72 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block72").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block73 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block73").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block74 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block74").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block75 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block75").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block76 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block76").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block80 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block80").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block81 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block81").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block82 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block82").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block83 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block83").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block84 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block84").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block85 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block85").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
			else if (block86 == curPos) {
				SpriteRenderer sprite = GameObject.Find("block86").GetComponent<SpriteRenderer>();
				if (sprite.color == Color.white) {
					sprite.color = new Color (1.0f, 0.2f, 0.0f, 1.0f);
					transform.position = curPos;
					numColored++;
				}
			}
		}

		if (curPos == win) {
			if (numColored >= 54) {
				isSuccess = true;
				transform.position = curPos;
			}
		}
	}
}
