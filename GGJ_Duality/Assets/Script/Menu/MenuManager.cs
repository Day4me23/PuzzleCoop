using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class MenuManager : MonoBehaviour
{
    

    [SerializeField] private GameObject mainMenu;
    [SerializeField] private GameObject settingsMenu;
    [SerializeField] private GameObject levelsMenu;

    //UI buttons for event system toggles
    [SerializeField] private GameObject settingsButton;
    [SerializeField] private GameObject levelsButton;
    [SerializeField] private GameObject volumeSlider;
    [SerializeField] private GameObject firstLevelButton;
    private bool settingsOn;
    private bool levelsOn;
    public void HandleSettings() {
        EventSystem.current.currentSelectedGameObject.GetComponent<Menu_Button>().DecreaseSize(0.05f);
        if (!settingsOn) {
            //turn on the settings menu
            mainMenu.SetActive(false);
            settingsMenu.SetActive(true);
            settingsOn = true;
            EventSystem.current.SetSelectedGameObject(volumeSlider);
        } else {
            //turn off the settings menu
            settingsMenu.SetActive(false);
            mainMenu.SetActive(true);
            settingsOn = false;
            EventSystem.current.SetSelectedGameObject(settingsButton);
        }
    }

    public void HandleLevels() {
        EventSystem.current.currentSelectedGameObject.GetComponent<Menu_Button>().DecreaseSize(0.05f);
        if (!levelsOn) {
            //turn on level selection
            mainMenu.SetActive(false);
            levelsMenu.SetActive(true);
            levelsOn = true;
            EventSystem.current.SetSelectedGameObject(firstLevelButton);

        } else {
            levelsMenu.SetActive(false);
            mainMenu.SetActive(true);
            levelsOn = false;
            EventSystem.current.SetSelectedGameObject(levelsButton);
        }
    }

    public void QuitGame() {
        Debug.Log("Quitting Game...");
        Application.Quit();
    }

    public void LoadLevel(int levelNum) {
        SceneManager.LoadScene(levelNum);
    }

}
