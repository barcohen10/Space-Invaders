using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Xna.Framework.Input;

namespace SpaceInvaders.Menu
{
    public delegate void ClickedEventHandler();

    public class MenuItem
    {
        private readonly string r_ItemName;
        protected Dictionary<Keys, MethodKey> m_AllMethods;

        private event ClickedEventHandler m_MethodToRun = null;

        public MenuItem(string i_ItemName, params MethodKey[] i_Methods)
        {
            r_ItemName = i_ItemName;
            m_AllMethods = new Dictionary<Keys, MethodKey>();

            foreach(MethodKey methodKey in i_Methods)
            {
              m_AllMethods.Add(methodKey.ActivateKey, methodKey);
            }
        }

        public string ItemName
        {
            get { return r_ItemName; }
        }

        /// <summary>
        /// Run all the methods of menu item in his ClickedEventHandler delegate
        /// </summary>
        public void RunMethod(Keys i_Key)
        {
            if(m_AllMethods.ContainsKey(i_Key))
            {
                m_MethodToRun = m_AllMethods[i_Key].MethodToRun;

                if (m_MethodToRun != null)
                {
                    m_MethodToRun.Invoke();
                }
            }
        }
    }
}
