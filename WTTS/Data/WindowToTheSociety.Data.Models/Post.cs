namespace WindowToTheSociety.Data.Models
{
    using System;
    using System.ComponentModel.DataAnnotations;

    using WindowToTheSociety.Data.Common.Models;

    public class Post : IAuditInfo, IDeletableEntity
    {
        public Post()
        {
            this.Id = Guid.NewGuid().ToString();
        }

        public string Id { get; set; }

        [MaxLength(3000)]
        public string Text { get; set; }

        public string PhotoId { get; set; }

        public Photo Photo { get; set; }

        public string ApplicationUserId { get; set; }

        public ApplicationUser ApplicationUser { get; set; }

        public string PageId { get; set; }

        public Page Page { get; set; }

        // TODO: Implement Likes int [Required], ICollection<Comment> Comments

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }
    }
}
