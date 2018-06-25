using UnityEngine;
using UnityEngine.UI;

public class PermeationImage : MonoBehaviour {

    //減少値
    [SerializeField] float val;

    private RawImage image;
    private float currentAlpha = 0.0f;

    public void Init()
    {
        currentAlpha = 1.0f;
        this.image.color = Color.white;
    }

    void Start ()
    {
        this.image = this.GetComponent<RawImage>();
    }

    void Update ()
    {
        if (currentAlpha < 0)
        {
            currentAlpha = 0.0f;
        }
        else
        {
            currentAlpha -= val;
        }

        this.image.color = new Color(1.0f, 1.0f, 1.0f, currentAlpha);
    }
}
