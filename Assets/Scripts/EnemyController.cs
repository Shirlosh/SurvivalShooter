using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LevelManager p_LevelManagerRef;
    public float p_speed = 1f;
    private Vector3 m_target; 
    private Animation m_anim;
    private bool m_isAlive = true;
    
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animation>();
        Transform camera = Camera.main.transform;
        int spawnIndex = (int) Random.Range(0f,(float)p_LevelManagerRef.m_spawnPoints.Length);
        transform.position = p_LevelManagerRef.m_spawnPoints[0].transform.position;
        m_target = new Vector3(camera.position.x, 0, camera.position.z);
        transform.LookAt(camera);
    }

    // Update is called once per frame
    void Update()
    {
        if (m_isAlive)
        {
            if (Vector3.Distance(transform.position , m_target) < 1.5f)
            {
                Attack();
            }
            else
            {
                transform.position = Vector3.Lerp(transform.position, m_target, Time.deltaTime / p_speed);
            }
        }
    }
    
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToLower().Contains("bullet"))
        {
            StartCoroutine(Die());
        }
    }
    
    IEnumerator Die()
    {
        m_isAlive = false;
        m_anim.Stop();
        while (m_anim.isPlaying)
        {
            
        }
        m_anim.Play("Death");
        yield return new WaitForSeconds(m_anim["Death"].length);
        Destroy(gameObject);
    }

    void Attack()
    {
        m_anim.Play("Attack1");
    }
}
