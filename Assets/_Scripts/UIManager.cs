using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    public TextMeshProUGUI simulateButtonText;
    public GameObject leftTextPanel;
    public TextMeshProUGUI leftPanelNameText;
    public TextMeshProUGUI leftPanelMassText;
    public TextMeshProUGUI leftPanelVelocityText;
    public GameObject simulateButton;
    public GameObject hideUIButton;

    private bool isUIHidden;

    // Start is called before the first frame update
    void Start()
    {
        PlanetManager.instance.OnGameModeChange.AddListener(OnGameModeChange);
        PlanetManager.instance.OnFocusPlanetChanged.AddListener(HandleCloestPlanetChange);
        PlanetManager.instance.OnUIHiddenChanged.AddListener(ToogleALlUI);
        OnGameModeChange(PlanetManager.instance.currentMode);
    }

    private void OnGameModeChange(GameMode mode)
    {
        simulateButtonText.text = mode == GameMode.Simulation ? "Pause" : "Simulate";
        if(mode == GameMode.Edit)
        {
            leftTextPanel.SetActive(true);
        }
        else
        {
            leftTextPanel.SetActive(false);
        }
    }

    private void HandleCloestPlanetChange(Planet planet)
    {
        leftPanelNameText.text = $"Planet Name: {planet.name}";
        leftPanelMassText.text = $"Mass: { Mathf.Round(planet.Mass* 100f) / 100f} units";
        leftPanelVelocityText.text = "";//$"Velocity: {planet.velocity}";
    }

    public void ToogleALlUI()
    {
        isUIHidden = !isUIHidden;
        if (isUIHidden)
        {
            leftTextPanel.SetActive(false);
            simulateButton.SetActive(false);
            hideUIButton.SetActive(false);
        }
        else
        {
            if(PlanetManager.instance.currentMode == GameMode.Edit)
            {
                leftTextPanel.SetActive(true);
            }
            simulateButton.SetActive(true);
            hideUIButton.SetActive(true);
        }
        PlanetManager.instance.isUIHidden = isUIHidden;
    }
}
