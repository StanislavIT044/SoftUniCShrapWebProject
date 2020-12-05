// ReSharper disable VirtualMemberCallInConstructor
namespace WindowToTheSociety.Data.Models
{
    using System;
    using System.Collections.Generic;
    using System.ComponentModel.DataAnnotations;
    using System.ComponentModel.DataAnnotations.Schema;
    using Microsoft.AspNetCore.Identity;

    using WindowToTheSociety.Data.Common.Models;

    public class ApplicationUser : IdentityUser, IAuditInfo, IDeletableEntity
    {
        public ApplicationUser()
        {
            this.Id = Guid.NewGuid().ToString();
            this.Roles = new HashSet<IdentityUserRole<string>>();
            this.Claims = new HashSet<IdentityUserClaim<string>>();
            this.Logins = new HashSet<IdentityUserLogin<string>>();
            this.Album = new HashSet<Photo>();
            this.Posts = new HashSet<Post>();
        }

        // Personal info
        [Required]
        [MaxLength(25)]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(30)]
        public string Surname { get; set; }

        public Gender Gender { get; set; }

        [Required]
        public DateTime BirthDate { get; set; }

        // Audit info
        public DateTime CreatedOn { get; set; }

        public DateTime? ModifiedOn { get; set; }

        // Deletable entity
        public bool IsDeleted { get; set; }

        public DateTime? DeletedOn { get; set; }

        public virtual ICollection<IdentityUserRole<string>> Roles { get; set; }

        public virtual ICollection<IdentityUserClaim<string>> Claims { get; set; }

        public virtual ICollection<IdentityUserLogin<string>> Logins { get; set; }

        // Pictures
        [ForeignKey("ProfilePicture")]
        public string ProfilePictureId { get; set; }

        public ProfilePicture ProfilePicture { get; set; }

        [ForeignKey("CoverPhoto")]
        public string CoverPhotoId { get; set; }

        public CoverPhoto CoverPhoto { get; set; }

        public virtual ICollection<Photo> Album { get; set; }

        public virtual ICollection<Post> Posts { get; set; }
    }
}
