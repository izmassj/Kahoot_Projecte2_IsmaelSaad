using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;

public class ClickableLink : MonoBehaviour, IPointerClickHandler
{
    [SerializeField] private TMP_Text textComponent;
    public string url;
    public string title;

    void Start()
    {
        if (textComponent == null)
            textComponent = GetComponent<TMP_Text>();

        // Formatear el texto con enlace
        textComponent.text = $"<link=\"{url}\">{title}</link>";
        textComponent.ForceMeshUpdate();
    }

    public void OnPointerClick(PointerEventData eventData)
    {
        int linkIndex = TMP_TextUtilities.FindIntersectingLink(textComponent,
            eventData.position, eventData.pressEventCamera);

        if (linkIndex != -1)
        {
            TMP_LinkInfo linkInfo = textComponent.textInfo.linkInfo[linkIndex];
            string linkID = linkInfo.GetLinkID();

            // Abrir el enlace en el navegador
            Application.OpenURL(linkID);
        }
    }
}