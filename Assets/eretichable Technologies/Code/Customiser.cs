using UnityEngine.UI;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Customiser : MonoBehaviour
{
    [SerializeField]
    private Image bg,
        goalkeeper,
        gates;

    [SerializeField]
    private Sprite[] bgVars,
        gkVars,
        gatesVars;

    [SerializeField]
    private Button bgPrev,
        bgNext,
        gkPrev,
        gkNext,
        gatesPrev,
        gatesNext;

    private CustomItem bgItem,
        gkItem,
        gatesItem;

    private void Start()
    {
        CustomItem bgItem = new CustomItem(bg, bgVars, "BG");
        bgPrev.onClick.AddListener(bgItem.PrevSprite);
        bgNext.onClick.AddListener(bgItem.NextSprite);

        CustomItem gkItem = new CustomItem(goalkeeper, gkVars, "GK");
        gkPrev.onClick.AddListener(gkItem.PrevSprite);
        gkNext.onClick.AddListener(gkItem.NextSprite);

        CustomItem gatesItem = new CustomItem(gates, gatesVars, "GATES");

        gatesPrev.onClick.AddListener(gatesItem.PrevSprite);
        gatesNext.onClick.AddListener(gatesItem.NextSprite);
    }

    public void BackMenu()
    {
        SceneManager.LoadScene("SettingsMenu");
    }
}

public class CustomItem
{
    private int currentIndex;
    private Image image;
    private Sprite[] sprites;
    private string playerPrefs;

    public CustomItem(Image image, Sprite[] sprites, string playerPrefs)
    {
        this.image = image;
        this.sprites = sprites;
        this.playerPrefs = playerPrefs;
        this.currentIndex = PlayerPrefs.GetInt(playerPrefs);
        image.sprite = sprites[currentIndex];
    }

    public void NextSprite()
    {
        currentIndex++;
        if (currentIndex >= sprites.Length)
        {
            currentIndex = 0;
        }
        PlayerPrefs.SetInt(playerPrefs, currentIndex);
        PlayerPrefs.Save();
        image.sprite = sprites[currentIndex];
    }

    public void PrevSprite()
    {
        currentIndex--;
        if (currentIndex < 0)
        {
            currentIndex = sprites.Length - 1;
        }
        PlayerPrefs.SetInt(playerPrefs, currentIndex);
        PlayerPrefs.Save();
        image.sprite = sprites[currentIndex];
    }
}
