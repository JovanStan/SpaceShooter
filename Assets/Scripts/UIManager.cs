using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
   public static UIManager instance;

	public TextMeshProUGUI healthText;
	public TextMeshProUGUI scoreText;
	public TextMeshProUGUI bestScoreText;
	public Slider healthSlider;

	public GameObject gameOverPanel;
	public GameObject pausePanel;

	private void Awake()
	{
		if(instance == null)
		{
			instance = this;
		}
		else
		{
			Destroy(gameObject);
		}
	}

	public void Restart()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
	}

	public void MainMenu()
	{
		SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
	}

	public void PauseGame()
	{
		pausePanel.SetActive(true);
		Time.timeScale = 0.0f;
	}
	public void UnpauseGame()
	{
		pausePanel.SetActive(false);
		Time.timeScale = 1.0f;
	}
}
