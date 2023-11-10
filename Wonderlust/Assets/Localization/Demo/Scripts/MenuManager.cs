using UnityEngine;
using DFTGames.Localization;

public class MenuManager : MonoBehaviour
{
    #region Public Methods
    public static string lingua;

    private void Start()
    {
        lingua = "ita";
    }

    public void SetEnglish()
    {
        Localize.SetCurrentLanguage(SystemLanguage.English);
        LocalizeImage.SetCurrentLanguage();
        lingua = "eng";
    }

    public void SetItalian()
    {
        Localize.SetCurrentLanguage(SystemLanguage.Italian);
        LocalizeImage.SetCurrentLanguage();
        lingua = "ita";
    }

    public void SetFrench()
    {
        Localize.SetCurrentLanguage(SystemLanguage.French);
        LocalizeImage.SetCurrentLanguage();
        lingua = "fra";
    }

    #endregion Public Methods
}
