using UnityEngine;
using UnityEngine.SceneManagement;
using System.Text;

public class QuitGame : MonoBehaviour {

    // loads the titlescreen scene
    public void LoadTitleScreen()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("titlescreen");
    }

    // loads the level screen/scene
    public void LoadLevels()
    {
        SceneManager.LoadScene("Levels");
    }

    // loads the Megan intro
    public void LoadIntro()
    {
        SceneManager.LoadScene("MeganIntro");
    }

    // loads the introduction level
    public void LoadGame()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Introduction");
    }

    // loads the supplier level/scene
    public void LoadSupplier()
    {
        SceneManager.LoadScene("Supplier");
    }

    // loads the consumer level/scene
    public void LoadConsumer()
    {
        SceneManager.LoadScene("Consumer");
    }

    // loads the producer level/scene
    public void LoadProducer()
    {
        SceneManager.LoadScene("Producer");
    }

    // loads the TGO level/scene
    public void LoadTGO()
    {
        SceneManager.LoadScene("TGO");
    }

    // loads the DGO level/scene
    public void LoadDGO()
    {
        SceneManager.LoadScene("DGO");
    }

    // loads the Mecoms level/scene
    public void LoadMecoms()
    {
        SceneManager.LoadScene("Mecoms");
    }

    // loads the highscore screen/scene
    public void LoadHighscores()
    {
        SceneManager.LoadScene("Highscores");
    }

    public void LoadHighscore()
    {
        SceneManager.LoadScene("Highscore");
    }

    public void LoadSupport()
    {
        SceneManager.LoadScene("Support");
    }

    // closes the game
    public void DoQuit()
    {
        Application.Quit();
    }

    // when game over, append the current player highscore to the encrypted online highscores.txt file
    public void GameOver() {
        string onlineHighscoresPath = Encryption.path;

        var key = Encoding.UTF8.GetBytes(Encryption.key);
        var iv = Encoding.UTF8.GetBytes(Encryption.key);
        string line = DataScript.GetName() + ":" + DataScript.GetScore();

        Encryption.AppendStringToFile(onlineHighscoresPath, line, key, iv);
        print("Online append succesful");
        SceneManager.LoadScene("titlescreen");
    }
}