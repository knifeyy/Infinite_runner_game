using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlockManager : MonoBehaviour {

	[SerializeField]
	private int maxBlocks = 10;

	[SerializeField]
	private GameObject[] blocks;

	[SerializeField]
	private GameObject lastBlock;

	[HideInInspector]
	public GameObject catLandedBlock;

	void Start () {
		for (int i = 1; i < maxBlocks; i++) {
			Invoke ("CreateNewBlock", i * 0.05f);
		}
	}

	void CreateNewBlock () {
		Vector3 pos = Vector3.zero;

		while (true) {
			int rand = Random.Range (0, 100);

			if (rand < 50) {
				// 50% chance to spawn ahead of the last block

				pos = new Vector3 (lastBlock.transform.localPosition.x, 1f,
					lastBlock.transform.localPosition.z + 1f);

				if (Physics.Raycast (pos, Vector3.down, 1.5f) &&
				    Physics.Raycast (new Vector3 (pos.x, pos.y, pos.z + 1f), Vector3.down, 1.5f))
					break;

			} else if (rand < 70) {
				// 20% chance to create a block to the right

				pos = new Vector3 (lastBlock.transform.localPosition.x + 1f, 1f,
					lastBlock.transform.localPosition.z);

				if (Physics.Raycast (pos, Vector3.down, 1.5f) &&
				    Physics.Raycast (new Vector3 (pos.x + 1f, pos.y, pos.z), Vector3.down, 1.5f))
					break;

			} else if (rand < 90) {
				// 10% chance for the block to be created on the left

				pos = new Vector3 (lastBlock.transform.localPosition.x - 1f, 1f,
					lastBlock.transform.localPosition.z);

				if (Physics.Raycast (pos, Vector3.down, 1.5f) &&
				    Physics.Raycast (new Vector3 (pos.x - 1f, pos.y, pos.z), Vector3.down, 1.5f))
					break;
			} 
		}

		int num = Random.Range (0, 100) > 0 ? 0 : 1;

		GameObject temp = Instantiate (blocks [num]) as GameObject;
		temp.transform.localPosition = new Vector3 (pos.x, 0f, pos.z);
		temp.transform.parent = transform;
		temp.name = "Block";
		lastBlock = temp;

		temp.GetComponent<BlockScript> ().moveLikeYoyo = true;

	}

	public void LeaveLandedBlock() {

		for (int i = 0; i < 2; i++) {
			CreateNewBlock ();
		}

//		CreateNewBlock ();

		if (catLandedBlock != null) {
			catLandedBlock.SendMessage ("FallBlock");
		}
	}


} // class







































