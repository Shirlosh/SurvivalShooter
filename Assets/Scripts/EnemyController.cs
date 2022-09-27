using System.Collections;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LevelManager p_LevelManagerRef;
    public float p_speed = 1f;
    private Vector3 m_target; 
    private Animation m_anim;
    private bool m_isAlive = true;
    private int m_health;
    
    // Start is called before the first frame update
    void Start()
    {
        m_anim = GetComponent<Animation>();
        Transform camera = Camera.main.transform;
        int spawnIndex = (int) Random.Range(0f,(float)p_LevelManagerRef.m_spawnPoints.Length);
        transform.position = p_LevelManagerRef.m_spawnPoints[spawnIndex].transform.position;
        m_target = new Vector3(camera.position.x, 0, camera.position.z);
        m_health = (int) Random.Range(1f, 4f);
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
            GetComponents<AudioSource>()[0].Play();
            m_health--;
            if (m_health == 0)
            {
                StartCoroutine(Die());
            }
        }
    }
    
    IEnumerator Die()
    {
        m_isAlive = false;
        m_anim.Stop();
        GetComponents<AudioSource>()[1].Play();
        while (m_anim.isPlaying)
        {
            
        }
        m_anim.Play("Death");
        yield return new WaitForSeconds(m_anim["Death"].length);
        Destroy(gameObject);
        p_LevelManagerRef.incScore();
    }

    void Attack()
    {
        GameObject.Find("Player").GetComponent<MainPlayerController>().TakeDamage();
        m_anim.Play("Attack1");
    }
}
