using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;
using static UIManager;

public class PotalListUI : MonoBehaviour
{

    public GameObject[] buttonGameObject;
    [SerializeField] Button[] buttons;

    Button highLightButton;
    int minActiveBtnIndex;
    int maxActiveBtnIndex;
    int currentBtnIndex;

    List<int> buttonIndexList = new List<int>();

    private void Start()
    {
        InitPotalListUI();
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.UpArrow))
        {
            SelectPotalMenu(--currentBtnIndex);
        }

        if (Input.GetKeyUp(KeyCode.DownArrow))
        {
            SelectPotalMenu(++currentBtnIndex);
        }
    }
    void InitPotalListUI()
    {

        for (int i = 0; i < buttonGameObject.Length; i++)
        {
            buttons[i] = buttonGameObject[i].GetComponent<Button>();
        }
        //foreach (var button in buttonGameObject)
        //{
        //    button.gameObject.SetActive(false);
        //}
    }

    private void SelectPotalMenu(int btnIndex)
    {
        currentBtnIndex = Mathf.Clamp(btnIndex, minActiveBtnIndex, maxActiveBtnIndex);
        ReOrder();
    }

    void ReOrder()
    {
        for (int i = 0; i < buttonGameObject.Length; i++)
        {
            if (buttonGameObject[i].gameObject.activeInHierarchy && i != currentBtnIndex)
            {
                buttons[i].GetComponent<Image>().sprite = UIManager.Instance.unSelectButton;
            }
        }

        buttons[currentBtnIndex].GetComponent<Image>().sprite = UIManager.Instance.selectButton;
    }


    public void ButtonSet()
    {
        // 버튼 배열에서 활성화된 첫 번째 버튼을 찾음
        Button highLightButton = null;
        for (int i = 0; i < buttonGameObject.Length; i++)
        {
            if (buttonGameObject[i].gameObject.activeSelf)
            {
                highLightButton = buttonGameObject[i].GetComponent<Button>();
                buttonGameObject[i].GetComponent<Image>().sprite = UIManager.Instance.selectButton;
                break;
            }
        }

        // 첫 번째 활성화된 버튼이 없는 경우 종료
        if (highLightButton == null)
        {
            return;
        }

        for (int i = 0; i < buttonGameObject.Length; i++)
        {
            if (buttonGameObject[i].GetComponent<Button>() != highLightButton)
            {
                buttonGameObject[i].GetComponent<Image>().sprite = UIManager.Instance.unSelectButton;
            }
        }

        minActiveBtnIndex = FineMinIndex();
        maxActiveBtnIndex = FindMaxIndex();
        currentBtnIndex = minActiveBtnIndex;

    }

    public void ExecuteSelectedPotalMenuAction()
    {
        bool active = false;

        foreach (var button in buttonGameObject)
        {
            if (button.gameObject.activeInHierarchy)
            {
                active = true;
            }
        }

        if(active == true) buttonGameObject[currentBtnIndex].GetComponent<Button>().onClick.Invoke();

    }


    public void ShowButton(int btnIndex)
    {
        buttonIndexList.Add(btnIndex);
        buttonGameObject[btnIndex].gameObject.SetActive(true);
    }
    public void HideButton(int btnIndex)
    {
        buttonGameObject[btnIndex].gameObject.SetActive(false);
    }

    //제일 작은 버튼 인덱스 찾기
    int FineMinIndex()
    {
        if (buttonIndexList.Count > 0)
        {

            int smallest = buttonIndexList[0];

            foreach (int number in buttonIndexList)
            {
                if (number < smallest)
                {
                    smallest = number;
                }
            }
            return smallest;

        }

        return 0;
    }

    int FindMaxIndex()
    {
        if (buttonIndexList.Count > 0)
        {

            int smallest = buttonIndexList[0];

            foreach (int number in buttonIndexList)
            {
                if (number > smallest)
                {
                    smallest = number;
                }
            }
            return smallest;

        }

        return 0;
    }



    public void PotalA()
    {
        PotalManager.Instance.MovePotal(0);
    }

    public void PotalB()
    {
        PotalManager.Instance.MovePotal(1);
    }
    public void PotalC()
    {
        PotalManager.Instance.MovePotal(2);
    }
    public void PotalD()
    {
        PotalManager.Instance.MovePotal(3);
    }







}
