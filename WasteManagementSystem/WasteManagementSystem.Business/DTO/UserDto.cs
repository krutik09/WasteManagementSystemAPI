﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WasteManagementSystem.Business.DTO;
    public class UserDto
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Email { get; set; }
        public int PhoneNumber { get; set; }
        public int UserTypeId { get; set; }
        public string UserTypeName { get; set; }
        public string Password { get; set; }
}

