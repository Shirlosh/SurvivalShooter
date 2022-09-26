using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainPlayerController : MonoBehaviour
{
    private bool m_isGameStarted = false;
    
    private float m_timeCounter = 0.0f;
    
    [SerializeField] private Text m_GameTimer;
    [SerializeField] private Text m_CountDown;
    [SerializeField] private GameObject m_Aim;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private float m_forcePower = 10f;
    private AudioSource m_shooting;
    private float m_health = 100f;
    [SerializeField] private ProgressBar m_healthPB;
    [SerializeField] private ProgressBar m_AmmoPB;

    // Start is called before the first frame update
    void Start()
    {
        m_healthPB.BarValue = 100f;
        m_AmmoPB.BarValue = 100f;
        m_Aim.SetActive(false);
        StartCoroutine(countDown(3.0f));
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
        if (m_isGameStarted)
        {
            m_timeCounter += Time.deltaTime;
            m_GameTimer.text = "" + m_timeCounter + " s";
        }

        if (Input.anyKeyDown)
        {
            Shoot();
        }
    }


    private void Shoot()
    {
        GetComponent<AudioSource>().Play();
        GameObject bulletRef = Instantiate(m_bullet, transform.position, Quaternion.identity, transform);
        Destroy(bulletRef, 5f);
        bulletRef.GetComponent<Rigidbody>().AddForce(m_Aim.transform.forward * m_forcePower);
        m_AmmoPB.BarValue -= 10;
    }
    
    public void TakeDamage()
    {
        m_health -= 0.1f;   
        m_healthPB.BarValue = m_health;
        if (m_health <= 0f)
        {
            SceneManager.LoadScene(0);
        }
    }


}
