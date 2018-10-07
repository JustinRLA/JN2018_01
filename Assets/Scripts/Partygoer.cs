using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class Partygoer : MonoBehaviour {

	public UnityEngine.AI.NavMeshAgent AI;
	public Animator Rig;
	public MeshRenderer triggerMesh;
	public bool randomMood;
	public float anger; // 0-1 scale of how aggressive the AI is towards the player. 0 is dismissve, 1 is a bumrush towards the player.
	float minSpeed;
	float maxSpeed;
	GameObject player;
	public EmoteBubble emotion;
	public AudioSource Larynx;
	public AudioClip[] happySounds;
	public AudioClip[] unhappySounds;
	public Material darkMaterial;
	public SkinnedMeshRenderer skinRenderer;

	public void Speak (bool happy) {
		Larynx.pitch = Random.Range (0.8f, 1.2f);
		if (happy == true) {
			Larynx.PlayOneShot (happySounds[Random.Range (0, happySounds.Length)], Random.Range (0.8f, 1f));
		} else {
			Larynx.PlayOneShot (unhappySounds[Random.Range (0, unhappySounds.Length)], Random.Range (0.8f, 1f));
		}
	}

	void Awake () {
		triggerMesh.enabled = false;
	}

	public void EmoteAnim (float delay, string animName) {
		AI.ResetPath ();
		CancelInvoke ("Move");
		Rig.SetTrigger (animName);
		Invoke ("RestartAI", delay);
	}

	public void RestartAI () {
		InvokeRepeating ("Move", 0, (4f - anger) + Random.Range (0.1f, 4f));
	}

	void Start () {
		if (randomMood) {
			anger = Random.Range (0.1f, 1f);
		}

		AI.speed = 1.5f - anger;
		maxSpeed = AI.speed;
		minSpeed = AI.speed * 0.75f;

		InvokeRepeating ("Move", 0, (4f - anger) + Random.Range (0.1f, 4f));
	}

	void Update () {
		anger += 0.003f * Time.deltaTime;
	}

	public void Move () {

		AI.speed = maxSpeed;
		AI.destination = RandomNavmeshLocation (5f - (anger * 4f));

	}

	public void DepressPlayer (float damage) {
		//Player.Depress(how much to depress);
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//TRIGGER FUNCTIONS
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	private void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "partyGoer") { // if the partygoer runs into another partygoer

			Speak (true);
			if (Random.Range (0f, 1f) <= anger - 0.2f) {
				AI.destination = other.gameObject.transform.position; // "Angry" partygoers will act selfishly and flock to other players
			}
			if (Random.Range (0f, 1f) <= anger - 0.2f) { // maybe say hi

				if (Random.Range (0, 2) == 0) {
					EmoteAnim (1f, "Hi");
				} else {
					EmoteAnim (1f, "Wave");
				}
			}
		}
		if (other.gameObject.tag == "outerPlayer") { // if the partygoer runs into another player
			emotion.EmoteFaceOnly ("No");
			if (Random.Range (0f, 1f) <= anger - 0.5f) {
				AI.speed = minSpeed;
				AI.destination = other.gameObject.transform.position; // "Angry" partygoers will walk towards the player
			}
			if (Random.Range (0f, 1f) <= anger - 0.2f) { // maybe say what

				EmoteAnim (1f, "Neutral");

			}
		}
		if (other.gameObject.tag == "intimatePlayer") { // if the partygoer gets too close to the player
			if (Random.Range (0f, 1f) <= anger - 0.5f) {

				DepressPlayer (anger);
			}
			if (Random.Range (0f, 1f) <= anger - 0.2f) { // maybe say fuck off

				EmoteAnim (1f, "Oust");
			}
		}
	}

	public Vector3 RandomNavmeshLocation (float radius) {
		Vector3 randomDirection = Random.insideUnitSphere * radius;
		randomDirection += transform.position;
		UnityEngine.AI.NavMeshHit hit;
		Vector3 finalPosition = Vector3.zero;
		if (UnityEngine.AI.NavMesh.SamplePosition (randomDirection, out hit, radius, 1)) {
			finalPosition = hit.position;
		}
		return finalPosition;
	}

	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	//GAMEPLAY FUNCTIONS
	//////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////////
	public void SetMood (float modifier, GameObject player) {
		//Modify partygoer's mood
		anger = anger + modifier;
        this.transform.LookAt(player.transform);

		//Partygoer reactions
		if (modifier > 0) {
			Speak (false);
			if (Random.Range (0, 2) == 0) {
				emotion.Emote ("No");
			} else {
				emotion.Emote ("Irate");
			}
			EmoteAnim (4f, "Failure");
			DarkenCharacter ();

		} else if (modifier < 0) {
			Speak (true);
			emotion.Emote ("Yes");
			EmoteAnim (4f, "Success");
		} else {
			emotion.Emote ("Maybe");
			EmoteAnim (2f, "Neutral");
		}
	}

	public void DarkenCharacter () {
		skinRenderer.material = darkMaterial;

		/*	Material[] skinMaterials = skinRenderer.materials;

			for (int i = 0; i < skinMaterials.Length; i++) {
				skinMaterials[i] = darkMaterial;
			}

			skinRenderer.materials = skinMaterials;*/
	}

}