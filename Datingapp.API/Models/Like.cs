namespace Datingapp.API.Models
{
    public class Like
    {
        public int LikerId { get; set; }    
        public int LikeeId { get; set; }
        public user Liker { get; set; }
        public user Likee { get; set; }

        public int likecount { get; set; }
    }
}