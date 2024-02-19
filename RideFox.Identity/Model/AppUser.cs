﻿using Microsoft.AspNetCore.Identity;

namespace RideFox.Identity.Model;

public class AppUser : IdentityUser
{
	public string FirstName { get; set; }
	public string LastName { get; set; }
}