using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverController : MonoBehaviour
{
        public Text pointsText;

        private void Start()
        { 
                pointsText.text = CrossSceneInformation.Score + " POINTS";
                HighscoreTable.AddHighScoreEntry(CrossSceneInformation.Score, CrossSceneInformation.TimePlayed);
        }

        void Update()
        {
                if (Input.anyKeyDown)
                {
                        RaycastHit hit;
                        Ray ray = Camera.main.ScreenPointToRay(new Vector3(Screen.width / 4f, Screen.height / 2f, 0f));
                        if (Physics.Raycast(ray, out hit))
                        {
                                if (hit.collider.gameObject.name.ToLower().Contains("restart"))
                                {
                                        SceneManager.LoadScene(1);
                                }
                                else if (hit.collider.gameObject.name.ToLower().Contains("main"))
                                {
                                        SceneManager.LoadScene(0);
                                }
                        }
                }
        }
}