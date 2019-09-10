using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Principal;
namespace FinaceTool.Common
{
    public class CustomPrincipal : IPrincipal
    {
        public IIdentity Identity { get; private set; }
        public bool IsInRole(string role)
        {
            List<string> roleList = role.Split(',').ToList();

            foreach (var item in roleList)
            {
                if (item == RoleID.ToString())
                {
                    return true;
                }
            }
            return false;
        }

        public CustomPrincipal(string Username)
        {
            this.Identity = new GenericIdentity(Username);
        }

        public string SapId { get; set; }
        public int UserId { get; set; }
        public string UserName { get; set; }
        public string UserEmail { get; set; }
        public int RoleID { get; set; }
        public string RoleName { get; set; }

    }
}