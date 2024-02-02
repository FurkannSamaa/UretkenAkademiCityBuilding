using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BuildingPlacement : MonoBehaviour
{

    private bool currentlyPlacing;
    private bool currentlyBulldozering;

    private BuildingPreset curBuildingPreset;
    
    private float indicatorUpdateRate=0.05f;
    private float lastUpdateTime;
    private Vector3 curIndicatorPos;

    public GameObject placementIndicator;
    public GameObject bulldozerIndicator;

    public void BeginNewBuildingPlacement(BuildingPreset preset)
    {
        /*if (City.instance.money < preset.cost)
        {
            return;
        }*/
        currentlyBulldozering = false;
        bulldozerIndicator.SetActive(false);
        currentlyPlacing=true;
        curBuildingPreset=preset;
        placementIndicator.SetActive(true);

    }

    void CancelBuilding()
    {
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
        currentlyBulldozering = false;
        bulldozerIndicator.SetActive(false);
    }

    public void ToggleBulldoze()
    {
        currentlyBulldozering=!currentlyBulldozering;
        currentlyPlacing = false;
        placementIndicator.SetActive(false);
        bulldozerIndicator.SetActive(currentlyBulldozering);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            CancelBuilding();
        }

        if(Time.time-lastUpdateTime > indicatorUpdateRate) 
        {
            lastUpdateTime= Time.time;

            curIndicatorPos = Selector.instance.GetCurTilePosition();

            if (currentlyPlacing)
            {
                placementIndicator.transform.position = curIndicatorPos;
            }
            else if (currentlyBulldozering)
            {
                bulldozerIndicator.transform.position = curIndicatorPos;
            }

        }

        if(Input.GetMouseButtonDown(0)&& currentlyPlacing)
        {
            PlaceBuilding();
        }
        else if(Input.GetMouseButtonUp(0)&& currentlyBulldozering)
        {
            Bulldoze();
        }

    }

    void PlaceBuilding()
    {
        GameObject buildingObj = Instantiate(curBuildingPreset.prefab,curIndicatorPos,Quaternion.identity);
        CancelBuilding();
    }

    void Bulldoze()
    {

    }

}
