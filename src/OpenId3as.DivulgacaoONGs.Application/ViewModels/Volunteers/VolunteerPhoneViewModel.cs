﻿using System;

namespace OpenId3as.DivulgacaoONGs.Application.ViewModels.Volunteers
{
    public class VolunteerPhoneViewModel
    {
        public VolunteerPhoneViewModel()
        {

        }

        public Int64 Id { get; set; }
        public Int64 VolunteerId { get; set; }
        public int Type { get; set; }
        public string Number { get; set; }
        public DateTime Created { get; set; }
        public DateTime Updated { get; set; }
        public bool Active { get; set; }

        public VolunteerViewModel Volunteer { get; set; }
    }
}
