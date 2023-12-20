using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace wleepay.Core.Models
{
    public partial class UserDetail
    {
        public int Id { get; set; }
        public string UserId { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Gender { get; set; }
        public string PhoneNumber { get; set; }
        public DateTime? DOB { get; set; }
        public int KYCStatusId { get; set; }
        public string Image { get; set; }
        public int CountyId { get; set; }
        public int? KYCLevel { get; set; }
        public bool IsLoggedIn { get; set; } = false;
        public string AccountNumber { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime UpdatedDate { get; set; }
        public string KYCDocumentImage { get; set; }
        public DateTime? LastTimeLoggedIn { get; set; }
        public string VerificationCode { get; set; }
        public DateTime? VerificationExpiry { get; set; }
        public int Status { get; set; }

        public int SecurityQuestionId { get; set; }
        public string SecurityAnswerText { get; set; }

        public virtual ApplicationUser ApplicationUser { get; set; }
        public virtual KYCStatus KYCStatus { get; set; }
        public virtual County County { get; set; }
        public virtual KYCLevel KYCLevelDetail { get; set; }
        public virtual SecurityQuestions SecurityQuestions { get; set; }

    }
}
