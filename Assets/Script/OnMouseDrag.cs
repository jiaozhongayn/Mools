using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnMouseDrag : MonoBehaviour
{
    //public Image groove;
    private GameObject groove;
    private bool isInArea = false;
    public Vector3 resPos;
    public Text optionText;
    public Text selectIndex;
    private string resText;
    bool canPush;
    Text t;
    // Start is called before the first frame update
    void Start()
    {
        resPos = gameObject.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void test()
    {
        gameObject.transform.localPosition = (Input.mousePosition - new Vector3(UnityEngine.Screen.width/2, UnityEngine.Screen.height/2, 0));
        
        //if(gameObject.transform.position == groove.transform.localPosition)
        //{
        //    Debug.Log("放进去了");
        //}
    }
    public void test1()
    {
        Debug.Log(isInArea);
        if (isInArea && canPush)
        {
            gameObject.transform.position = groove.transform.position;
            Debug.Log(groove.GetComponentInParent<Transform>().position.y);
            groove.GetComponent<CanPush>().canPush = false;
            gameObject.GetComponent<Image>().color = new Color(0, 0, 255,0.2f);



            //switch (groove.GetComponentInParent<Transform>().position.y)
            //{
            //    case 353.5f:
            //        selectIndex.text = optionText.text;
            //        break;
            //    case 313.5f:
            //        selectIndex.text = optionText.text;
            //        break;
            //    case 273.5f:
            //        selectIndex.text = optionText.text;
            //        break;
            //    case 233.5f:
            //        selectIndex.text = optionText.text;
            //        break;
            //    case 193.5f:
            //        selectIndex.text = optionText.text;
            //        break;
            //    case 153.5f:
            //        selectIndex.text = optionText.text;
            //        break;
            //}

        }
        else
        {
            fuyuan();
            
        }
    }

    public void fuyuan()
    {
        
        if(gameObject.GetComponent<Image>().color == new Color(0, 0, 255, 0.2f))
        {
            groove.GetComponent<CanPush>().canPush = true;
            gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 1.0f);
        }
        gameObject.transform.position = resPos;
        
    }

    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    canPush = collision.gameObject.GetComponent<CanPush>().canPush;

        
    //    if (collision.gameObject.tag == "Interface" && canPush)
    //    {
    //        string interfaceName = collision.transform.parent.gameObject.name;
    //        Text t = GameObject.Find(interfaceName + "/TextAnswer").GetComponent<Text>();
    //        resText = t.text;
    //        t.text = optionText.text;
    //    }
    //    groove = collision.gameObject;
    //}

    private void OnTriggerStay2D(Collider2D collision)
    {
        canPush = collision.gameObject.GetComponent<CanPush>().canPush;
        if (collision.gameObject.tag == "Interface" && canPush)
        {
            isInArea = true;
            collision.gameObject.GetComponent<Image>().color = new Color(0, 255, 255, 0.2f);


            string interfaceName = collision.transform.parent.gameObject.name;
            Text t = GameObject.Find(interfaceName + "/TextAnswer").GetComponent<Text>();
            resText = t.text;
            t.text = optionText.text;
            groove = collision.gameObject;
        }
       
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(gameObject.GetComponent<Image>().color == new Color(0, 0, 255, 0.2f))
        {
            collision.gameObject.GetComponent<CanPush>().canPush = true;
        } 
        if (collision.gameObject.tag == "Interface" && gameObject.GetComponent<Image>().color == new Color(0, 0, 255, 0.2f))
        {
            isInArea = false;
            collision.gameObject.GetComponent<Image>().color = new Color(255, 255, 255, 0.2f);
            string interfaceName = collision.transform.parent.gameObject.name;
            t = GameObject.Find(interfaceName + "/TextAnswer").GetComponent<Text>();
            t.text = "null";
        }
    }
}
