using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ScrollRectFrameWork;
using UnityEngine.UI;
using ScreenFrameWork;
using TMPro;
using PopUpFrameWork;
using Unity.VisualScripting;
public class PackListItem : ExpandableListItem<GameModeInfor>
{

    [SerializeField]
    private Image banner;
    [SerializeField]
    private GameObject locked;
    [SerializeField]
    private GameObject showLevel;
    [SerializeField]
    private TextMeshProUGUI levelText;
    [SerializeField]
    private TextMeshProUGUI starRequestText;
    [SerializeField]
    private TextMeshProUGUI gameModeText;
    private GameModeInfor dataObject;
    private RectTransform rectLocked;
    private bool unlock = false;



    public override void Initialize(GameModeInfor dataObject)
    {
        rectLocked = locked.GetComponent<RectTransform>();
    }

    public override void Setup(GameModeInfor dataObject, bool isExpanded)
    {
        rectLocked.transform.localScale = Vector3.one;
        banner.sprite = dataObject.bannerGameMode;
        gameModeText.text = dataObject.nameGameMode;
        unlock = GameManager.instance.GetTotalStarEarnAllMode >= dataObject.starRequest;
        locked.SetActive(!unlock);
        showLevel.SetActive(unlock);
        levelText.text = string.Format("{0}/{1}",( GameManager.instance.GetLevelCompleteMode(dataObject.idGameMode)), dataObject.datas.Count);
        starRequestText.text = dataObject.starRequest.ToString();

        
 



        this.dataObject = dataObject;
  
    }


    public override void Removed()
    {

    }

    public override void Collapsed()
    {

    }

    private IEnumerator ScaleBoardLocked()
    {
        float duration = .3f;
     
        UIAnimation scaleX = UIAnimation.ScaleX(rectLocked, 1.2f, duration);
        scaleX.Play();
        UIAnimation scaleY = UIAnimation.ScaleY(rectLocked, 1.2f, duration);
        scaleY.Play();
        yield return new WaitForSeconds(duration);

        scaleX = UIAnimation.ScaleX(rectLocked, 1f, duration);
        scaleX.Play();
        scaleY = UIAnimation.ScaleY(rectLocked, 1f, duration);
        scaleY.Play();
    }

    private IEnumerator ShowPopupLock()
    {
        PopupManager.instance.PopupLock.GetComponent<PopupLock>().ShowStarHaveText(GameManager.instance.TotalStarEarnAllMode()); 
        float duration = .3f;
        PopupManager.instance.Show("lock");
        yield return new WaitForSeconds(duration);
    }

    public void OnEventClicked()
    {
        if (!unlock)
        {
            StartCoroutine(ShowPopupLock());
            return;
        }

       
        GameManager.GAME_MODE = dataObject.nameGameMode;
        GameManager.instance.SelectedGameMode(dataObject.idGameMode);
        ScreenManager.Instance.Show("levels");
    }


    /*[New added background]*/
    public void showMyBackground(bool IsOn)
    {
        GameManager.instance.background.SetActive(IsOn);
    }
}
