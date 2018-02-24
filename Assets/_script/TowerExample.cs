using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
	[CreateAssetMenu(fileName = "Data", menuName = "ArtelVR/TypeTower", order = 1)]
	public class TowerExample : ScriptableObject
	{
		[Header("Название башни")] public string Name;
		[Header("Модели башни")] public List<GameObject> Levels;
		[Header("Звук окончания строительства")] public AudioClip CompleteBuilding;
		[Header("Префаб снаряда")] public GameObject Bolt;
		
		[Header("Радиус Башни")] 
		[Range(3,5)]
		public float Raduis;
		
		[Header("Количество урона")] public float CountHit;
		
		[Header("Преумножитель за уровень ")] 
		[Range(1,5)]
		public float MultyplayHit;

	}
	
}
