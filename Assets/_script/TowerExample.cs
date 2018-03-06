using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ArtelVR
{
	[CreateAssetMenu(fileName = "Data", menuName = "ArtelVR/TypeTower", order = 1)]
	public class TowerExample : ScriptableObject, IGetObj //TODO: а нежен ли вообще интерфейс
	{
		[Header("Название башни")] public string Name;
		[Header("Модели башни")] public List<GameObject> Levels;
		
		[Header("Префаб снаряда")] public GameObject Bolt;
		[Header("Слой врагов")] public LayerMask EnemyLayer;
		[Header("Радиус Башни")] 
		[Range(3,15)]
		public float Raduis;
		[Header("Урон Башни")] 
		public float Damage;
		[Header("Количество урона")] public float CountHit;
		[Header("Частота выстрелов")] public float FireDelay;
		[Header("Преумножитель за уровень ")] 
		[Range(1,5)]
		public float MultyplayHit;

		public GameObject GetModel()
		{
			return Bolt;
		}
	}
	
}
