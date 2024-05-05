using API.Messages;
using Core;
using Core.Models;
using Core.ViewModels;
using log4net;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;


namespace API.Services
{

    public class UserService : ServiceBase<User, UserView>
    {

        static readonly object thisLock = new object();
        private static readonly ILog log = LogManager.GetLogger(typeof(UserService));

        public UserService(IUnitOfWork unitOfWork) : base(unitOfWork)
        {
            
        }



        private string MD5encrypt(string input)
        {
            MD5 md5 = MD5.Create();
            md5.ComputeHash(Encoding.ASCII.GetBytes(input));
            byte[] result = md5.Hash;

            StringBuilder strBuilder = new StringBuilder();
            for (int i = 0; i < result.Length; i++)
            {
                //change it into 2 hexadecimal digits
                //for each byte
                strBuilder.Append(result[i].ToString("x2"));
            }


            return strBuilder.ToString();
        }







        
    }

}