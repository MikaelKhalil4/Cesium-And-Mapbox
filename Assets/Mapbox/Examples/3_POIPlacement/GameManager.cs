using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameManager : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private GameObject gameOverScreen;
    [SerializeField] private Text restartText;
    [SerializeField] private Text gameOverText; // The text to animate

    [Header("Audio")]
    public AudioSource engineSound;
    public AudioSource crashSound;

    private bool isGameOver = false;

    void Start()
    {
        // Start engine sound
        engineSound.Play();

        // Initialize UI elements
        gameOverScreen.SetActive(false);
        restartText.gameObject.SetActive(false);
        gameOverText.gameObject.SetActive(false); // Ensure game over text is initially hidden
    }

    void Update()
    {
        // Only check for input if the game is over
        if (isGameOver)
        {
            // Restart the game when 'R' is pressed
            if (Input.GetKeyDown(KeyCode.R))
            {
                RestartGame();
            }

            // Quit the game when 'Q' is pressed
            if (Input.GetKeyDown(KeyCode.Q))
            {
                Application.Quit();
            }
        }
    }

    public void TriggerGameOver()
    {
        if (!isGameOver)
        {
            isGameOver = true;

            // Ensure Game Over text is visible before animating
            gameOverText.gameObject.SetActive(true);

            StartCoroutine(GameOverSequence());
        }
    }

    private IEnumerator GameOverSequence()
    {
        // Show game-over screen
        gameOverScreen.SetActive(true);

        // Stop engine sound and play crash sound
        engineSound.Stop();
        crashSound.Play();

        // Pause the game
        Time.timeScale = 0;

        // Animate the "Game Over" text
        yield return StartCoroutine(AnimateGameOverText());

        // Wait for 5 seconds before showing restart options
        yield return new WaitForSecondsRealtime(3.0f);

        // Show restart/quit text
        restartText.gameObject.SetActive(true);
    }

    private IEnumerator AnimateGameOverText()
    {
        // Initial settings
        Vector3 originalScale = gameOverText.transform.localScale;
        Vector3 targetScale = originalScale * 1.5f; // Scale up by 1.5 times
        float animationTime = 1.0f; // Animation duration

        // Scale up
        float timeElapsed = 0f;
        while (timeElapsed < animationTime)
        {
            gameOverText.transform.localScale = Vector3.Lerp(originalScale, targetScale, timeElapsed / animationTime);
            timeElapsed += Time.unscaledDeltaTime; // Use unscaledDeltaTime to ignore time scale
            yield return null;
        }
        gameOverText.transform.localScale = targetScale; // Ensure it reaches the target scale

        // Scale down
        timeElapsed = 0f;
        while (timeElapsed < animationTime)
        {
            gameOverText.transform.localScale = Vector3.Lerp(targetScale, originalScale, timeElapsed / animationTime);
            timeElapsed += Time.unscaledDeltaTime; // Use unscaledDeltaTime to ignore time scale
            yield return null;
        }
        gameOverText.transform.localScale = originalScale; // Ensure it returns to original scale
    }

    public void RestartGame()
    {
        // Hide the game-over screen
        gameOverScreen.SetActive(false);

        // Reset time scale and reload the current scene
        Time.timeScale = 1;
        isGameOver = false;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
