using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenuNavigation : MonoBehaviour
{
    [SerializeField] private int _mainMenuSceneID;
    [SerializeField] private int _gameSceneID;

    private void Update()
    {
        if (Input.GetButtonDown("Escape"))
        {
            int sceneID = SceneManager.GetActiveScene().buildIndex;
            if (this._gameSceneID == sceneID)
                this.LoadMainMenuScene();
            else if (this._mainMenuSceneID == sceneID)
                this.QuitGame();
        }
    }

    public void LoadMainMenuScene()
    {
        SceneManager.LoadScene(this._mainMenuSceneID, LoadSceneMode.Single);
    }

    public void LoadGameScene()
    {
        SceneManager.LoadScene(this._gameSceneID, LoadSceneMode.Single);
    }

    public void QuitGame()
    {
        Application.Quit();
    }
}
