using System;
using System.Collections;
using System.Collections.Generic;
using System.Reflection;
using UnityEngine;
using UnityEngine.UI;


namespace ArtelVR
{

	public class UIController : MonoBehaviour
	{
		[Header("Канвас с выбором типа башни")]
		public GameObject UIChooseTower;
		
		CellController currentCell; //TODO: Надовспомнить зачем ты статичная

		[Header("Перетяни сюда типы башень (созданные ScriptableObject")]
		public List<TowerExample> TowersType  = new List<TowerExample>();

		public static event Upgrade CheckMoney = b => { };

		public static float Bank;
		
		
		public Text UiText;

		private void Update()
		{
			UiText.text = Bank.ToString();
			CheckMoney(ChCesh(100));
		}

		bool ChCesh(float summa)
		{
			if (summa<0 && Mathf.Abs(summa)> Bank)
			{
				return false;
			}
			return Bank >= summa;
		}


		private void Awake()
		{
			InputController.OnClickCell += OnOnClickCell;
			Vector3 a;
			Bank = 100;

		}

		private void OnOnClickCell(CellController cell)
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
				currentCell.ConstructorSell(TowersType[0]);
			}
			if (numberTower == 1)
			{
				currentCell.ConstructorSell(TowersType[1]);
			}
		}



	}
}