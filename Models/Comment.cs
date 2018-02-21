using System;
using user_dashboard.Models;
using System.Collections.Generic;
namespace user_dashboard.Models{
    public class Comment{
        public int CommentId{get;set;}
        public int UserId{get;set;}
        public User User{get;set;}
        public int MessageId{get;set;}
        public string Content{get;set;}
        public DateTime CreatedAt{get;set;}
        public DateTime UpdatedAt{get;set;}

        public Comment(){
            CreatedAt=DateTime.Now;
            UpdatedAt=DateTime.Now;
        }
    }
}