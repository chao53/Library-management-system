using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Library
{
    public class SearchLoanInfoManager : SearchManager
    {
        // Start is called before the first frame update
        public override void search(string str = "")
        {
            foreach (Transform trans in content)
            {
                Destroy(trans.gameObject);
            }
            if(str.Length == 0)
            {
                str = searchField.text.Replace(' ', '%');
            }         
            Mono.Data.Sqlite.SqliteDataReader res =
                db.ExecuteQuery($"SELECT * FROM loanInfo WHERE readerName LIKE '%{str}%' OR bookName LIKE '%{str}%' OR readerID = '{str}'");
            int count = 0;
            while (res.Read())
            {
                count++;
                GameObject newGrid = Instantiate(grid);
                newGrid.GetComponent<GridScript>().searchManager = this;
                newGrid.GetComponent<GridScript>().fill(new string[] { res[0].ToString(), res[1].ToString(), res[2].ToString(), res[3].ToString(),res[4].ToString(), res[5].ToString(), res[6].ToString().Length == 0?"未归还": res[6].ToString() });
                newGrid.transform.SetParent(content);
                newGrid.transform.localScale = new Vector3(1, 1, 1);
            }
            statistics.text = $"共检索到{count}条读者信息";
        }
    }

}
