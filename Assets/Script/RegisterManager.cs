using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace Library
{

    public class RegisterManager : MonoBehaviour
    {
        [Header("用户名输入框")]
        [SerializeField]
        private InputField userNameField;

        [Header("密码输入框1")]
        [SerializeField]
        private InputField passWordField1;

        [Header("密码输入框2")]
        [SerializeField]
        private InputField passWordField2;

        [Header("邀请码输入框")]
        [SerializeField]
        private InputField inviteField;

        readonly DbAccess db = DbAccess.GetInstance();

        public void register()
        {
            if (userNameField.text.Trim().Length * passWordField1.text.Trim().Length * passWordField2.text.Trim().Length * inviteField.text.Trim().Length == 0)
            {
                CenterUIControlManager.instance.warn("请填写完所有信息栏!", 1);
            }
            else if (passWordField1.text != passWordField2.text)
            {
                CenterUIControlManager.instance.warn("两次密码不相同!", 1);
            }
            else if (db.ExecuteQuery($"SELECT * FROM adInfo WHERE userName = '{userNameField.text}'").HasRows)
            {
                CenterUIControlManager.instance.warn("该用户名已被注册!", 1);
            }
            else
            {
                CenterUIControlManager.instance.warn("注册成功", 0);
                db.ExecuteQuery($"INSERT INTO adInfo (userName,passWord) VALUES ('{userNameField.text}','{passWordField1.text}')");
            }
        }
    }

}
