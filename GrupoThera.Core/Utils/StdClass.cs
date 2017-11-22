﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace GrupoThera.Core.Utils
{
    public static class StandardClass
    {

        public static HttpCookie CreateCookie(cookieData data)
        {
            byte[] encrypted = System.Text.Encoding.Unicode.GetBytes(data.UserName);
            string userEncrypted = Convert.ToBase64String(encrypted);

            encrypted = System.Text.Encoding.Unicode.GetBytes(data.Password);
            string passwordEcrypted = Convert.ToBase64String(encrypted);

            HttpCookie aCookie = new HttpCookie("userInfoThera");
            aCookie.Values["userName"] = userEncrypted;
            aCookie.Values["password"] = passwordEcrypted;
            aCookie.Values["Name"] = data.UserName;
            aCookie.Values["ListRoles"] = string.Join(",", data.ListRoles.ToArray());
            aCookie.Expires = DateTime.Now.AddDays(30);
            return aCookie;
        }

         

        public static bool SessionValid(string user, string password)
        {
            //try
            //{
            //    using (DirectoryEntry entry = new DirectoryEntry())
            //    {
            //        entry.Username = user;
            //        entry.Password = password;
            //        DirectorySearcher searcher = new DirectorySearcher(entry);
            //        searcher.Filter = "(objectclass=user)";

            //        var res = searcher.FindOne();
            //    }
            //}
            //catch (Exception) { return false; }

            return true;
        } 
    } 

    public class cookieData
    {
        public string UserName { get; set; }
        public string Password { get; set; }
        public List<string> ListRoles { get; set; }

    } 
    
}