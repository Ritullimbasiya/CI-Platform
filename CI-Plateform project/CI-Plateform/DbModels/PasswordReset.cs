﻿using System;
using System.Collections.Generic;

namespace CI_Plateform.DbModels
{
    public partial class PasswordReset
    {
        public long PasswordResetId { get; set; }
        public string Email { get; set; } = null!;
        public string Token { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
    }
}
