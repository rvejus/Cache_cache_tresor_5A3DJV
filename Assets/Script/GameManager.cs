using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public static GameManager Instance;
    public List<GameObject> playersObjects = new List<GameObject>();
    public List<GameObject> cylinders = new List<GameObject>(2);
    [SerializeField]
    private List<GameObject> cylindersPrefab = new List<GameObject>();
    public int gameState;
    public bool gamePlays = false;
    [SerializeField]
    float timeWait;
    private int iter=0;
    private Vector3 chngScale = new Vector3(-0.1f, 0f, -0.1f);
    private void Awake()
    {
        {
            if (Instance == null)
            {
                DontDestroyOnLoad(gameObject);
                Instance = this;
            }
            else if (Instance != this)
            {
                Destroy (gameObject);
            }

        }
    }

    public void winner(int playerID)
    {
        Debug.Log("player "+playerID+" is the winner !");
    }

    public void timerBegin()
    {
        StartCoroutine(timer());
    }

    IEnumerator timer()
    {
        while (gamePlays)
        {
            yield return new WaitForSeconds(timeWait);
            if (iter % 2 == 0)
            {
                playSoundHint();
                iter++;
            }else if (iter % 2 == 1)
            {
                cylinderHint();
                iter++;
            }
        }
    }

    public void playSoundHint()
    {
        if (playersObjects[0] != null)
        {
            playersObjects[0].GetComponent<AudioSource>().Play();
        }
        if (playersObjects[1] != null)
        {
            playersObjects[1].GetComponent<AudioSource>().Play();
        }
    }

    public void cylinderHint()
    {
        if (playersObjects[0] != null)
        {
            if (cylinders[0] != null)
            {
                cylinders[0].transform.localScale +=chngScale;
            }
            else
            {
                GameObject go = Instantiate(cylindersPrefab[0],playersObjects[0].transform.position, Quaternion.Euler(Vector3.zero));
                cylinders[0] = go;
            }
        }
        if (playersObjects[1] != null)
        {
            if (cylinders[1] != null)
            {
                cylinders[1].transform.localScale +=chngScale;            
            }
            else
            {
                GameObject go = Instantiate(cylindersPrefab[1],playersObjects[1].transform.position, Quaternion.Euler(Vector3.zero));
                cylinders[1] = go;
            }
        }
    }
}
