﻿using System;

namespace BeStreet.Domain.Entities.User
{
    public class URegData
    {
        public string Name { get; set; }
        public string Login { get; set; }
        public string Pass { get; set; }
        public string Email { get; set; }
        public string Add { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime LastLogin { get; set; }

    }
}
