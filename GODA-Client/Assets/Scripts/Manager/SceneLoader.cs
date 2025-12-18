using System.Collections;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;


public class SceneLoader : SceneSingleMono<SceneLoader>
{
	[SerializeField]
	private Animator animator;

	[SerializeField]
	private float LoadingTime = 1f;

	public void LoadNextLevel(int SceneIndex)
	{
		FindFirstObjectByType<EventSystem>()?.gameObject.SetActive(false);
		StartCoroutine(LoadLevel(
			SceneManager.GetActiveScene().buildIndex + 1));
	}
	public void LoadScene(string name)
	{
		FindFirstObjectByType<EventSystem>()?.gameObject.SetActive(false);
		StartCoroutine(LoadLevel(name));
	}
	IEnumerator LoadLevel(int levelIndex)
	{
		//animator.SetTrigger("Start");

		yield return new WaitForSeconds(LoadingTime);

		SceneManager.LoadScene(levelIndex);
	}

	IEnumerator LoadLevel(string name)
	{
		//animator.SetTrigger("Start");

		yield return new WaitForSeconds(LoadingTime);

		SceneManager.LoadScene(name);
	}
}
