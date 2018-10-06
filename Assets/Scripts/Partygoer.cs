﻿using System.Collections;
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
			if (Random.Range (0f, 1f) <= anger - 0.2f) {
				AI.destination = other.gameObject.transform.position; // "Angry" partygoers will act selfishly and flock to other players
			}
			if (Random.Range (0f, 1f) <= anger - 0.5f) { // maybe say hi
				emotion.Emote ("Yes");
				if (Random.Range (0, 2) == 0) {
					EmoteAnim (1f, "Hi");
				} else {
					EmoteAnim (1f, "Wave");
				}
			}
		}
		if (other.gameObject.tag == "outerPlayer") { // if the partygoer runs into another player

			if (Random.Range (0f, 1f) <= anger - 0.5f) {
				AI.speed = minSpeed;
				AI.destination = other.gameObject.transform.position; // "Angry" partygoers will walk towards the player
			}
			if (Random.Range (0f, 1f) <= anger - 0.5f) { // maybe say what
				emotion.Emote ("Maybe");
				EmoteAnim (1f, "Neutral");

			}
		}
		if (other.gameObject.tag == "intimatePlayer") { // if the partygoer gets too close to the player

			if (Random.Range (0f, 1f) <= anger - 0.5f) {
				// Play freakout anim
				DepressPlayer (anger);
			}
			if (Random.Range (0f, 1f) <= anger - 0.5f) { // maybe say fuck off
				emotion.Emote ("No");
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
	public void SetMood (float modifier) {
		//Modify partygoer's mood
		anger = anger + modifier;

		//Partygoer reactions
		if (modifier > 0) {
			EmoteAnim (1f, "Failure");
		} else if (modifier < 0) {
			EmoteAnim (3f, "Success");
		} else {
			EmoteAnim (1f, "Neutral");
		}
	}
}