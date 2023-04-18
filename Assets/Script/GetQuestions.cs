using System.Collections;
using System.Collections.Generic;
using System.Text;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GetQuestions : MonoBehaviour
{
    private string[] lineQuestionsSingle;
    private string[][] QuestionArray;
    private int questionSum = 0;

    private int titleIndex = 0;

    public Canvas endCanvas;

    [Tooltip("��Ŀ��UI")]
    public List<GameObject> SelectUI;
    [Header("��ѡ��Ŀ����ֶ�")]
    [Tooltip("ѡ��أ�Toggle���б�")]
    public List<Toggle> toggleList;
    [Tooltip("��Ŀ����")]
    public Text title;
    [Tooltip("ѡ�����ֲ��֣�TextMeshPro���б�")]
    public List<Text> options;
    private int selectIndex;

    [Header("��ѡ��Ŀ����ֶ�")]
    [Tooltip("ѡ��أ�Toggle���б�")]
    public List<Toggle> toggleListMore;
    [Tooltip("��Ŀ����")]
    public Text titleMore;
    [Tooltip("ѡ�����ֲ��֣�TextMeshPro���б�")]
    public List<Text> optionsMore;
    private string selectIndexMore;

    [Header("������Ŀ����ֶ�")]
    [Tooltip("��Ŀ����")]
    public Text titleSort;
    [Tooltip("ѡ���б�")]
    public List<Text> optionsSort;
    [Tooltip("�����б�")]
    public List<Text> InterfaceList;
    public List<GameObject> optionsBtn;
    private string selectAnswerSort;
    // Start is called before the first frame update
    void Start()
    {
        GetQuestion();
        LoadQuestion();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.V))
        {
            Verify();
        }
    }

    void GetQuestion()
    {
        TextAsset textAsset = Resources.Load("Question", typeof(TextAsset)) as TextAsset;
        lineQuestionsSingle = textAsset.text.Split('\n');
        //test
        foreach(var i in lineQuestionsSingle)
        {
            Debug.Log(i);
        }
        //end
        QuestionArray = new string[lineQuestionsSingle.Length][];//[5][]
        for(int i = 0; i < lineQuestionsSingle.Length; i++)
        {
            QuestionArray[i] = lineQuestionsSingle[i].Split(':');
        }
        //test
        for(int i = 0; i < QuestionArray.Length; i++)
        {
            for(int j = 0; j < QuestionArray[i].Length; j++)
            {
                Debug.Log(QuestionArray[i][j]);
            }
        }
        //end
        questionSum = lineQuestionsSingle.Length;
    }

    void LoadQuestion()
    {
        for(int i = 0; i < toggleList.Count; i++)
        {
            toggleList[i].isOn = false;
        }


        int optionCount;
        switch (QuestionArray[titleIndex][0])
        {
            case "��ѡ":
                Debug.Log("��ǰ��" + titleIndex + "�⣻ �ܹ�" + questionSum + "��");
                for (int i = 0; i < SelectUI.Count; i++)
                {
                    SelectUI[i].gameObject.SetActive(false);
                    if (SelectUI[i].gameObject.name == "OneSelect_UI") SelectUI[i].gameObject.SetActive(true);
                }
                title.text = QuestionArray[titleIndex][1];
                optionCount = QuestionArray[titleIndex].Length - 3;
                for (int i = 0; i < options.Count; i++)
                {
                    options[i].text = QuestionArray[titleIndex][i + 2];
                }
                break;
            case "��ѡ":
                Debug.Log("��ǰ��" + titleIndex + "�⣻ �ܹ�" + questionSum + "��");
                for (int i = 0; i < SelectUI.Count; i++)
                {
                    SelectUI[i].gameObject.SetActive(false);
                    if (SelectUI[i].gameObject.name == "MoreSelect_UI") SelectUI[i].gameObject.SetActive(true);
                }
                titleMore.text = QuestionArray[titleIndex][1];
                optionCount = QuestionArray[titleIndex].Length - 3;
                for (int i = 0; i < optionsMore.Count; i++)
                {
                    optionsMore[i].text = QuestionArray[titleIndex][i + 2];
                }
                break;
            case "����":
                //for (int i = 0; i < optionsBtn.Count; i++)
                //{
                //    Debug.Log(optionsBtn[i].transform.position);
                //}
                Debug.Log("��ǰ��" + titleIndex + "�⣻ �ܹ�" + questionSum + "��");
                for (int i = 0; i < SelectUI.Count; i++)
                {
                    SelectUI[i].gameObject.SetActive(false);
                    if (SelectUI[i].gameObject.name == "DragSelect_UI") SelectUI[i].gameObject.SetActive(true);
                }
                titleSort.text = QuestionArray[titleIndex][1];
                optionCount = QuestionArray[titleIndex].Length - 3;
                for (int i = 0; i < optionsSort.Count; i++)
                {
                    optionsSort[i].color = Color.black;
                    optionsSort[i].text = QuestionArray[titleIndex][i + 2];
                }
                break;
            default:
                break;
        }
    }

    public void GetSelect()
    {
        switch (QuestionArray[titleIndex][0])
        {
            case "��ѡ":
                for (int i = 0; i < toggleList.Count; i++)
                {
                    if (toggleList[i].isOn == true)
                    {
                        selectIndex = i + 1;
                    }
                }
                break;
            case "��ѡ":
                for (int i = 0; i < toggleListMore.Count; i++)
                {
                    if (toggleListMore[i].isOn == true)
                    {
                        selectIndexMore += (i+1);
                    }
                }
                break;
            case "����":
                for (int i = 0; i < InterfaceList.Count; i++)
                {
                    selectAnswerSort += InterfaceList[i].text;
                }
                break;
            default:
                break;
        }
    }
    public void Verify()
    {
        switch (QuestionArray[titleIndex][0])
        {
            case "��ѡ":
                for (int i = 0; i < options.Count; i++)
                {
                    options[i].color = Color.white;
                }
                GetSelect();
                if (QuestionArray[titleIndex][QuestionArray[titleIndex].Length - 1] == selectIndex.ToString())
                {
                    titleIndex++;
                    if (titleIndex+1 < questionSum || titleIndex + 1 == questionSum)
                    {
                        LoadQuestion();
                    }
                    else
                    {
                        //SceneManager.LoadScene("EndSence");
                        endCanvas.gameObject.SetActive(true);
                    }
                    Debug.Log("��ȷ");
                }
                else
                {
                    options[selectIndex - 1].color = Color.red;
                    Debug.Log("����");
                }
                break;
            case "��ѡ":
                for (int i = 0; i < optionsMore.Count; i++)
                {
                    optionsMore[i].color = Color.white;
                }
                GetSelect();
                if (QuestionArray[titleIndex][QuestionArray[titleIndex].Length - 1] == selectIndexMore)
                {
                    titleIndex++;
                    for (int i = 0; i < toggleListMore.Count; i++)
                    {
                        toggleListMore[i].isOn = false;
                    }
                    if(titleIndex + 1 < questionSum || titleIndex + 1 == questionSum)
                    {
                        LoadQuestion();
                    }
                    else
                    {
                        //SceneManager.LoadScene("EndSence");
                        endCanvas.gameObject.SetActive(true);
                    }
                    selectIndexMore = "";
                    Debug.Log("��ȷ");
                }
                else
                {
                    for(int i = 0; i < toggleListMore.Count; i++)
                    {
                        if(toggleListMore[i].isOn == true)
                        {
                            optionsMore[i].color = Color.red;
                        }
                    }
                    Debug.Log("����" + selectIndexMore);

                    selectIndexMore = "";
                }
                break;
            case "����":
                for (int i = 0; i < optionsSort.Count; i++)
                {
                    optionsSort[i].color = Color.white;
                }
                GetSelect();
                if (QuestionArray[titleIndex][QuestionArray[titleIndex].Length - 1] == selectAnswerSort)
                {
                    titleIndex++;
                    for(int i = 0; i < optionsBtn.Count; i++)
                    {
                        Debug.Log(optionsBtn[i].GetComponent<OnMouseDrag>().resPos);
                        optionsBtn[i].GetComponent<OnMouseDrag>().fuyuan();
                    }
                    if (titleIndex + 1 < questionSum || titleIndex + 1 == questionSum)
                    {
                        LoadQuestion();
                    }
                    else
                    {
                        //SceneManager.LoadScene("EndSence");
                        endCanvas.gameObject.SetActive(true);
                    }
                    selectAnswerSort = "";
                    Debug.Log("��ȷ");
                }
                else
                {
                    for (int i = 0; i < optionsSort.Count; i++)
                    {
                        optionsSort[i].color = Color.red;
                    }
                    Debug.Log("����" + QuestionArray[titleIndex][QuestionArray[titleIndex].Length - 1] + " ;" + selectAnswerSort);

                    selectAnswerSort = "";
                }
                break;
            default:
                break;
        }
    }
}
