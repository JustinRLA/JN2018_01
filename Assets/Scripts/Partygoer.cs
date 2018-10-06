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

	void Awake () {
		triggerMesh.enabled = false;
	}

	void Start () {
		if (randomMood) {
			anger = Random.Range (0.1f, 1f);
		}

		AI.speed = 2f - anger;
		maxSpeed = AI.speed;
		minSpeed = AI.speed * 0.75f;

		InvokeRepeating ("Move", 0, (4f - anger) + Random.Range (0.1f, 2f));
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

	private void OnTriggerEnter (Collider other) {

		if (other.gameObject.tag == "partyGoer") { // if the partygoer runs into another partygoer
			if (Random.Range (0f, 1f) <= anger - 0.2f) {
				AI.destination = other.gameObject.transform.position; // "Angry" partygoers will act selfishly and flock to other players
			}
			if (Random.Range (0f, 1f) <= anger - 0.2f) { // maybe say hi
				emotion.Emote ("Yes");
			}
		}
		if (other.gameObject.tag == "outerPlayer") { // if the partygoer runs into another player

			if (Random.Range (0f, 1f) <= anger - 0.2f) {
				AI.speed = minSpeed;
				AI.destination = other.gameObject.transform.position; // "Angry" partygoers will walk towards the player
			}
			if (Random.Range (0f, 1f) <= anger - 0.2f) { // maybe say what
				emotion.Emote ("Maybe");
			}
		}
		if (other.gameObject.tag == "intimatePlayer") { // if the partygoer gets too close to the player

			if (Random.Range (0f, 1f) <= anger - 0.2f) {
				// Play freakout anim
				DepressPlayer (anger);
			}
			if (Random.Range (0f, 1f) <= anger - 0.2f) { // maybe say fuck off
				emotion.Emote ("No");
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

}