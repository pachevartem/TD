using UnityEngine;
using UnityEngine.SceneManagement;

public class myScenceController : MonoBehaviour {

	public void ChangeScense(int numberScense)
	{
		SceneManager.LoadScene(numberScense);
	}
}
