using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ExitManager : MonoBehaviour
{
    void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.name.ToLower().Contains("bullet"))
        {
            SceneManager.LoadScene(0);
        }
    }
}
