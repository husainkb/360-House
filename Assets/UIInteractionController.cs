using UnityEngine;
using UnityEngine.EventSystems;

public class UIInteractionController : MonoBehaviour, IPointerDownHandler, IPointerUpHandler
{
    private bool isUIInteracting = false;

    public void OnPointerDown(PointerEventData eventData)
    {
        isUIInteracting = true;
        // Disable camera rotation and zoom when UI interaction starts
        Camera.main.GetComponent<CameraRotation>().enabled = false;
    }

    public void OnPointerUp(PointerEventData eventData)
    {
        isUIInteracting = false;
        // Enable camera rotation and zoom when UI interaction ends
        Camera.main.GetComponent<CameraRotation>().enabled = true;
    }

    public bool IsUIInteracting()
    {
        return isUIInteracting;
    }
}