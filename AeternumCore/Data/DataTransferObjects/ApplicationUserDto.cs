﻿namespace AeternumCore.Data.DataTransferObjects
{
    public class ApplicationUserDto
    {
        public string Id { get; set; }
        public string UserName { get; set; }
        public string Email { get; set; }
        public ApplicationUserProfileDto Profile { get; set; }
    }

}
