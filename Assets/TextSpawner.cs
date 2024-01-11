using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
public class TextSpawner : MonoBehaviour
{
    public Transform contentTransform; // Drag and drop the "Content" object here in the Inspector.
    public Text textTemplate; // Drag and drop the "TextTemplate" object here in the Inspector.
    public GameObject textContainerPrefab; // Drag and drop a prefab of the "TextContainer" GameObject here in the Inspector.

    private Color defaultColor = Color.white; // The default color for textContainerPrefab.
    private Color selectedColor = Color.red; // The color when selected.
    public GameObject FlatVacancy;
    public Cubemap[] list101_image;
    public Cubemap[] list106_image, list110_image, list115_image;
    public GameObject[] Arrows101;
    public GameObject sphere,camera1,camera2,mainpanel,building,Back_Button, Back_Button2,ButtonContainer_3D;
    
    public GameObject[] View_GameObject;
    public Toggle Available_toggle;
    private void Start()
    {
        SpawnText();
    }
    public void open()
    {
        if(Available_toggle.isOn)
        {

          FlatVacancy.SetActive(true);
        }
        else
        {
            FlatVacancy.SetActive(false);
        }
    }

    public void SpawnText()
    {
        
        for (int i = 0; i < 7; i++)
        {
            int startValue = i * 100 + 101;
            int endValue = i * 100 + 115;

            for (int j = startValue; j <= endValue; j++)
            {
                CreateTextElement(j.ToString());
            }
        }
    }

    private void CreateTextElement(string text)
    {
        // Create a new parent GameObject for the text element.
        GameObject newTextContainer = Instantiate(textContainerPrefab, contentTransform);

        // Attach the text element as a child of the parent.
        Text newText = Instantiate(textTemplate, newTextContainer.transform);

        // Set the text and make it active.
        newText.text = text;
        newText.gameObject.SetActive(true);

        // Check PlayerPrefs for saved color, or use the default color.
        Color savedColor = PlayerPrefs.HasKey(text) ? HexToColor(PlayerPrefs.GetString(text)) : defaultColor;
        newTextContainer.GetComponent<Image>().color = savedColor;

        // Add an Event Trigger component to handle click events.
        EventTrigger eventTrigger = newTextContainer.AddComponent<EventTrigger>();

        // Add PointerClick event to change color on click.
        EventTrigger.Entry clickEntry = new EventTrigger.Entry();
        clickEntry.eventID = EventTriggerType.PointerClick;
        clickEntry.callback.AddListener((eventData) => ChangeColorOnClick(newTextContainer, text));
        eventTrigger.triggers.Add(clickEntry);
    }

    private void ChangeColorOnClick(GameObject textContainer, string text)
    {
        Image containerImage = textContainer.GetComponent<Image>();

        // Toggle between defaultColor and selectedColor.
        Color currentColor = containerImage.color;
        if (currentColor == defaultColor)
        {
            containerImage.color = selectedColor;
        }
        else
        {
            containerImage.color = defaultColor;
        }

        // Save the current color to PlayerPrefs.
        PlayerPrefs.SetString(text, ColorToHex(containerImage.color));
        PlayerPrefs.Save();
    }

    // Helper method to convert Color to hex string.
    private string ColorToHex(Color color)
    {
        Color32 color32 = (Color32)color;
        return color32.r.ToString("X2") + color32.g.ToString("X2") + color32.b.ToString("X2");
    }

    // Helper method to convert hex string to Color.
    private Color HexToColor(string hex)
    {
        Color color;
        ColorUtility.TryParseHtmlString("#" + hex, out color);
        return color;
    }
    public int value;
    public void View(int i)
    {
        mainpanel.SetActive(false);
        FlatVacancy.SetActive(false);
        camera1.SetActive(false);
        building.SetActive(false);
        camera2.SetActive(true);
        Back_Button.SetActive(true);
        value = i;
        View_GameObject[i].SetActive(true);
        ButtonContainer_3D.SetActive(false);
    }
    public void click_Views(int i)
    {
        Back_Button2.SetActive(true);
        sphere.SetActive(true);
        View_GameObject[value].SetActive(false);
        if(value==0)
        {

        sphere.GetComponent<MeshRenderer>().material.SetTexture("_Tex", list101_image[i]);
            Arrows101[i].SetActive(true);
            for (int j = 0; j < Arrows101.Length; j++)
            {
                if (j != i)
                {
                    Arrows101[j].SetActive(false);
                }
            }
        }
        if (value == 1)
        {

            sphere.GetComponent<MeshRenderer>().material.SetTexture("_Tex", list106_image[i]);
        }
        if (value == 2)
        {

            sphere.GetComponent<MeshRenderer>().material.SetTexture("_Tex", list110_image[i]);
        }
        if (value == 3)
        {

            sphere.GetComponent<MeshRenderer>().material.SetTexture("_Tex", list115_image[i]);
        }
    }
    public void Back_Click()
    {
        mainpanel.SetActive(true);
        FlatVacancy.SetActive(false);
        camera1.SetActive(true);
        building.SetActive(true);
        camera2.SetActive(false);
        Back_Button.SetActive(false);
        Available_toggle.isOn = false;
        View_GameObject[value].SetActive(false);
        ButtonContainer_3D.SetActive(true);
        
        for (int j = 0; j < Arrows101.Length; j++)
        {
            
                Arrows101[j].SetActive(false);
            
        }
    }
    public void Back_CLick_View()
    {
        Back_Button2.SetActive(false);
        sphere.SetActive(false);
        View_GameObject[value].SetActive(true);
        
    }
}