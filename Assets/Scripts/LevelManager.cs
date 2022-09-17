using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    [SerializeField] private GameObject m_EnemyRef;

    [SerializeField] private List<GameObject> m_Enemies;

    [SerializeField] public GameObject[] m_walkThrogh;

    [SerializeField] private int m_ontensityLevel = 1;

    private int m_enemyCounter = 0;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(EnemyCreator());
    }

    
    // Update is called once per frame
    IEnumerator EnemyCreator()
    {
        while (true)
        {
            yield return new WaitForSeconds((1f*(6/m_ontensityLevel)));
            m_enemyCounter++;
            if (m_enemyCounter > 10)
            {
                m_ontensityLevel += 1;
                m_enemyCounter = 0;
            }

           GameObject currentEnemy =  Instantiate(m_EnemyRef,
                m_walkThrogh[((int) Random.Range(0f, (float) m_walkThrogh.Length))].transform.position,
                Quaternion.identity, transform);
           
           m_Enemies.Add(currentEnemy);
           
           currentEnemy.GetComponent<EnemyController>().p_LevelManagerRef = this;
           currentEnemy.GetComponent<EnemyController>().p_speed = Random.Range(1f, 5f);
        }
    }
}
