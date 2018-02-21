using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;


namespace user_dashboard.Models{
    public class User{
        public int UserId{get;set;}
        public string FirstName{get;set;}
        public string LastName{get;set;}
        
        public string Email{get;set;}
        public string Password{get;set;}
        public string Status{get;set;}
        public string Description{get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime UpdatedAt{get;set;}
        public List <Message> Messages{get;set;}
        public User(){
            Messages=new List<Message>();
            Status="normal";
            CreatedAt=DateTime.Now;
            UpdatedAt=DateTime.Now;
            Description="";
        }
    }
}
