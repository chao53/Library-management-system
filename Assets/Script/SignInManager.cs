using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Library
{
    public class SignInManager : MonoBehaviour
    {
        [Header("用户名输入框")]
        [SerializeField]
        private InputField userNameField;

        [Header("密码输入框")]
        [SerializeField]
        private InputField passWordField;

        [Header("管理员面板")]
        [SerializeField]
        private GameObject adPanel;

        [Header("权限管理按钮")]
        [SerializeField]
        private GameObject opButton;

        readonly DbAccess db = DbAccess.GetInstance();

        public void signIn()
        {
            if (userNameField.text.Trim().Length * passWordField.text.Trim().Length == 0)
            {
                CenterUIControlManager.instance.warn("用户名或密码不能为空!", 1);
            }
            else if (db.ExecuteQuery($"SELECT passWord FROM adInfo WHERE userName = '{userNameField.text}'")[0].ToString() == passWordField.text)
            {
                if (userNameField.text == "000")
                {
                    CenterUIControlManager.instance.warn("欢迎超级管理员", 0);
                    CenterUIControlManager.instance.pageJump(adPanel);
                    CenterUIControlManager.instance.isAD = true;
                    opButton.SetActive(true);
                }
                else
                {
                    CenterUIControlManager.instance.warn("登录成功", 0);
                    CenterUIControlManager.instance.pageJump(adPanel);
                    CenterUIControlManager.instance.isAD = true;
                    opButton.SetActive(false);
                }
            }
            else
            {
                CenterUIControlManager.instance.warn("用户名或密码错误!", 1);
            }
        }
    }

}

