using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Gameplay : MonoBehaviour
{
    [SerializeField]
    float minTranslation = 0.05f;
    public Text scoreTxt;
    public int scoreboard = 0;
    public string preScoreText = "0";
    public int pointsPerBounce = 35;
    public ParticleSystem deathParts;
    
    public GameObject btn_Exit;
    public GameObject btn_Retry;
    public string sceneToReload = "Game";





    // Start is called before the first frame update
    void Start()
    {
        
        btn_Exit.SetActive(false);
        btn_Retry.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnCollisionEnter(Collision collision)
    {
        Vector3 colPos = collision.transform.position;
        Rigidbody rigid = GetComponent<Rigidbody>();
        if (collision.gameObject.tag == "Paddle")
        {
            float diffX = minTranslation + colPos.x - transform.position.x * Random.Range(1, 10) ; 
            float diffz = minTranslation+ colPos.z - transform.position.z * Random.Range(1,10);

            rigid.velocity = new Vector3(-diffX, 8, -diffz);
            Debug.Log("x: " + diffX + " z: " + diffz);
            AddScore();
            IncreaseDifficulty();
        }
        else
        {
            //GameOver
            deathParts.transform.position = transform.position;
            GetComponent<TrailRenderer>().enabled = false;
            transform.position = new Vector3(-1000, -1000, -1000);
            Time.timeScale = 1.0f;
            deathParts.Play();
            btn_Exit.SetActive(true);
            btn_Retry.SetActive(true);
        }
    }

    void AddScore()
    {
        scoreboard += pointsPerBounce;
        scoreTxt.text = scoreboard.ToString("D6");
    }

    void IncreaseDifficulty()
    {
        float timeScaleLimit = 3.0f;
        float timeScaler = Time.timeScale + 0.03f;
        if (timeScaler > timeScaleLimit)
        {
            timeScaler = timeScaleLimit;
        }
        Time.timeScale = timeScaler;
    }

    public void DoQuit()
    {
        Application.Quit();
    }

    public void DoRestart()
    {
        SceneManager.LoadScene(sceneToReload);
    }
}
