using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;

public class CatMovement : MonoBehaviour {

	[SerializeField]
	private float speed = 0.3f, height = 2f;

	private BlockManager blockManager;

	[SerializeField]
	private AudioClip catDie;
	private AudioSource audioManager;

	private Animator anim;

	ArrayList keyArray = new ArrayList();

	private bool isDead;

	private GameObject waterFX;

	void Awake () {
		blockManager = GameObject.Find ("Block Manager").GetComponent<BlockManager> ();
		anim = GetComponentInChildren<Animator> ();
		audioManager = GetComponent<AudioSource> ();

		waterFX = GameObject.Find ("Water Fountain");
		waterFX.SetActive (false);
	}

	void Start() {
		DOTween.Init (false, true, LogBehaviour.ErrorsOnly);
	}

	void Update () {

		if (!isDead && blockManager.catLandedBlock != null) {
			CheckInput ();
		}

		if (transform.position.y < 0) {
			if (!waterFX.activeInHierarchy) {
				audioManager.clip = catDie;
				audioManager.Play ();

				LeanTween.rotateAroundLocal (gameObject, Vector3.left, 90f, 0.5f);
				GetComponent<BoxCollider> ().isTrigger = true;
				isDead = true;

				waterFX.SetActive (true);
				waterFX.transform.position = new Vector3 (transform.position.x, -0.5f,
					transform.position.z);
			}
		}

	}

	void CheckInput() {
		if (Input.GetKeyDown (KeyCode.UpArrow))
			keyArray.Add (KeyCode.UpArrow);

		if (Input.GetKeyDown (KeyCode.DownArrow))
			keyArray.Add (KeyCode.DownArrow);

		if (Input.GetKeyDown (KeyCode.RightArrow))
			keyArray.Add (KeyCode.RightArrow);

		if (Input.GetKeyDown (KeyCode.LeftArrow))
			keyArray.Add (KeyCode.LeftArrow);

		if (keyArray.Count > 0) {

			MoveTheCat ();

			keyArray.RemoveAt (0);
		}

	}

	void MoveTheCat () {
		KeyCode key = (KeyCode)keyArray [0];

		if (key == KeyCode.UpArrow) {
//			audioManager.Play ();
			anim.Play ("ready");

			transform.rotation = Quaternion.Euler (0f, 180f, 0f);
			Vector3 pos = new Vector3 (transform.position.x, transform.position.y,
				transform.position.z + 1f);

			transform.DOJump (pos, height, 1, speed);

			blockManager.LeaveLandedBlock ();
			anim.SetTrigger("jump");

		}

		if (key == KeyCode.DownArrow) {
//			audioManager.Play ();
			anim.Play ("ready");

			transform.rotation = Quaternion.Euler (0f, 0f, 0f);
			Vector3 pos = new Vector3 (transform.position.x, transform.position.y,
				transform.position.z - 1f);

			transform.DOJump (pos, height, 1, speed);

			blockManager.LeaveLandedBlock ();
			anim.SetTrigger("jump");

		}

		if (key == KeyCode.RightArrow) {
//			audioManager.Play ();
			anim.Play ("ready");

			transform.rotation = Quaternion.Euler (0f, -90f, 0f);
			Vector3 pos = new Vector3 (transform.position.x + 1f, transform.position.y,
				transform.position.z);

			transform.DOJump (pos, height, 1, speed);

			blockManager.LeaveLandedBlock ();
			anim.SetTrigger("jump");

		}

		if (key == KeyCode.LeftArrow) {
//			audioManager.Play ();
			anim.Play ("ready");

			transform.rotation = Quaternion.Euler (0f, 90f, 0f);
			Vector3 pos = new Vector3 (transform.position.x - 1f, transform.position.y,
				transform.position.z);

			transform.DOJump (pos, height, 1, speed);

			blockManager.LeaveLandedBlock ();
			anim.SetTrigger("jump");

		}

	}

	void OnCollisionEnter(Collision target) {
		if (target.gameObject.name == "Block") {
			blockManager.catLandedBlock = target.gameObject;
		}
	}

} // class




































