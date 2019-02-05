using UnityEngine;
using UnityEngine.UI;

public class TarifScript : MonoBehaviour
{
    [HideInInspector] public double LowTarif;
    [HideInInspector] public double HighTarif;
    public Button LowPlus;
    public Button LowMin;
    public Button HighPlus;
    public Button HighMin;
    public InputField LowInputText;
    public InputField HighInputText;
    void Start()
    {
        //Randomly set the low and high tarifs.
        LowTarif = (float)new System.Random().Next(10,20)/100f;
        HighTarif = (float)new System.Random().Next(20,30)/100f;
        LowInputText.text = LowTarif.ToString();
        HighInputText.text = HighTarif.ToString();

        //Add listeners to the plus and minus buttons to increase/decrease the tarifs.
        LowMin.onClick.AddListener(()=> { LowInputText.text = (double.Parse(LowInputText.text) - 0.01).ToString(); });
        LowPlus.onClick.AddListener(() => { LowInputText.text = (double.Parse(LowInputText.text) + 0.01).ToString(); });
        HighMin.onClick.AddListener(() => { HighInputText.text = (double.Parse(HighInputText.text) - 0.01).ToString(); });
        HighPlus.onClick.AddListener(() => { HighInputText.text = (double.Parse(HighInputText.text) + 0.01).ToString(); });

        LowInputText.onValueChanged.AddListener((value) => { LowTarif = double.Parse(value); });
        HighInputText.onValueChanged.AddListener((value) => { HighTarif = double.Parse(value); });
        //Enable the homecanvasscript AFTER the player tarifs have been set. This is needed because otherwise the cubes cant check colour, for they might be called before these values are set.
        foreach (var item in FindObjectsOfType<SupplierHomeCanvasScript>())
        {
            item.enabled = true;
        }
    }   
}