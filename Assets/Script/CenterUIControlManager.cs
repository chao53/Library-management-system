using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Library {
    public class CenterUIControlManager : MonoBehaviour
    {
        [Header("提示条")]
        [SerializeField]
        private Image WarningBar;

        [Header("提示文字")]
        [SerializeField]
        private Text WarningText;

        //页面栈
        private Stack<GameObject> pageSta = new Stack<GameObject>();
        //当前页面
        private GameObject currentPage;

        private float timer = 0;
        // 提示条透明度
        private float a = 0.7f;
        //单例
        public static CenterUIControlManager instance;

        public bool isAD = false; 
        public void Start()
        {
            instance = this;
        }

        public void warn(string str,int isWarn)
        {
            WarningBar.gameObject.SetActive(true);
            WarningText.text = str;
            WarningBar.color = new Color(isWarn, 1 - isWarn, 0, 0.5f + isWarn*0.2f);
            WarningText.color = new Color(1, 1, 1, 1);
            timer = 1.5f;
            a = 0.5f + isWarn * 0.2f;
        }

        public void pageJump(GameObject nextPage)
        {

            if (currentPage)
            {
                pageSta.Push(currentPage);
                currentPage.SetActive(false);
            }
            nextPage.SetActive(true);
            currentPage = nextPage;
        }
        
        public void pageJumpAsyn(GameObject nextPage)
        {
            pageSta.Push(currentPage);
            nextPage.SetActive(true);
            currentPage = nextPage;
        }

        public void pageBack()
        {
            currentPage.SetActive(false);
            currentPage = pageSta.Pop();
            currentPage.SetActive(true);
        }

        private void Update()
        {
            //控制提示条
            if(timer > 0)
            {
                timer -= Time.deltaTime;
            }
            else
            {
                WarningBar.gameObject.SetActive(false);
            }
            if(timer > 0 && timer < 0.5f)
            {
                a -= 1.4f * Time.deltaTime;
                WarningBar.color = new Color(WarningBar.color.r, WarningBar.color.g, 0, a);
                WarningText.color = new Color(1, 1, 1, a);
            }

            if (Input.GetKey(KeyCode.Escape))
            {
                if (currentPage)
                {
                    pageBack();
                }
            }
        }
    }
}


