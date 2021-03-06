using Project.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project.viewModel
{
    public class accountListViewModel
    {
        public accountList movie { get; set; }

        public List<accountList> movies { get; set; }
    }
}