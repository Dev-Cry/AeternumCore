using System;
using System.ComponentModel.DataAnnotations;

namespace AeternumCore.Data.Entity
{
    /// <summary>
    /// Reprezentuje osobní informace uživatele.
    /// </summary>
    public class ApplicationUserProfileEntity
    {
        public int Id { get; set; }

        /// <summary>
        /// Jméno uživatele.
        /// </summary>
        [Required(ErrorMessage = "Jméno je povinné.")]
        [StringLength(50, ErrorMessage = "Jméno nesmí překročit 50 znaků.")]
        public required string FirstName { get; set; }

        /// <summary>
        /// Příjmení uživatele.
        /// </summary>
        [Required(ErrorMessage = "Příjmení je povinné.")]
        [StringLength(50, ErrorMessage = "Příjmení nesmí překročit 50 znaků.")]
        public required string LastName { get; set; }

        /// <summary>
        /// Datum narození uživatele.
        /// </summary>
        [Required(ErrorMessage = "Datum narození je povinné.")]
        [DataType(DataType.Date)]
        [Display(Name = "Datum narození")]
        public required DateTime DateOfBirth { get; set; }

        /// <summary>
        /// URL adresa profilového obrázku.
        /// </summary>
        [Url(ErrorMessage = "URL profilového obrázku musí být platná URL adresa.")]
        public string? ProfilePictureUrl { get; set; }

        /// <summary>
        /// Ulice a číslo popisné.
        /// </summary>
        [StringLength(100, ErrorMessage = "Ulice nesmí překročit 100 znaků.")]
        public string? StreetAddress { get; set; }

        /// <summary>
        /// Město uživatele.
        /// </summary>
        [StringLength(50, ErrorMessage = "Název města nesmí překročit 50 znaků.")]
        public string? City { get; set; }

        /// <summary>
        /// PSČ uživatele.
        /// </summary>
        [StringLength(10, ErrorMessage = "PSČ nesmí překročit 10 znaků.")]
        public string? PostalCode { get; set; }

        /// <summary>
        /// Země uživatele.
        /// </summary>
        [StringLength(50, ErrorMessage = "Název země nesmí překročit 50 znaků.")]
        public string? Country { get; set; }

        // Cizí klíč na ApplicationUserEntity
        public string UserId { get; set; }

        /// <summary>
        /// Navigační vlastnost pro přístup k uživatelským informacím.
        /// </summary>
        public virtual ApplicationUserEntity User { get; set; }

        /// <summary>
        /// Vrátí celé jméno uživatele.
        /// </summary>
        public string GetFullName()
        {
            return $"{FirstName} {LastName}";
        }

        /// <summary>
        /// Aktualizuje profilové informace.
        /// </summary>
        /// 
        public void UpdateProfile(string firstName, string lastName, DateTime dateOfBirth, string profilePictureUrl)
        {
            FirstName = firstName;
            LastName = lastName;
            DateOfBirth = dateOfBirth;
            ProfilePictureUrl = profilePictureUrl;
        }

        /// <summary>
        /// Kontroluje, zda jsou profilové informace platné.
        /// </summary>
        public bool IsValid()
        {
            var validationContext = new ValidationContext(this);
            var validationResults = new List<ValidationResult>();
            return Validator.TryValidateObject(this, validationContext, validationResults, true);
        }

        /// <summary>
        /// Obnoví profilový obrázek na výchozí hodnotu.
        /// </summary>
        public void ResetProfilePicture()
        {
            ProfilePictureUrl = null; // nebo nastavte na výchozí URL
        }

        /// <summary>
        /// Aktualizuje adresu uživatele.
        /// </summary>
        public void UpdateAddress(string streetAddress, string city, string postalCode, string country)
        {
            StreetAddress = streetAddress;
            City = city;
            PostalCode = postalCode;
            Country = country;
        }
    }
}
