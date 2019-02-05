using UnityEngine;
using UnityEngine.UI;

public class SupplierHomeCanvasScript : MonoBehaviour
{
    public Canvas familyCanvas;
    private static System.Random random = new System.Random();
    private string familyName;
    [HideInInspector] public float HighExpectations;
    [HideInInspector] public float LowExpectations;
    private string EnergyPreference;
    private int hapyness;

    public Slider hapSlider;
    public Text famName;
    public Text high;
    public Text low;
    public Text energyPref;

    private static string[] energyPreferences = new string[] { "Green energy", "Dirty energy" };
    private static string[] names = new string[] { "smith", "Johnson", "Brown", "Jenkins", "White", "Cook", "Coleman", "Ross", "Barnes", "Patterson", "Washington", "Miller", "Davis",
        "Martinez", "Clark", "Walker", "Turner", "Edwards", "Collins", "Gray", "Watson", "Diaz", "Lautner", "Gomez", "Cyrus", "Stewart", "Moore", "Pearce",
        "Bennet", "Davies", "McDonald", "McLean", "McCartney", "Simpson", "Herbert", "Richards", "O'Brien", "Morgan", "Brown", "Bourne", "Evans", "Watts",
        "James", "James", "Reeve", "Hunter", "Walker", "Young", "Scott", "Crawford", "Walters", "Adams", "Garret", "Latham", "Dixton", "Wyncall", "Garneys",
        "Deacons", "Jones", "Khan", "Ali", "Foremoon", "Clarck", "Stark", "Hawkins", "Blueberry", "Salvatore", "Russo", "Mars", "Sheeran", "Cocoleto", "Capassio",
        "Avery", "Beck", "Baxter", "Bean", "Bernard", "Beverly", "Blackwood", "Blue", "Clyde", "Bowie", "Buckley", "Bryson", "Bullock", "Bunker", "Butcher",
        "Chester", "Coke", "Stephens", "Stanley", "Stafford", "Tatum", "Thurstan", "Tinker", "Thorne", "Timberlake", "Vernon", "Wallace", "Wade", "Wanyne" };

    // script used to randomly generate the houses canvasses
    void OnEnable()
    {
        familyName = names[random.Next(100)];
        hapyness = random.Next(250, 1000);
        HighExpectations = (float)random.Next(20, 30)/100f;
        LowExpectations = (float)random.Next(10, 20)/100f;
        EnergyPreference = energyPreferences[random.Next(1)];
        //Activate cubes AFTER houses have been done, this is needed because the cubes need information about the house it belongs to before it can check its colour.
        transform.GetChild(0).gameObject.SetActive(true);
    }

    // when clicking on a house, a house canvas is generated
    public void OnMouseDown()
    {
        //If there is another pop up shown, a player can't show the house popup. This forces the player to chose what he/she spends his/her time on.
        if (!CameraControl.showingPopUp)
        {
            if (!UnityEngine.EventSystems.EventSystem.current.IsPointerOverGameObject())
            {
                CameraControl.showingPopUp = true;
                famName.text = familyName;
                high.text = HighExpectations.ToString();
                low.text = LowExpectations.ToString();
                energyPref.text = EnergyPreference;
                hapSlider.value = hapyness;
                familyCanvas.enabled = true;
            }
        }
    }

    public int getHapyness()
    {
        return this.hapyness;
    }
}