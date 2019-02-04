using UnityEngine;
using UnityEngine.UI;

public class SupplierStart : MonoBehaviour {
    public Canvas market;
    public Canvas eventC;
    public Canvas houses;
    public Canvas pause;
    public Canvas tarif;
    public Canvas intro;
    public Canvas endOfGame;
    public Text txtPlayer;
    
    // script used for disabling the canvasses at the start and to update the player name and score field
	void Start () {
        intro.enabled = true;
        market.enabled = false;
        eventC.enabled = false;
        houses.enabled = false;
        pause.enabled = false;
        tarif.enabled = false;
        endOfGame.enabled = false;
	}
    private void Update()
    {
        txtPlayer.text = "Player: " + DataScript.GetName() + " Score: " + DataScript.GetScore();
    }
}