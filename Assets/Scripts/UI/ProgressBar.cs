
using UnityEngine;
using UnityEngine.UI;

public class ProgressBar : MonoBehaviour
{

    private Image image;
    public void Start()
    {
        image = transform.GetComponent<Image>();
        image.type = Image.Type.Filled;
        image.fillMethod = Image.FillMethod.Horizontal;
        image.fillOrigin = 0;
    }

    public void SetProgressValue(float value)
    {
        image.fillAmount = value;
    }

    public void AddProgressValue(float value)
    {
        image.fillAmount += value;
    }

    
}
