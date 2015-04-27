using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace EliaStore.Models
{
    public class shirt
    {
        [Key]
        public int ID { get; set; }
        public virtual shirtbrand brand{ get; set; }
        public virtual shirtmodel model{ get; set; }
        public string img_url { get; set; }
        public decimal price { get; set; }
        public shirt_color color { get; set; }
        public string big_img_url { get; set; }


    }


    public class shirtbrand
    {        
        private List<shirtmodel> _models = new List<shirtmodel>();
        [Key]
        public int ID { get; set; }
        public string name { get; set; }
        public virtual ICollection<shirt> shirts { get; set; }
        public virtual List<shirtmodel> models
        {
            get { return _models; }
            set { _models = value; }
        }
    }


    public class shirtmodel 
    {
        
        [Key]
        public int ID { get; set; }
        public string name { get; set; }
        public virtual shirtbrand brand { get; set; }
        public virtual ICollection<shirt> shirts { get; set; }
    }

    public enum shirt_color : int
    {
        Black = 1,
        White = 2,
        Grey = 3,
        Red = 4,
        Green = 5,
        Beige = 6
    }

}

