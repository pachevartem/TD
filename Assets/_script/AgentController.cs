using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

public class AgentController : MonoBehaviour
{
	public NavMeshAgent _agent;
	public Transform Castle;


	void Awake()
	{
		_agent.SetDestination(EnemyController.Castl.position);  
	}


	private void Update()
	{
		Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
		RaycastHit hit;

		if (Physics.Raycast(ray, out hit, 10000))
		{
			Debug.DrawLine(ray.origin, hit.point);
			if (Input.GetMouseButtonUp(0))
			{
			}
		}
		
	}
}
