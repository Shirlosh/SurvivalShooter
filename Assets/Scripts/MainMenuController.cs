using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuController : MonoBehaviour
{
    [SerializeField] private GameObject mainMenuButtons;
    [SerializeField] private GameObject topScoreTable;
    [SerializeField] private AudioSource music;
    private bool topScoreTableIsActive;

    private void Start()
    {
        mainMenuButtons.SetActive(true);
        topScoreTable.SetActive(false);

        topScoreTableIsActive = false;
        if (CrossSceneInformation.Music)
        {
            if (CrossSceneInformation.Music.Equals(false))
            {
                music.Stop();
            }
        }
        else
        {
            CrossSceneInformation.Music = music.isPlaying;
        }
    }

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
                    mainMenuButtons.SetActive(false);
                    topScoreTable.SetActive(true);
                    topScoreTableIsActive = true;
                }
                else if (hit.collider.gameObject.name.ToLower().Contains("start"))
                {
                    SceneManager.LoadScene(1);
                }
                else if (hit.collider.gameObject.name.ToLower().Contains("music"))
                {
                    if (music.isPlaying)
                    {
                        music.Stop();
                    }
                    else
                    {
                        music.Play();
                    }

                    CrossSceneInformation.Music = music.isPlaying;
                }
                else if (hit.collider.gameObject.name.Equals("Exit"))
                {
                    if (topScoreTableIsActive == true)
                    {
                        mainMenuButtons.SetActive(true);
                        topScoreTable.SetActive(false);
                        topScoreTableIsActive = false;
                    }
                    else
                    {
                        Application.Quit();
                    }
                }
            }
        }
    }
}