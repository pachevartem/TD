﻿using System.Collections;
using System.Collections.Generic;
using ArtelVR;
using UnityEngine;
using UnityEngine.AI;

[RequireComponent(typeof(NavMeshAgent))]
public class AgentController : MonoBehaviour
{
	public NavMeshAgent _agent;
	public Vector3 Castle;
	private Vector3 _startPosition;
	public float Health = 1000;
	
	private void Awake()
	{
		Castle = EnemyController.Castl.position;
		_agent = GetComponent<NavMeshAgent>();
		_agent.speed = 1f;
	}

	private void OnEnable()
	{
		_startPosition = transform.position;	
		_agent.SetDestination(Castle);
	}


	private void OnTriggerEnter(Collider other)
	{
		Health -= 30;
		if (Health<0)
		{
			Helper.SetActive(gameObject, false);
		}
		print(Health);
	}

	private void OnDisable()
	{
		transform.position = _startPosition;
	}
}
