using System;
using System.ComponentModel.DataAnnotations;

namespace MyWebApp.Models
{
    public class User
    {
        public Guid Id { get; set; }
        [Display(Name = "氏名")]
        public string Name { get; set; }
        [Display(Name = "年齢")]
        public int Age { get; set; }
        [Display(Name = "プロフィール画像")]
        public string ProfileFileName { get; set; }
    }
}