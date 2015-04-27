using EliaStore.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EliaStore.ViewModel
{
    public class ShowroomViewModel
    {
        public shirt shirt { get; set; }

        public List<shirt> related_shirts  { get; set; }
    }
}