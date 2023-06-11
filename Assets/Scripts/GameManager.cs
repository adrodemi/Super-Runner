using System.Collections;
using UnityEngine;
using TMPro;

public class GameManager : MonoBehaviour
{
    [SerializeField] int score;
    private PlayerController playerControllerScript;
    public Transform startingPoint;
    public float lerpSpeed;
    public TextMeshProUGUI scoreText;
    public GameObject titleScreen, defeatScreen;
    void Start()
    {
        Time.timeScale = 0f;
        playerControllerScript = GameObject.Find("Player").GetComponent<PlayerController>();
        score = 0;
        playerControllerScript.gameOver = true;
        StartCoroutine(PlayIntro());
    }
    void Update()
    {
        if (!playerControllerScript.gameOver)
        {
            if (playerControllerScript.doubleSpeed)
            {
                score += 2;
            }
            else
            {
                score++;
            }
        }
        scoreText.SetText("Score: " + score);
    }

    IEnumerator PlayIntro()
    {
        Vector3 startPos = playerControllerScript.transform.position;
        Vector3 endPos = startingPoint.position;
        float journeyLength = Vector3.Distance(startPos, endPos);
        float startTime = Time.time;

        float distanceCovered = (Time.time - startTime) * lerpSpeed;
        float fractionOfJourney = distanceCovered / journeyLength;

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 
            0.5f);
        while (fractionOfJourney < 1)
        {
            distanceCovered = (Time.time - startTime) * lerpSpeed;
            fractionOfJourney = distanceCovered / journeyLength;
            playerControllerScript.transform.position = Vector3.Lerp(startPos, endPos, 
                fractionOfJourney);
            yield return null;
        }

        playerControllerScript.GetComponent<Animator>().SetFloat("Speed_Multiplier", 1f);
        playerControllerScript.gameOver = false;
    }
    public void StartGame()
    {
        Time.timeScale = 1f;
        titleScreen.gameObject.SetActive(false);
    }
    public void RestartGame()
    {
        playerControllerScript.gameOver = false;
    }
    public void ExitGame()
    {
        //UnityEditor.EditorApplication.isPlaying = false;
        Application.Quit();
    }
}