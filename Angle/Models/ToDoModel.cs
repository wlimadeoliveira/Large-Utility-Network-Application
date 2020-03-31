using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace LUNA.Models
{
    public class ToDoModel
    {
        public long Id { get; set; }
        private string _key;
        public string Key
        {
            get
            {
                if (_key == null)
                {
                    _key = Regex.Replace(Title.ToLower(), "[^a-z0-9]", "-");
                }
                return _key;
            }
            set { _key = value; }
        }
        [Required]
        [DataType(DataType.Text)]
        [MinLength(5, ErrorMessage = "Min 5 Letters")]
        public string Title { get; set; }
        [Required]
        [DataType(DataType.MultilineText)]
        [MinLength(5, ErrorMessage = "Min 5 Letters")]
        public string Text { get; set; }

        public DateTime Created { get; set; }
    }
}
