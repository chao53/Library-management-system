using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Library
{
    public class SearchBookManager : SearchManager
    {
        [Header("借阅信息面板")]
        [SerializeField]
        private SearchLoanInfoManager searchLoanInfoManager;
        
        override public void search(string str = "")
        {
            foreach (Transform trans in content)
            {
                Destroy(trans.gameObject);
            }
            if (str.Length == 0)
            {
                str = searchField.text.Replace(' ', '%');
            }
            Mono.Data.Sqlite.SqliteDataReader res =
                db.ExecuteQuery($"SELECT * FROM bookInfo WHERE name LIKE '%{str}%' OR category LIKE '%{str}%' OR ID = '{str}'");
            int count = 0;
            while (res.Read())
            {
                count++;
                string bookstate;
                if (res[3].ToString() == "0")
                {
                    bookstate = "在馆";
                }
                else if (res[3].ToString() == "1")
                {
                    bookstate = "已借出";
                }
                else
                {
                    bookstate = "已不存在";
                }
                GameObject newGrid = Instantiate(grid);
                newGrid.GetComponent<GridScript>().searchManager = this;
                newGrid.GetComponent<GridScript>().fill(new string[] { res[0].ToString(), res[1].ToString(), res[2].ToString(), bookstate, res[3].ToString() == "0" ? res[4].ToString() : " " });
                newGrid.transform.SetParent(content);
                newGrid.transform.localScale = new Vector3(1, 1, 1);
            }
            statistics.text = $"共检索到{count}条图书信息";
        }

        //跳转借阅
        public void toLoan()
        {
            CenterUIControlManager.instance.pageJumpAsyn(searchLoanInfoManager.gameObject);
            searchLoanInfoManager.search("2");
            gameObject.SetActive(false);
        }
    }
}