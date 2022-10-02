using System;
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
    private float m_deathCounter = 0.0f;

    [SerializeField] private Text m_GameTimer;
    [SerializeField] private Text m_CountDown;
    [SerializeField] private Text m_ExitInstructions;
    [SerializeField] private GameObject m_Aim;
    [SerializeField] private GameObject m_bullet;
    [SerializeField] private float m_forcePower = 10f;
    [SerializeField] private AudioSource music;
    private AudioSource m_shooting;
    private float m_health = 100f;
    private bool isDead = false;
    [SerializeField] private ProgressBar m_healthPB;
    [SerializeField] private ProgressBar m_AmmoPB;
    public LevelManager p_LevelManagerRef;

    private bool reloading = false;
    private bool canShoot = true;

    // Start is called before the first frame update
    void Start()
    {
        if (CrossSceneInformation.Music 
        == false)
        {
        
            music.Stop();
            
        }
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
            m_CountDown.text = "" + (int)countDownAmount;
        }

        m_isGameStarted = true;
        m_CountDown.gameObject.SetActive(false);
        m_ExitInstructions.gameObject.SetActive(false);
        m_Aim.SetActive(true);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isGameStarted)
        {
            if (isDead)
            {
                m_deathCounter += Time.deltaTime;
                if (m_deathCounter >= 1.5f)
                {
                    p_LevelManagerRef.EndGame(m_timeCounter);
                }
            }
            else
            {
                m_timeCounter += Time.deltaTime;
                m_GameTimer.text = "" + (int)Math.Round(m_timeCounter) + " s";
            }
        }

        if (Input.anyKeyDown)
        {
            if (canShoot)
            {
                Shoot();
            }
        }
    }


    private void Shoot()
    {
        GetComponents<AudioSource>()[0].Play();
        GameObject bulletRef = Instantiate(m_bullet, transform.position, Quaternion.identity, transform);
        
        Destroy(bulletRef, 2f);
        bulletRef.GetComponent<Rigidbody>().AddForce(m_Aim.transform.forward * m_forcePower);
        m_AmmoPB.BarValue -= 10;
        if (m_AmmoPB.BarValue == 0f)
        {
            StartCoroutine(Reload());
        }
    }

    private IEnumerator Reload()
    {
        canShoot = false;
        reloading = true;
        AudioSource source = GetComponents<AudioSource>()[3];
        source.Play();
        yield return new WaitForSeconds(2f);
        m_AmmoPB.BarValue = 100f;
        canShoot = true;
        reloading = false;
    }

    public void TakeDamage()
    {
        if (!isDead)
        {
            AudioSource soundHit = GetComponents<AudioSource>()[1];
            if (soundHit.isPlaying == false)
            {
                soundHit.Play();
            }

            m_health -= 0.1f;
            m_healthPB.BarValue = (int)Math.Round(m_health);
            if (m_health <= 0f)
            {
                isDead = true;
                canShoot = false;
                AudioSource soundDeath = GetComponents<AudioSource>()[2];
                soundDeath.Play();
            }   
        }
    }
}