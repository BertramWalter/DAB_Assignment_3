using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using MongoDB.Driver;
using DAB_Assignment_3.Models;

namespace DAB_Assignment_3.Services
{
    public class CommentServices
    {
        private IMongoCollection<Comment> _comments;
        private IMongoCollection<User> _users;
        private IMongoCollection<Circle> _circle;
        private IMongoCollection<Post> _post;

        public CommentServices()
        {
            var client = new MongoClient("mongodb://localhost:27017");
            var database = client.GetDatabase("SocialNetworkDb");

            _comments = database.GetCollection<Comment>("Comments");
            _users = database.GetCollection<User>("Users");
            _post = database.GetCollection<Post>("Posts");
            _circle = database.GetCollection<Circle>("Circles");
        }

        public void CreateComment(string post_id, string userId, string c)
        {
            var user = _users.Find(x => x.Id == userId).FirstOrDefault();
            /*if (user.BlockId == null)
            {
                return false;
            }*/

            //if no posts available
            var post = _post.Find(x => x.PostId == post_id ).FirstOrDefault();
            if (post == null)
            {
                Console.WriteLine("Invalid post");
            }
            else
            {
                if (post.IsPublic == true)
                { // if post is public
                    if (post.BlockedAllowedUserId.Contains(userId))
                    {
                        Console.WriteLine("UserID is blocked, you cannot write any comment.");
                    }
                    else
                    {
                        var comment = new Comment(post_id, user.Id, user.Name, c,DateTime.Now);
                        Console.WriteLine("Comment added");
                        _comments.InsertOne(comment);
                    }
                }
                else
                { // if post is private.
                    if (post.BlockedAllowedUserId.Contains(userId))
                    {
                        var comment = new Comment(post_id, user.Id, user.Name, c, DateTime.Now);
                        Console.WriteLine("Comment added");
                        _comments.InsertOne(comment);
                    }
                    else
                    {
                        Console.WriteLine("UserID is not a part of any of the user circles.");
                    }
                }
            }

            //post_id = post.PostId;
            
            //   user = _users;
            //DateTime comment.DateTime = datetime;
            //comment.DateTime = DateTime.Parse(Date);
            //Console.WriteLine("Type your comment: ");
            //string commentstring = Console.ReadLine();
            //comment.AuthorName = post.AuthorName;
            //comment.CommentString = commentstring;
            //comment.AuthorId = post.AuthorId;
            //comment.PostId = post_id;
            //this.DateTime = Console.WriteLine("{Date}");
            //Comment postcomment = new Comment(postid, authorid, authorname, commentstring, DateTime);
            

        }


    }
}
