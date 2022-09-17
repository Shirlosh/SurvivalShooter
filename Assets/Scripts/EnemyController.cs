using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public LevelManager p_LevelManagerRef;

    public float p_speed = 1f;

    public int m_targetPos = 1;
    // Start is called before the first frame update
    void Start()
    {
        DeclareNewTarget();
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = Vector3.Lerp(transform.position,
            p_LevelManagerRef.m_walkThrogh[m_targetPos].transform.position, Time.deltaTime / p_speed);
        
        if (Vector3.Distance(transform.position , p_LevelManagerRef.m_walkThrogh[m_targetPos].transform.position) < 0.5f)
        {
            DeclareNewTarget();
        }
    }

    private void DeclareNewTarget()
    {
        m_targetPos = (int) Random.Range(0f,(float)p_LevelManagerRef.m_walkThrogh.Length);
    }
    
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToLower().Contains("bullet"))
        {
          Destroy(gameObject);  
        }
    }
}
