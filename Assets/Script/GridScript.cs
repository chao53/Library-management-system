using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Library
{
    public class GridScript : MonoBehaviour
    {
        [Header("字段")]
        [SerializeField]
        private Text[] texts;

        [Header("隶属模块")]
        public SearchManager searchManager;

        private string[] info;
        void Start()
        {
            GetComponent<Button>().onClick.AddListener(OnClick);
        }

        private void OnClick()
        {
            searchManager.showBox();
            searchManager.SelectRow(info);
        }

        public void fill(string[] strs)
        {
            info = strs;
            for (int i = 0; i < texts.Length; i++)
            {
                texts[i].text = strs[i];
            }
        }
    }
}
