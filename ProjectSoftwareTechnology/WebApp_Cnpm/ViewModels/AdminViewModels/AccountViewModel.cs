using Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApp_Cnpm.Models.Helper;

namespace WebApp_Cnpm.ViewModels.AdminViewModels
{
    public class AccountViewModel
    {
        #region fields user
        public PaginatedList<AccountDTO> Accounts { get; set; }
        public int ID_User { get; set; }
        public String Status_User { get; set; }
        #endregion

        #region fields admin
        public PaginatedList<AdminDTO> Admins { get; set; }
        public int ID_Admin { get; set; }
        public String Status_Admin { get; set; }
        public String Permission { get; set; }
        public ChangePasswordKeys ChangePasswordKeys { get; set; }
        public ChangePositionKeys ChangePositionKeys { get; set; }
        public ChangeYourSelfAdminPassKeys ChangeYourSelfAdminPassKeys { get; set; }
        public PermissionAdminKeys Keys { get; set; }
        public CreateNewAccountAdminKeys createNewAdmin { get; set; }

        #endregion

        #region fields function
        public PaginatedList<FunctionDTO> Functions { get; set; }
        public String ID_Function { get; set; }
        public String Name_Function { get; set; }
        public String Name_Function_Edit { get; set; }
        #endregion

        #region Comon
        public int countUser { get; set; }
        public int countAdmin { get; set; }
        public int countFunction { get; set; }
        #endregion
    }
}
