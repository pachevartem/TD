// ShowGoldenPath.cs
using UnityEngine;
using System.Collections;
using ArtelVR;
using UnityEngine.AI;
using GameController = ArtelVR.TestRun.Learn.GameController; //TODO: this is fuck

public class Agent_Test : MonoBehaviour {
	
	public Transform target;
	private NavMeshPath path;
	private float elapsed = 0.0f;

	public NavMeshAgent _agent;

	private MyPool EnemyType1;
	private MyPool EnemyType2;

	public GameObject SpawnAgent; //TODO: move to Scriptable
	
	
	void Start () {
//		path = new NavMeshPath();
		
		EnemyType1 = new MyPool(GameController.Instance.Enemies[0],new GameObject("For Type1").transform, SpawnAgent.transform.position,10);
		EnemyType2 = new MyPool(GameController.Instance.Enemies[1],new GameObject("For Type2").transform, SpawnAgent.transform.position,10);
		
	}
	
	
	
	void Update () {
//		DrawPath();
	}

	

	void DrawPath()
	{
		
		elapsed += Time.deltaTime;
		if (elapsed > 1.0f) {
			elapsed -= 1.0f;
			NavMesh.CalculatePath(transform.position, target.position, NavMesh.AllAreas, path);
		}

		float dist = 0;
		
		for (int i = 0; i < path.corners.Length - 1; i++)
		{
			Debug.DrawLine(path.corners[i], path.corners[i + 1], Color.red);
			dist += Vector3.Distance(path.corners[i], path.corners[i + 1]);
		}
	}
}