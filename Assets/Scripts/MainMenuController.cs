using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenuController : MonoBehaviour
{    
    [SerializeField] private GameObject m_Aim;

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKeyDown)
        {
            RaycastHit hit;
            Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 4f, Screen.height / 2f, 0f));

             if (Physics.Raycast(ray, out hit))
             {
                 if (hit.collider.gameObject.name.ToLower().Contains("top"))
                 {
                     Debug.Log("top10");
                 }
                 else
                 {
                     if (hit.collider.gameObject.name.ToLower().Contains("start"))
                     {
                         SceneManager.LoadScene(1);
                     }
                 }
            }
        }
    }
}
