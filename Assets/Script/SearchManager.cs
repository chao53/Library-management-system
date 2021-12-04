using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Library
{
    public abstract class SearchManager : MonoBehaviour
    {
        [Header("容器")]
        [SerializeField]
        protected Transform content;

        [Header("格子预制件")]
        [SerializeField]
        protected GameObject grid;

        [Header("搜索框")]
        [SerializeField]
        protected InputField searchField;

        [Header("统计文本")]
        [SerializeField]
        protected Text statistics;

        [Header("下拉框")]
        [SerializeField]
        protected GameObject box;

        //选中的信息
        protected string[] selectInfo; 

        readonly protected DbAccess db = DbAccess.GetInstance();

        private bool onBox = false;

        //总搜索抽象方法
        abstract public void search(string str = "");

        ////重载虚函数
        //virtual public void search(string str) { }

        protected void Awake()
        {
            search();
        }

        public void showBox()
        {
            //if (CenterUIControlManager.instance.isAD)
            //{
            //    box.SetActive(true);
            //    box.transform.position = Input.mousePosition;
            //}
            box.SetActive(true);
            box.transform.position = Input.mousePosition;
        }

        public void mouseOnBox(bool onbox)
        {
            onBox = onbox;
        }

        public void SelectRow(string[] info)
        {
            selectInfo = info;
        }

        public void Update()
        {
            if (!onBox && (Input.GetMouseButtonDown(0) || Input.GetMouseButtonDown(1)))
            {
                box.SetActive(false);
            }
        }
    }
}
