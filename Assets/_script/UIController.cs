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

		private CellController _currentCell; //TODO: Надовспомнить зачем ты статичная

		public static event Upgrade CheckMoney;

		public static float Bank;
		
		public Text UiText;
		
		private void Update()
		{
			UiText.text = Bank.ToString();
			
			if (CheckMoney!=null)
			{
				CheckMoney(ChCesh(100));				
			}
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
			_currentCell = cell;
		}

		public void SetActiveUIChooseTower(bool choose)
		{
			UIChooseTower.SetActive(choose);

			if (!choose)
			{
				_currentCell = null;
			}
		}

		public void ChooseTypeTower(int numberTower)
		{
			if (numberTower == 0)
			{
				_currentCell.ConstructorSell(GameController.Instance.GameSettings.TowerType[0]);
			}
			if (numberTower == 1)
			{
				_currentCell.ConstructorSell(GameController.Instance.GameSettings.TowerType[1]);
			}
		}



	}
}