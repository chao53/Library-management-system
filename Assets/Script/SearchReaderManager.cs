using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Library
{
    public class SearchReaderManager : SearchManager
    {
        public override void search(string str = "")
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
                db.ExecuteQuery($"SELECT * FROM readerInfo WHERE userName LIKE '%{str}%' OR realName LIKE '%{str}%'");
            int count = 0;
            while (res.Read())
            {
                count++;
                string readerState;
                if (res[5].ToString() == "1")
                {
                    readerState = "正常";
                }
                else
                {
                    readerState = "已注销";
                }
                GameObject newGrid = Instantiate(grid);
                newGrid.GetComponent<GridScript>().searchManager = this;
                newGrid.GetComponent<GridScript>().fill(new string[] { res[0].ToString(), res[1].ToString(), res[3].ToString(), res[4].ToString(), readerState});
                newGrid.transform.SetParent(content);
                newGrid.transform.localScale = new Vector3(1, 1, 1);
            }
            statistics.text = $"共检索到{count}条读者信息";
        }
    }

}
