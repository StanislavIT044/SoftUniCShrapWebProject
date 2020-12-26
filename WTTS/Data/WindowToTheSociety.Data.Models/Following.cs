namespace WindowToTheSociety.Data.Models
{
    using System;

    using WindowToTheSociety.Data.Common.Models;

    public class Following : BaseModel<string>
    {
        public Following()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string UserId { get; set; }

        public string FollowedUserId { get; set; }
    }
}
