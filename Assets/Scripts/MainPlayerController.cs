using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPlayerController : MonoBehaviour
{
    private bool m_isInGameScene = false;

    private bool m_isGameStarted = false;
    
    private float m_timeCounter = 0.0f;
    
    [SerializeField] private Text m_GameTimer;

    [SerializeField] private Text m_CountDown;

    [SerializeField] private GameObject m_Aim;

    [SerializeField] private GameObject m_bullet;
    
    [SerializeField] private float m_forcePower = 10f;
    
    // Start is called before the first frame update
    void Start()
    {
        if (SceneManager.GetActiveScene().name.ToLower().Contains("game"))
        {
            m_isInGameScene = true;
        }

        if (m_isInGameScene)
        {
            m_Aim.SetActive(false);
            StartCoroutine(countDown(3.0f));
        }
    }

    IEnumerator countDown(float countDownAmount)
    {
        while (countDownAmount > 0.0f)
        {
            yield return new WaitForSeconds(1.0f);
            countDownAmount -= 1.0f;
            m_CountDown.text = "" + (int) countDownAmount;
        }

        m_isGameStarted = true;
        m_CountDown.gameObject.SetActive(false);
        m_Aim.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (!m_isInGameScene)
        {//Main Menu
            if (Input.anyKeyDown)
            {
                RaycastHit hit;
                Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width/2f,Screen.height/2f,0f));
                if (Physics.Raycast(ray, out hit))
                {
                    if (hit.collider.gameObject.name.Contains("Exit"))
                    {
                        Application.Quit();
                    }
                    else
                    {
                        if (hit.collider.gameObject.name.Contains("Start"))
                        {
                            SceneManager.LoadScene(1);
                            m_isInGameScene = true;
                        }
                    }
                }
            }
        }
        else
        {//Game Scene
            if (m_isGameStarted)
            {
                m_timeCounter += Time.deltaTime;
                m_GameTimer.text = "" + m_timeCounter + " s";
            }
            
            if (Input.anyKeyDown)
            {
                GameObject bulletRef =  Instantiate(m_bullet, transform.position, Quaternion.identity, transform);
                Destroy(bulletRef,5f);
                bulletRef.GetComponent<Rigidbody>().AddForce(transform.forward * m_forcePower);
            }
        }
    }
}
