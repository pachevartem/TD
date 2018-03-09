// ShowGoldenPath.cs
using UnityEngine;
using System.Collections;
using ArtelVR;
using ArtelVR.TestRun.Learn;
using UnityEngine.AI;
using GameController = ArtelVR.TestRun.Learn.GameController; //TODO: this is fuck

public class Agent : MonoBehaviour {
	
	public NavMeshAgent _agent;	
	private NavMeshPath _path;

	public GameObject TEXTobj;
	
	public LayerMask _BulletLayerMask;
	
	private float _health = 100; //TODO: change it

	public TextMesh text; 
	
	
	public float Health
	{
		get { return _health; }
		set
		{
			_health = value;
			text.text = ((int)Health).ToString();
			if (Health<=0)
			{
				gameObject.SetActive(false);
			}
		}
	}

	private void Update()
	{
		TEXTobj.transform.LookAt(Camera.main.transform);
	}

	private Vector3 _startPosition;

	private void Awake()
	{
		_startPosition = transform.position;
	}

	private void OnEnable()
	{		
		_path = new NavMeshPath();
		_agent.SetDestination(GameController.Instance.MainCastle.position);

//		print(GetDistance());

	}

	public float GetDistance()
	{
		float _dist = 0;		
		NavMesh.CalculatePath(transform.position, GameController.Instance.MainCastle.position, NavMesh.AllAreas, _path);
		
		for (int i = 0; i < _path.corners.Length - 1; i++)
		{
			_dist += Vector3.Distance(_path.corners[i], _path.corners[i + 1]);
		}
		return _dist;
	}

	private void OnTriggerEnter(Collider other)
	{
		
		if (other.gameObject.layer == 29 )
		{
			Bullet _bullet = other.gameObject.GetComponent<Bullet>();
			Health -= _bullet.GetDamage();
			_bullet.ForcedOff();
			
		}
//		print(_health);
	}


	private void OnDisable()
	{
		transform.position = _startPosition;
		Health = 100;

	}
}