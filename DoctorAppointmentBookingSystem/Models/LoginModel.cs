﻿using DoctorAppointmentBookingSystem.Enum;

namespace DoctorAppointmentBookingSystem.Models
{
    public class LoginModel
    {
        public bool RememberMe { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
