using UnityEngine;

public class CubeSatisfaction : MonoBehaviour
{
    private int houseSatisfaction;
    private float HighExpectations;
    private float LowExpectations;
    private int frameCounter;
    private Renderer cubeRenderer;
    private static System.Random random = new System.Random();
    private bool IsStartUp = true;
    private Color32 ourColour = new Color32(19, 101, 128, 1);
    private Color32 competitionColour = new Color32(255, 156, 77, 1);

    // script used for the cubes
    void Start()
    {
        cubeRenderer = GetComponent<Renderer>();
        HomeCanvasScript homeCanvasScript = transform.parent.GetComponent<HomeCanvasScript>();
        houseSatisfaction = homeCanvasScript.getHapyness();
        HighExpectations = homeCanvasScript.HighExpectations;
        LowExpectations = homeCanvasScript.LowExpectations;
        transform.parent = null;
        checkColour();
        IsStartUp = false;
        frameCounter = random.Next(0, 170);
    }

    void Update()
    {
        if (frameCounter == 180)
        {
            frameCounter = 0;
            checkColour();
        }
        else
        {
            frameCounter++;
        }
    }
    //Sets the cube's colour. This is based on satisfaction, high and low tarif. 2 of the customers expectations HAVE to be met. 
    //The last one needs to fall within a 10% interval of the expectations
    private void checkColour()
    {
        double SupplierHighValue = FindObjectOfType<TarifScript>().HighTarif;
        double SupplierLowValue = FindObjectOfType<TarifScript>().LowTarif;
        double SupplierSatisfaction = Satisfaction.satisfaction;
        if (SupplierSatisfaction <= houseSatisfaction)
        {
            if (houseSatisfaction <= (SupplierSatisfaction * 1.05))
            {
                if (HighExpectations >= SupplierHighValue && LowExpectations >= SupplierLowValue)
                {
                    //Check if same colour, otherwise we would add customers that are already ours. We only need to change colour and add/remove customers if the colour changes.
                    if (cubeRenderer.material.color != ourColour)
                    {
                        cubeRenderer.material.color = ourColour;
                        Satisfaction.numberOfCustomers++;
                    }
                }
                else
                {
                    if (cubeRenderer.material.color != competitionColour)
                    {
                        cubeRenderer.material.color = competitionColour;
                        if(!IsStartUp)
                        {
                            Satisfaction.numberOfCustomers--;
                        }
                    }
                }
            }
            else
            {
                if (cubeRenderer.material.color != competitionColour)
                {
                    cubeRenderer.material.color = competitionColour;
                    if(!IsStartUp)
                    {
                        Satisfaction.numberOfCustomers--;
                    }
                }
            }
        }
        else
        {
            if (SupplierLowValue <= LowExpectations)
            {
                if (SupplierHighValue <= HighExpectations)
                {
                    if (cubeRenderer.material.color != ourColour)
                    {
                        cubeRenderer.material.color = ourColour;
                        Satisfaction.numberOfCustomers++;
                    }
                }
                else
                {
                    if ((SupplierHighValue * 0.95) <= HighExpectations)
                    {
                        if (cubeRenderer.material.color != ourColour)
                        {
                            cubeRenderer.material.color = ourColour;
                            Satisfaction.numberOfCustomers++;
                        }
                    }
                    else
                    {
                        if (cubeRenderer.material.color != competitionColour)
                        {
                            cubeRenderer.material.color = competitionColour;
                            if (!IsStartUp)
                            {
                                Satisfaction.numberOfCustomers--;
                            }
                        }
                    }
                }
            }
            else
            {
                if (SupplierHighValue <= HighExpectations)
                {
                    if ((0.95 * SupplierLowValue) < LowExpectations)
                    {
                        if (cubeRenderer.material.color != ourColour)
                        {
                            cubeRenderer.material.color = ourColour;
                            Satisfaction.numberOfCustomers++;
                        }
                    }
                    else
                    {
                        if (cubeRenderer.material.color != competitionColour)
                        {
                            cubeRenderer.material.color = competitionColour;
                            if (!IsStartUp)
                            {
                                Satisfaction.numberOfCustomers--;
                            }
                        }
                    }
                }
                else
                {
                    if (cubeRenderer.material.color != competitionColour)
                    {
                        cubeRenderer.material.color = competitionColour;
                        if (!IsStartUp)
                        {
                            Satisfaction.numberOfCustomers--;
                        }
                    }
                }
            }
        }
    }
}