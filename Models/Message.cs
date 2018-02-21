using System;
using user_dashboard.Models;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;
namespace user_dashboard.Models{
    public class Message{
        public int MessageId{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}        
        public int ProfileId{get;set;}
        public string Content{get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime UpdatedAt{get;set;}
        public List<Comment> Comments{get;set;}

        public Message(){
            Comments=new List<Comment>();
            CreatedAt=DateTime.Now;
            UpdatedAt=DateTime.Now;
        }
    }
}