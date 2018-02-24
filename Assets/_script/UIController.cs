using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;


namespace ArtelVR
{

	public class UIController : MonoBehaviour
	{
		[Header("Канвас с выбором типа башни")]
		public GameObject UIChooseTower;
		
		Cell currentCell; //TODO: Надовспомнить зачем ты статичная

		[Header("Перетяни сюда типы башень (созданные ScriptableObject")]
		public List<TowerExample> TowersType  = new List<TowerExample>();
		
		private void Awake()
		{
			InputController.OnClickCell += OnOnClickCell;
			Vector3 a; 
			
		}

		private void OnOnClickCell(Cell cell)
		{
			SetActiveUIChooseTower(true);
			currentCell = cell;
		}

		public void SetActiveUIChooseTower(bool choose)
		{
			UIChooseTower.SetActive(choose);

			if (!choose)
			{
				currentCell = null;
			}
		}

		public void ChooseTypeTower(int numberTower)
		{
			if (numberTower == 0)
			{
				currentCell.SetupCell(TowersType[0]);
			}
			if (numberTower == 1)
			{
				currentCell.SetupCell(TowersType[1]);
			}
			
			
		}



	}
}